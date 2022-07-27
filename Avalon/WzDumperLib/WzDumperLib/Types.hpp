#pragma once

#include <list>
#include <string>

#include "WzDumperLib.hpp"

namespace WzDumperLib
{
    struct WZDUMPERLIB_API Point
    {
        int x;
        int y;
    };

    struct WZDUMPERLIB_API Size
    {
        int width;
        int height;
    };
} // namespace MapDumperLib
