#pragma once

#include "MapData.hpp"

namespace MapDumperLib
{
    Frame to_native(::MapDumper::Frame^ managed);
    Tile to_native(::MapDumper::Tile^ managed);
    Layer to_native(::MapDumper::Layer^ managed);
    MapData to_native(::MapDumper::MapData^ managed);
} // namespace MapDumperLib
