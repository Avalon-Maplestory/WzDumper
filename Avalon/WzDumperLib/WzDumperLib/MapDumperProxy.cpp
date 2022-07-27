#include "MapDumperProxy.hpp"
#include "MapDataConverters.hpp"
#include "TypesConverters.hpp"
#include "Converters.hpp"

WzDumperLib::MapDumperProxy::MapDumperProxy(std::filesystem::path maplestoryDirectory) :
    m_dumper(gcnew ::WzDumperCS::MapDumperCS(to_managed(maplestoryDirectory.wstring())))
{
}

WzDumperLib::MapData WzDumperLib::MapDumperProxy::dump(int mapId, std::filesystem::path assetsDumpDirectory)
{
    auto map = m_dumper->DumpMap(mapId);
    ::WzDumperCS::MapDataSaver::SaveMapAssets(map, to_managed(assetsDumpDirectory.wstring()));

    return to_native(map);
}

std::list<WzDumperLib::AvailableMap> WzDumperLib::MapDumperProxy::getAvailableMaps()
{
    return to_native_list(m_dumper->GetAvailableMaps());
}
