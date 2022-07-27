#pragma once

#include <list>
#include <string>
#include <msclr/auto_handle.h>

namespace WzDumperLib
{
    template <typename TManaged>
    using native_type_t = decltype(to_native(std::declval<TManaged>()));
    //template <typename TNative>
    //using managed_type_t = decltype(to_managed(std::declval<TNative>()));

    std::wstring to_native(System::String^ managed);
    System::String^ to_managed(const std::wstring& native);
    int to_native(int^ managed);
    int^ to_managed(int native);

    template <typename T>
    auto to_native_list(T^ managed)
    {
        using native_t = native_type_t<decltype(managed[0])>;

        std::list<native_t> native;
        for each (auto obj in managed)
        {
            native.push_back(to_native(obj));
        }
        return std::list<native_t> { std::move(native) };
    }

    //template <typename T>
    //auto to_managed_list(const std::list<T>& native)
    //{
    //    auto managed = gcnew System::Collections::Generic::List<managed_type_t<T>>;
    //    for (const auto& obj : native)
    //    {
    //        managed->Add(to_managed(obj));
    //    }
    //    return managed;
    //}

} // namespace MapDumperLib
