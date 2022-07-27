#pragma once

#include <filesystem>
#include <memory>
#include "MapData.hpp"
#include "MapDumperLib.hpp"

namespace MapDumperLib
{
    // Forward declaration
    class MapDumperProxy;

    class MAPDUMPERLIB_API MapDumper
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
