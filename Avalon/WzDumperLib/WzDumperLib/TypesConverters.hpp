#pragma once

#include "Types.hpp"

namespace WzDumperLib
{
    Point to_native(::WzDumper::Point^ managed);
    Size to_native(::WzDumper::Size^ managed);
} // namespace MapDumperLib
