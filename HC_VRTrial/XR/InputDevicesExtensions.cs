using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;

namespace UnityEngine.XR;

public class InputDeviceList : Il2CppSystem.Object
{
    private static readonly System.IntPtr NativeMethodInfoPtr_get_Count_Public_Virtual_Final_New_get_Int32_0;
    private static readonly System.IntPtr NativeMethodInfoPtr_get_Item_Public_Virtual_Final_New_get_T_Int32_0;

    static InputDeviceList()
    {
        Il2CppClassPointerStore<List<Il2CppSystem.Object>>.NativeClassPtr = IL2CPP.il2cpp_class_from_type(Type.internal_from_handle(IL2CPP.il2cpp_class_get_type(IL2CPP.GetIl2CppClass("mscorlib.dll", "System.Collections.Generic", "List`1"))).MakeGenericType(new Il2CppReferenceArray<Type>(new Type[1] { Type.internal_from_handle(IL2CPP.il2cpp_class_get_type(Il2CppClassPointerStore<Il2CppSystem.Object>.NativeClassPtr)) })).TypeHandle.value);
        IL2CPP.il2cpp_runtime_class_init(Il2CppClassPointerStore<List<Il2CppSystem.Object>>.NativeClassPtr);
        NativeMethodInfoPtr_get_Count_Public_Virtual_Final_New_get_Int32_0 = IL2CPP.GetIl2CppMethodByToken(Il2CppClassPointerStore<List<Il2CppSystem.Object>>.NativeClassPtr, 100674641);
        NativeMethodInfoPtr_get_Item_Public_Virtual_Final_New_get_T_Int32_0 = IL2CPP.GetIl2CppMethodByToken(Il2CppClassPointerStore<List<Il2CppSystem.Object>>.NativeClassPtr, 100674647);
    }

    public unsafe virtual int Count
    {
        get
        {
            IL2CPP.Il2CppObjectBaseToPtrNotNull(this);
            System.IntPtr* param = null;
            System.Runtime.CompilerServices.Unsafe.SkipInit(out System.IntPtr exc);
            System.IntPtr obj = IL2CPP.il2cpp_runtime_invoke(NativeMethodInfoPtr_get_Count_Public_Virtual_Final_New_get_Int32_0, IL2CPP.Il2CppObjectBaseToPtrNotNull(this), (void**)param, ref exc);
            Il2CppException.RaiseExceptionIfNecessary(exc);
            return *(int*)IL2CPP.il2cpp_object_unbox(obj);
        }
    }

    public unsafe virtual InputDevice this[int index]
    {
        get
        {
            // List<T> インスタンスのポインタを取得
            IL2CPP.Il2CppObjectBaseToPtrNotNull((Il2CppObjectBase)(object)this);

            // インデックスのポインタをスタックに割り当て
            System.IntPtr* ptr = stackalloc System.IntPtr[1];
            *ptr = (nint)(&index);

            // メソッド呼び出しのための変数を宣言
            System.IntPtr intPtr = IntPtr.Zero;
            System.IntPtr intPtr2 = IL2CPP.il2cpp_runtime_invoke(
                NativeMethodInfoPtr_get_Item_Public_Virtual_Final_New_get_T_Int32_0,
                IL2CPP.Il2CppObjectBaseToPtrNotNull((Il2CppObjectBase)(object)this),
                (void**)ptr,
                ref intPtr
            );

            // 例外が発生していれば、それを投げる
            Il2CppException.RaiseExceptionIfNecessary(intPtr);

            // ポインタから値型をアンボックス化して取得
            return *(InputDevice*)IL2CPP.il2cpp_object_unbox(intPtr2);
        }
    }
}

public class InputDevicesExtensions : Il2CppSystem.Object
{
    private delegate void GetDevices_InternalDelegate(System.IntPtr inputDevices);

    private static readonly GetDevices_InternalDelegate GetDevices_InternalDelegateField;

    static InputDevicesExtensions()
    {
        GetDevices_InternalDelegateField = IL2CPP.ResolveICall<GetDevices_InternalDelegate>("UnityEngine.XR.InputDevices::GetDevices_Internal");
    }

    public static InputDeviceList GetDevices()
    {
        var result = new InputDeviceList();
        GetDevices_InternalDelegateField(IL2CPP.Il2CppObjectBaseToPtr(result));
        return result;
    }
}
