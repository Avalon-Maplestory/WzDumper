#pragma once

#include <filesystem>
#include <msclr/auto_gcroot.h>
#include "MapData.hpp"

namespace MapDumperLib
{
    class MapDumperProxy
    {
    public:
        MapDumperProxy(std::filesystem::path maplestoryDirectory);

        [[nodiscard]] MapData dump(int mapId, std::filesystem::path assetsDumpDirectory);

    private:
        msclr::auto_gcroot<MapDumperCS::MapDumperCS^> m_dumper;
    };
} // namespace MapDumperLib