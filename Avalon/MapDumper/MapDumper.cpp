#include "pch.h"

using namespace System;

int main(array<System::String ^> ^args)
{
    args = gcnew array<System::String^>{ R"(C:\Maplestory\MaplestoryV232.2)" };
    
    auto dumper = gcnew MapDumperCS::MapDumper(args[0]);
    auto maple_hill = dumper->DumpMap(10000); // Maple Hill
    auto henesys = dumper->DumpMap(100000000); // Henesys

    //MapDumperCS::MapDataPreview::ShowMapPreview(maple_hill);
    //MapDumperCS::MapDataPreview::ShowMapPreview(henesys);

    MapDumperCS::MapDataSaver::SaveMapAssets(maple_hill, R"(C:\Maplestory\AssetDump)");
    Console::WriteLine(MapDumperCS::MapDataSaver::ToJson(maple_hill));

    return 0;
}
