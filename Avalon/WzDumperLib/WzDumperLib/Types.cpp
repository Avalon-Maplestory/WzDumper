#include "TypesConverters.hpp"
#include "Converters.hpp"

WzDumperLib::Point WzDumperLib::to_native(::WzDumper::Point ^ managed)
{
    return {
        to_native(managed->x),
        to_native(managed->y)
    };
}

WzDumperLib::Size WzDumperLib::to_native(::WzDumper::Size ^ managed)
{
    return {
        to_native(managed->width),
        to_native(managed->height)
    };
}