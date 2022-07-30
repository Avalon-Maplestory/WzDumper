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
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static HaCreator.MapSimulator.MapSimulator;

namespace WzDumper.Map
{
    public static class MapDumper
    {
        public static List<WzData.AvailableMap> GetAvailableMaps(this WzDumper _, string query, bool verifyExists = false, int count = -1, int offset = 0)
        {
            var maps = new List<WzData.AvailableMap>();
            foreach (var (mapId, mapStreetName, mapName) in WzFileManager.Instance.InfoManager.Maps.Select(map => (map.Key, map.Value.Item1, map.Value.Item2)))
            {
                string mapIdStr = $"{mapId}".PadLeft(9, '0');
                if (!Regex.IsMatch(mapIdStr, query)
                    && !Regex.IsMatch($"{mapId}", query)
                    && !Regex.IsMatch(mapName, query)
                    && !Regex.IsMatch(mapStreetName, query)
                    && !Regex.IsMatch($"{mapStreetName} : {mapName}", query)
                    && !Regex.IsMatch($"{mapId} - {mapStreetName} : {mapName}", query)
                    && !Regex.IsMatch($"{mapIdStr} - {mapStreetName} : {mapName}", query))
                {
                    continue;
                }
                
                if (verifyExists)
                {
                    try
                    {
                        GetMapImage(int.Parse(mapId));
                    }
                    catch
                    {
                        continue;
                    }
                }

                if (offset != 0)
                {
                    --offset;
                    continue;
                }

                if (maps.Count == count)
                    break;
                
                maps.Add(new WzData.AvailableMap() {
                    mapId = int.Parse(mapId),
                    mapStreetName = mapStreetName,
                    mapName = mapName
                });
            }
            return maps.OrderBy(map => map.mapId).ToList();
        }

        public static (WzData.MapData, WzData.Assets) DumpMap(this WzDumper _, int mapId)
        {
            var (mapData, assets) = (new WzData.MapData(), new WzData.Assets());

            Exception mapException = null;
            var thread = new Thread(() =>
            {
                WzImage mapImage;
                try
                { 
                    mapImage = GetMapImage(mapId);
                    if (!mapImage.Parsed)
                        mapImage.ParseImage();
                }
                catch (Exception ex)
                {
                    mapException = ex;
                    return;
                }

                var mapIdStr = $"{mapId}".PadLeft(9, '0');
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
                mapSimulator.Load(true);

                (mapData, assets) = mapSimulator.DumpMap();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            if (mapException != null)
            {
                throw mapException;
            }

            return (mapData, assets);
        }

        private static WzImage GetMapImage(int mapId)
        {
            var mapIdStr = $"{mapId}".PadLeft(9, '0');
            var mapWzFileName = $"Map{mapIdStr[0]}";
            var wzDirectory = WzFileManager.Instance.FindMapWz(mapWzFileName);
            var mapImage = (WzImage)wzDirectory[$"{mapIdStr}.img"];
            
            if (mapImage == null)
            {
                throw new KeyNotFoundException($"map {mapId} does not exist");
            }

            return mapImage;
        }
    }
}
