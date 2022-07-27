#include "MapDumperProxy.hpp"
#include "MapDataConverters.hpp"
#include "TypesConverters.hpp"
#include "Converters.hpp"

MapDumperLib::MapDumperProxy::MapDumperProxy(std::filesystem::path maplestoryDirectory) :
    m_dumper(gcnew MapDumperCS::MapDumperCS(to_managed(maplestoryDirectory.wstring())))
{
}

MapDumperLib::MapData MapDumperLib::MapDumperProxy::dump(int mapId, std::filesystem::path assetsDumpDirectory)
{
    auto map = m_dumper->DumpMap(mapId);
    MapDumperCS::MapDataSaver::SaveMapAssets(map, to_managed(assetsDumpDirectory.wstring()));

    return to_native(map);
}

std::list<MapDumperLib::AvailableMap> MapDumperLib::MapDumperProxy::getAvailableMaps()
{
    return to_native_list(m_dumper->GetAvailableMaps());
}
