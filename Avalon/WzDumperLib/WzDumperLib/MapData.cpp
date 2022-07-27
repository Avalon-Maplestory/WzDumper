#include "MapDataConverters.hpp"
#include "TypesConverters.hpp"
#include "Converters.hpp"

WzDumperLib::Frame WzDumperLib::to_native(::WzDumper::Frame ^ managed)
{
    return {
        to_native(managed->bitmapPath),
        to_native(managed->durationMs),
        to_native(managed->position)
    };
}

WzDumperLib::Tile WzDumperLib::to_native(::WzDumper::Tile ^ managed)
{
    return {
        to_native_list(managed->frames)
    };
}

WzDumperLib::Layer WzDumperLib::to_native(::WzDumper::Layer ^ managed)
{
    return {
        to_native_list(managed->tiles)
    };
}

WzDumperLib::MapData WzDumperLib::to_native(::WzDumper::MapData ^ managed)
{
    return {
        to_native(managed->mapSize),
        to_native_list(managed->layers)
    };
}

WzDumperLib::AvailableMap WzDumperLib::to_native(::WzDumper::AvailableMap ^ managed)
{
    return {
        to_native(managed->mapId),
        to_native(managed->mapName)
    };
}
