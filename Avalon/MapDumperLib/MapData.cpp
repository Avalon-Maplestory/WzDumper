#include "MapDataConverters.hpp"
#include "TypesConverters.hpp"
#include "Converters.hpp"

MapDumperLib::Frame MapDumperLib::to_native(::MapDumper::Frame^ managed)
{
    return {
        to_native(managed->bitmapPath),
        to_native(managed->durationMs),
        to_native(managed->position)
    };
}

MapDumperLib::Tile MapDumperLib::to_native(::MapDumper::Tile ^ managed)
{
    return {
        to_native_list(managed->frames)
    };
}

MapDumperLib::Layer MapDumperLib::to_native(::MapDumper::Layer ^ managed)
{
    return {
        to_native_list(managed->tiles)
    };
}

MapDumperLib::MapData MapDumperLib::to_native(::MapDumper::MapData ^ managed)
{
    return {
        to_native(managed->mapSize),
        to_native_list(managed->layers)
    };
}
