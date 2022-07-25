#pragma once

#include "Types.hpp"

namespace MapDumperLib
{
    Point to_native(MapDumper::Point^ managed);
    Size to_native(MapDumper::Size^ managed);
} // namespace MapDumperLib
