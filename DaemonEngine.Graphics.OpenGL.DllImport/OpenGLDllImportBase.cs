using System.Runtime.InteropServices;

namespace DaemonEngine.OpenGL.DllImport;

internal class OpenGLDllImportBase
{
    public const string LIB_OPENGL_LIBRARY = "opengl32.dll";

    [DllImport(LIB_OPENGL_LIBRARY, SetLastError = true)]
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
