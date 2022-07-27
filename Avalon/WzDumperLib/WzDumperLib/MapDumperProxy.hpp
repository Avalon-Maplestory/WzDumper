#pragma once

#include <filesystem>
#include <msclr/auto_gcroot.h>
#include "MapData.hpp"

namespace WzDumperLib
{
    class MapDumperProxy
    {
    public:
        MapDumperProxy(std::filesystem::path maplestoryDirectory);

        [[nodiscard]] MapData dump(int mapId, std::filesystem::path assetsDumpDirectory);
        [[nodiscard]] std::list<AvailableMap> getAvailableMaps();

    private:
        msclr::auto_gcroot<::WzDumperCS::MapDumperCS ^> m_dumper;
    };
} // namespace MapDumperLib