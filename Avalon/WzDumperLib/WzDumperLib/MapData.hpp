#pragma once

#include "Types.hpp"

namespace WzDumperLib
{
    struct WZDUMPERLIB_API Frame
    {
        std::wstring bitmapPath;
        int durationMs;
        Point position;
    };

    struct WZDUMPERLIB_API Tile
    {
        std::list<Frame> frames;
    };

    struct WZDUMPERLIB_API Layer
    {
        std::list<Tile> tiles;
    };

    struct WZDUMPERLIB_API MapData
    {
        Size mapSize;
        std::list<Layer> layers;
    };

    struct WZDUMPERLIB_API AvailableMap
    {
        int mapId;
        std::wstring mapName;
    };
} // namespace MapDumperLib
