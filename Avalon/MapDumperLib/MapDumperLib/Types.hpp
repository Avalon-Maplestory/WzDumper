#pragma once

#include <list>
#include <string>

#include "MapDumperLib.hpp"

namespace MapDumperLib
{
    struct MAPDUMPERLIB_API Point
    {
        int x;
        int y;
    };

    struct MAPDUMPERLIB_API Size
    {
        int width;
        int height;
    };
} // namespace MapDumperLib
