#pragma once

#include "Types.hpp"

namespace MapDumperLib
{
    struct MAPDUMPERLIB_API Frame
    {
        std::wstring bitmapPath;
        int durationMs;
        Point position;
    };

    struct MAPDUMPERLIB_API Tile
    {
        std::list<Frame> frames;
    };

    struct MAPDUMPERLIB_API Layer
    {
        std::list<Tile> tiles;
    };

    struct MAPDUMPERLIB_API MapData
    {
        Size mapSize;
        std::list<Layer> layers;
    };

    struct MAPDUMPERLIB_API AvailableMap
    {
        int mapId;
        std::wstring mapName;
    };
} // namespace MapDumperLib
