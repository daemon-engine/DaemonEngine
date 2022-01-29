using System.Runtime.InteropServices;

namespace DaemonEngine.Extensions.Glfw;

internal unsafe static class GlfwDllImport
{
    #region Structs

    public struct GLFWImage
    {
        public int width;
        public int height;
        public IntPtr pixels;
    }

    #endregion

    #region Commands

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr glfwGetWindowUserPointer(IntPtr window);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwGetVersion(ref int major, ref int minor, ref int rev);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwGetMonitorPos(IntPtr monitor, ref int xpos, ref int ypos);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwGetMonitorPhysicalSize(IntPtr monitor, ref int widthMM, ref int heightMM);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwGetWindowPos(IntPtr window, ref int xpos, ref int ypos);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwGetWindowSize(IntPtr window, ref int width, ref int height);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwGetFramebufferSize(IntPtr window, ref int width, ref int height);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwGetWindowFrameSize(IntPtr window, ref int left, ref int top, ref int right, ref int bottom);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwGetCursorPos(IntPtr window, ref double xpos, ref double ypos);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSetGamma(IntPtr monitor, float gamma);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSetGammaRamp(IntPtr monitor, long ramp);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSetWindowShouldClose(IntPtr window, int value);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSetWindowTitle(IntPtr window, string title);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSetWindowPos(IntPtr window, int xpos, int ypos);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSetWindowSize(IntPtr window, int width, int height);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSetWindowUserPointer(IntPtr window, IntPtr pointer);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSetCursorPos(IntPtr window, double xpos, double ypos);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSetCursor(IntPtr window, long cursor);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSetClipboardString(IntPtr window, string @string);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSetTime(double time);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSetInputMode(IntPtr window, int mode, int value);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwTerminate();
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwDefaultWindowHints();
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwWindowHint(int target, int hint);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwDestroyWindow(IntPtr window);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwIconifyWindow(IntPtr window);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwRestoreWindow(IntPtr window);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwShowWindow(IntPtr window);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwHideWindow(IntPtr window);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwPollEvents();
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwWaitEvents();
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwPostEmptyEvent();
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwDestroyCursor(long cursor);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwMakeContextCurrent(IntPtr window);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSwapBuffers(IntPtr window);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSwapInterval(int interval);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern int glfwInit();
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern int glfwWindowShouldClose(IntPtr window);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern int glfwGetWindowAttrib(IntPtr window, int attrib);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern int glfwGetInputMode(IntPtr window, int mode);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern int glfwGetKey(IntPtr window, int key);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern int glfwGetMouseButton(IntPtr window, int button);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern int glfwJoystickPresent(int joy);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern int glfwExtensionSupported(string extension);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr glfwGetJoystickButtons(int joy, out int count);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern string glfwGetVersionString();
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern string glfwGetMonitorName(IntPtr monitor);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern string glfwGetJoystickName(int joy);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern string glfwGetClipboardString(IntPtr window);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern long[] glfwGetVideoModes(IntPtr monitor, ref int count);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern long[] glfwGetMonitors(int count);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern long glfwGetVideoMode(IntPtr monitor);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern long glfwGetGammaRamp(IntPtr monitor);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr glfwGetPrimaryMonitor();
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr glfwCreateWindow(int width, int height, byte[] title, IntPtr monitor, IntPtr share);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr glfwGetWindowMonitor(IntPtr window);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern long glfwCreateCursor(long image, int xhot, int yhot);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern long glfwCreateStandardCursor(int shape);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern long glfwGetCurrentContext();
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern long glfwGetProcAddress(string procname);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr glfwGetJoystickAxes(int joy, out int count);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr glfwGetJoystickHats(int joy, out int count);
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern double glfwGetTime();
    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern void glfwSetWindowIcon(IntPtr window, int count, GLFWImage[] images);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWerrorfun glfwSetErrorCallback(GlfwCallbacks.GLFWerrorfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWmonitorfun glfwSetMonitorCallback(GlfwCallbacks.GLFWmonitorfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWwindowposfun glfwSetWindowPosCallback(IntPtr window, GlfwCallbacks.GLFWwindowposfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWwindowsizefun glfwSetWindowSizeCallback(IntPtr window, GlfwCallbacks.GLFWwindowsizefun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWwindowclosefun glfwSetWindowCloseCallback(IntPtr window, GlfwCallbacks.GLFWwindowclosefun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWwindowrefreshfun glfwSetWindowRefreshCallback(IntPtr window, GlfwCallbacks.GLFWwindowrefreshfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWwindowfocusfun glfwSetWindowFocusCallback(IntPtr window, GlfwCallbacks.GLFWwindowfocusfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWwindowiconifyfun glfwSetWindowIconifyCallback(IntPtr window, GlfwCallbacks.GLFWwindowiconifyfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWframebuffersizefun glfwSetFramebufferSizeCallback(IntPtr window, GlfwCallbacks.GLFWframebuffersizefun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWkeyfun glfwSetKeyCallback(IntPtr window, GlfwCallbacks.GLFWkeyfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWcharfun glfwSetCharCallback(IntPtr window, GlfwCallbacks.GLFWcharfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWcharmodsfun glfwSetCharModsCallback(IntPtr window, GlfwCallbacks.GLFWcharmodsfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWmousebuttonfun glfwSetMouseButtonCallback(IntPtr window, GlfwCallbacks.GLFWmousebuttonfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWcursorposfun glfwSetCursorPosCallback(IntPtr window, GlfwCallbacks.GLFWcursorposfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWcursorenterfun glfwSetCursorEnterCallback(IntPtr window, GlfwCallbacks.GLFWcursorenterfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWscrollfun glfwSetScrollCallback(IntPtr window, GlfwCallbacks.GLFWscrollfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWdropfun glfwSetDropCallback(IntPtr window, GlfwCallbacks.GLFWdropfun cbfun);

    [DllImport(GlfwDllImportBase.LIB_GLFW_LIBRARY, CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwCallbacks.GLFWjoystickfun glfwSetJoystickCallback(GlfwCallbacks.GLFWjoystickfun cbfun);

    #endregion
}
