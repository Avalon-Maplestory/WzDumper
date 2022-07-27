#pragma once

#ifdef WZDUMPERLIB_EXPORTS
#define WZDUMPERLIB_API __declspec(dllexport)
#else
#define WZDUMPERLIB_API __declspec(dllimport)
#endif
