#pragma once

#include "Types.hpp"

namespace MapDumperLib
{
    struct Frame
    {
        std::wstring bitmapPath;
        int durationMs;
        Point position;
    };

    struct Tile
    {
        std::list<Frame> frames;
    };

    struct Layer
    {
        std::list<Tile> tiles;
    };

    struct MapData
    {
        Size mapSize;
        std::list<Layer> layers;
    };
} // namespace MapDumperLib
