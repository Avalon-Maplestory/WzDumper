#include "TypesConverters.hpp"
#include "Converters.hpp"

MapDumperLib::Point MapDumperLib::to_native(MapDumper::Point^ managed)
{
    return {
        to_native(managed->x),
        to_native(managed->y)
    };
}

MapDumperLib::Size MapDumperLib::to_native(MapDumper::Size^ managed)
{
    return {
        to_native(managed->width),
        to_native(managed->height)
    };
}