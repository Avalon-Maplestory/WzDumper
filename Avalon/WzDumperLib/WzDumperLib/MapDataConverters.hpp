#pragma once

#include "MapData.hpp"

namespace WzDumperLib
{
    Frame to_native(::WzDumper::Frame^ managed);
    Tile to_native(::WzDumper::Tile ^ managed);
    Layer to_native(::WzDumper::Layer ^ managed);
    MapData to_native(::WzDumper::MapData ^ managed);
    AvailableMap to_native(::WzDumper::AvailableMap ^ managed);
} // namespace MapDumperLib
