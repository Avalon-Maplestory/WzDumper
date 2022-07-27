#include "MapDumper.hpp"
#include "MapDumperProxy.hpp"

WzDumperLib::MapDumper::MapDumper(std::filesystem::path maplestoryDirectory) :
    m_proxy(new MapDumperProxy(std::move(maplestoryDirectory)))
{
}

WzDumperLib::MapDumper::~MapDumper()
{
    if (m_proxy != nullptr)
    {
        delete m_proxy;
        m_proxy = nullptr;
    }
}

WzDumperLib::MapData WzDumperLib::MapDumper::dump(int mapId, std::filesystem::path assetsDumpDirectory)
{
    return m_proxy->dump(mapId, std::move(assetsDumpDirectory));
}

std::list<WzDumperLib::AvailableMap> WzDumperLib::MapDumper::getAvailableMaps()
{
    return m_proxy->getAvailableMaps();
}
