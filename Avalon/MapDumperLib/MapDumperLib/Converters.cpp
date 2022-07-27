#include "Converters.hpp"

#include <msclr\marshal_cppstd.h>

std::wstring MapDumperLib::to_native(System::String ^ managed)
{
    return msclr::interop::marshal_as<std::wstring>(managed);
}

System::String ^ MapDumperLib::to_managed(const std::wstring& native)
{
    return msclr::interop::marshal_as<System::String ^>(native);
}

int MapDumperLib::to_native(int^ managed)
{
    return *managed;
}

int^ MapDumperLib::to_managed(int native)
{
    return native;
}
