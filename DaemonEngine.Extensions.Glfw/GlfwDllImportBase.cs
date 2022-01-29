using System.Runtime.InteropServices;

namespace DaemonEngine.Extensions.Glfw;

internal class GlfwDllImportBase
{
    public const string LIB_GLFW_LIBRARY = "Binary/glfw3.dll";

    [DllImport(LIB_GLFW_LIBRARY, SetLastError = true)]
    private static extern IntPtr wglGetProcAddress(string name);

    internal static T? GetDelegateFor<T>() where T : class
    {
        Type delegateType = typeof(T);
        string name = delegateType.Name.Replace("Delegate", "");
        IntPtr proc = wglGetProcAddress(name);
        Delegate del = Marshal.GetDelegateForFunctionPointer(proc, delegateType);

        return del as T;
    }

    internal static T? GetDelegateFor<T>(string name) where T : class
    {
        Type delegateType = typeof(T);
        IntPtr proc = wglGetProcAddress(name);
        Delegate del = Marshal.GetDelegateForFunctionPointer(proc, delegateType);

        return del as T;
    }
}
