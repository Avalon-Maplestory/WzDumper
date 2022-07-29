using HaCreator.Wz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WzDumper
{
    public class WzDumper
    {
        private static WzDumper _instance = null;
        public static WzDumper Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("WzDumper was not initialized");
                }
                return _instance;
            }
        }

        public static void Initialize(string maplestoryDirectory)
        {
            if (_instance != null)
            {
                throw new InvalidOperationException("WzDumper was already initialized");
            }

            _instance = new WzDumper(maplestoryDirectory);
        }

        public static void Uninitialize(string maplestoryDirectory)
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("WzDumper was not initialized");
            }

            _instance = null;
        }

        private WzDumper(string maplestoryDirectory)
        {
            WzFileManager.Initialize(maplestoryDirectory);
            InitializeWzFiles();
        }

        ~WzDumper()
        {
            WzFileManager.Uninitialize();
        }

        private void InitializeWzFiles()
        {
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
    }
}
