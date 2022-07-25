#pragma once

#include <filesystem>
#include <memory>
#include "MapData.hpp"

namespace MapDumperLib
{
    // Forward declaration
    class MapDumperProxy;

    class MapDumper
    {
    public:
        MapDumper(std::filesystem::path maplestoryDirectory);
        ~MapDumper();

        [[nodiscard]] MapData dump(int mapId, std::filesystem::path assetsDumpDirectory);

    private:
        MapDumperProxy* m_proxy;
    };
} // namespace MapDumperLib
