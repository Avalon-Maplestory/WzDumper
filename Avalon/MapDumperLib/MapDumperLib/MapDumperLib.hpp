#pragma once

#ifdef MAPDUMPERLIB_EXPORTS
#define MAPDUMPERLIB_API __declspec(dllexport)
#else
#define MAPDUMPERLIB_API __declspec(dllimport)
#endif
