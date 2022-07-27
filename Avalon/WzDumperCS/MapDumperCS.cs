using HaCreator.MapEditor;
using HaCreator.MapEditor.Instance.Shapes;
using HaCreator.MapSimulator;
using HaCreator.Wz;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using MapleLib.WzLib.WzStructure;
using MapleLib.WzLib.WzStructure.Data;
using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static HaCreator.MapSimulator.MapSimulator;

namespace WzDumperCS
{
    public class MapDumperCS
    {
        public MapDumperCS(string maplestoryDirectory)
        {
            InitializeWzFiles(maplestoryDirectory);
        }

        public List<WzDumper.AvailableMap> GetAvailableMaps()
        {
            var maps = new List<WzDumper.AvailableMap>();
            foreach (var (mapId, mapStreetName, mapName) in WzFileManager.Instance.InfoManager.Maps.Select(map => (map.Key, map.Value.Item1, map.Value.Item2)))
            {
                maps.Add(new WzDumper.AvailableMap() {
                    mapId = int.Parse(mapId),
                    mapName = $"{mapStreetName} : {mapName}"
                });
            }
            return maps.OrderBy(map => map.mapId).ToList();
        }

        public WzDumper.MapData DumpMap(int mapId)
        {
            var mapData = new WzDumper.MapData();

            var thread = new Thread(() =>
            {
                var mapIdStr = $"{mapId}".PadLeft(9, '0');
                var mapWzFileName = $"Map{mapIdStr[0]}";

                var wzDirectory = WzFileManager.Instance.FindMapWz(mapWzFileName);
                var mapImage = (WzImage)wzDirectory[$"{mapIdStr}.img"];

                if (!mapImage.Parsed)
                    mapImage.ParseImage();

                var mapStringProp = WzInfoTools.GetMapStringProp(mapIdStr);
                var mapName = WzInfoTools.GetMapName(mapStringProp);
                var mapStreetName = WzInfoTools.GetMapStreetName(mapStringProp);
                var mapCategoryName = WzInfoTools.GetMapCategoryName(mapStringProp);

                List<string> copyPropNames = MapLoader.VerifyMapPropsKnown(mapImage, false);

                MapInfo mapInfo = new MapInfo(mapImage, mapName, mapStreetName, mapCategoryName);
                foreach (string copyPropName in copyPropNames)
                {
                    mapInfo.additionalNonInfoProps.Add(mapImage[copyPropName]);
                }
                MapType type = MapLoader.GetMapType(mapImage);
                if (type == MapType.RegularMap)
                    mapInfo.id = int.Parse(WzInfoTools.RemoveLeadingZeros(WzInfoTools.RemoveExtension(mapImage.Name)));
                mapInfo.mapType = type;


                Rectangle mapVR = new Rectangle();
                Point mapCenter = new Point();
                Point mapSize = new Point();
                Point mapMinimapSize = new Point();
                Point mapMinimapCenter = new Point();
                bool mapHasMinimap = false;
                bool mapHasVR = false;

                MapLoader.GetMapDimensions(mapImage, out mapVR, out mapCenter, out mapSize, out mapMinimapCenter, out mapMinimapSize, out mapHasVR, out mapHasMinimap);

                var multiBoard = new MultiBoard();
                var mapBoard = multiBoard.CreateBoard(mapSize, mapCenter, null);
                //Board mapBoard = new Board(size, center, multiBoard, null, ItemTypes.All, ItemTypes.All);

                if (mapHasMinimap)
                {
                    mapBoard.MiniMap = ((WzCanvasProperty)mapImage["miniMap"]["canvas"]).GetLinkedWzCanvasBitmap();
                    System.Drawing.Point mmPos = new System.Drawing.Point(-mapMinimapCenter.X, -mapMinimapCenter.Y);
                    mapBoard.MinimapPosition = mmPos;
                    mapBoard.MinimapRectangle = new MinimapRectangle(mapBoard, new Rectangle(mmPos.X, mmPos.Y, mapMinimapSize.X, mapMinimapSize.Y));
                }

                if (mapHasVR)
                {
                    mapBoard.VRRectangle = new VRRectangle(mapBoard, mapVR);
                }

                MapLoader.LoadLayers(mapImage, mapBoard);
                MapLoader.LoadLife(mapImage, mapBoard);
                MapLoader.LoadFootholds(mapImage, mapBoard);
                MapLoader.GenerateDefaultZms(mapBoard);
                MapLoader.LoadRopes(mapImage, mapBoard);
                MapLoader.LoadChairs(mapImage, mapBoard);
                MapLoader.LoadPortals(mapImage, mapBoard);
                MapLoader.LoadReactors(mapImage, mapBoard);
                MapLoader.LoadToolTips(mapImage, mapBoard);
                MapLoader.LoadBackgrounds(mapImage, mapBoard);
                MapLoader.LoadMisc(mapImage, mapBoard);

                mapBoard.BoardItems.Sort();

                var mapSimulator = new MapSimulator(mapBoard, $"MapDumper: {mapIdStr}");
                mapSimulator.Load();

                mapData = mapSimulator.DumpMap();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            return mapData;
        }

        #region Implementation

        private void InitializeWzFiles(string wzPath)
        {
            WzFileManager.Init(wzPath);

            if (WzFileManager.Instance.HasDataFile) //currently always false
            {
                Console.WriteLine("Initializing Data.wz...");

                WzFileManager.Instance.LoadDataWzFile("data");
                WzFileManager.Instance.ExtractStringWzMaps();
                WzFileManager.Instance.ExtractMobFile();
                WzFileManager.Instance.ExtractNpcFile();
                WzFileManager.Instance.ExtractReactorFile();
                WzFileManager.Instance.ExtractSoundFile("sound");
                WzFileManager.Instance.ExtractMapMarks();
                WzFileManager.Instance.ExtractPortals();
                WzFileManager.Instance.ExtractTileSets();
                WzFileManager.Instance.ExtractObjSets();
                WzFileManager.Instance.ExtractBackgroundSets();
            }
            else
            {
                Console.WriteLine("Initializing String.wz...");

                WzFileManager.Instance.LoadWzFile("string");
                WzFileManager.Instance.ExtractStringWzMaps();

                // Mob WZ
                foreach (string mobWZFile in WzFileManager.MOB_WZ_FILES)
                {
                    Console.WriteLine(string.Format("Initializing {0}.wz...", mobWZFile));

                    WzFileManager.Instance.LoadWzFile(mobWZFile.ToLower());
                }
                WzFileManager.Instance.ExtractMobFile();

                Console.WriteLine("Initializing Npc.wz...");

                WzFileManager.Instance.LoadWzFile("npc");
                WzFileManager.Instance.ExtractNpcFile();

                Console.WriteLine("Initializing Reactor.wz...");

                WzFileManager.Instance.LoadWzFile("reactor");
                WzFileManager.Instance.ExtractReactorFile();

                // Load sound
                foreach (string soundWzFile in WzFileManager.SOUND_WZ_FILES)
                {
                    Console.WriteLine(string.Format("Initializing {0}.wz...", soundWzFile));
    
                    WzFileManager.Instance.LoadWzFile(soundWzFile.ToLower());
                    WzFileManager.Instance.ExtractSoundFile(soundWzFile.ToLower());
                }

                Console.WriteLine("Initializing Map.wz...");

                WzFileManager.Instance.LoadWzFile("map");
                WzFileManager.Instance.ExtractMapMarks();
                WzFileManager.Instance.ExtractPortals();
                WzFileManager.Instance.ExtractTileSets();
                WzFileManager.Instance.ExtractObjSets();
                WzFileManager.Instance.ExtractBackgroundSets();

                foreach (string mapwzFile in WzFileManager.MAP_WZ_FILES)
                {
                    if (WzFileManager.Instance.LoadWzFile(mapwzFile.ToLower()))
                    {
                        Console.WriteLine(string.Format("Initializing {0}.wz...", mapwzFile));
        
                        WzFileManager.Instance.ExtractBackgroundSets();
                        WzFileManager.Instance.ExtractObjSets();
                    }
                }

                Console.WriteLine("Initializing UI.wz...");

                WzFileManager.Instance.LoadWzFile("ui");
            }
        }

        #endregion
    }
}
