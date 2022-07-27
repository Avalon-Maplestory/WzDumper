#pragma once

#include <filesystem>
#include <memory>
#include "MapData.hpp"
#include "WzDumperLib.hpp"

namespace WzDumperLib
{
    // Forward declaration
    class MapDumperProxy;

    class WZDUMPERLIB_API MapDumper
    {
    public:
        MapDumper(std::filesystem::path maplestoryDirectory);
        ~MapDumper();

        [[nodiscard]] MapData dump(int mapId, std::filesystem::path assetsDumpDirectory);
        [[nodiscard]] std::list<AvailableMap> getAvailableMaps();

    private:
        MapDumperProxy* m_proxy;
    };
} // namespace MapDumperLib
