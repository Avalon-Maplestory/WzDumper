
    #include <MapDumper.hpp>

    int main()
    {
        MapDumperLib::MapDumper dumper(R"(C:\Maplestory\MaplestoryV232.2)");
        auto maple_hill = dumper.dump(10000, R"(C:\Maplestory\AssetDump)");
        return 0;
    }
