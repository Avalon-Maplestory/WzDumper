#include "MapDumper.hpp"
#include "MapDumperProxy.hpp"

MapDumperLib::MapDumper::MapDumper(std::filesystem::path maplestoryDirectory) :
    m_proxy(new MapDumperProxy(std::move(maplestoryDirectory)))
{
}

MapDumperLib::MapDumper::~MapDumper()
{
    if (m_proxy != nullptr)
    {
        delete m_proxy;
        m_proxy = nullptr;
    }
}

MapDumperLib::MapData MapDumperLib::MapDumper::dump(int mapId, std::filesystem::path assetsDumpDirectory)
{
    return m_proxy->dump(mapId, std::move(assetsDumpDirectory));
}

std::list<MapDumperLib::AvailableMap> MapDumperLib::MapDumper::getAvailableMaps()
{
    return m_proxy->getAvailableMaps();
}
