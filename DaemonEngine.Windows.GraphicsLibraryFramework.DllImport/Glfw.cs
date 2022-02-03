using DaemonEngine.GraphicsLibraryFramework.DllImport.Structs;
using System.Text;

namespace DaemonEngine.GraphicsLibraryFramework.DllImport;

public static class Glfw
{
    #region Mouse callback methods
    public static void SetScrollCallback(ref GlfwNativeWindowHandle glfwNativeWindowHandle, GlfwCallbacks.GLFWscrollfun scrollEvent)
    {
        glfwNativeWindowHandle.ScrollFunc = scrollEvent;
        GlfwDllImport.glfwSetScrollCallback(glfwNativeWindowHandle.WindowHandle, scrollEvent);
    }

    public static void SetCursorPosCallback(ref GlfwNativeWindowHandle glfwNativeWindowHandle, GlfwCallbacks.GLFWcursorposfun mouseMovedEvent)
    {
        glfwNativeWindowHandle.MouseMovedFunc = mouseMovedEvent;
        GlfwDllImport.glfwSetCursorPosCallback(glfwNativeWindowHandle.WindowHandle, mouseMovedEvent);
    }

    public static void SetMouseButtonCallback(ref GlfwNativeWindowHandle glfwNativeWindowHandle, GlfwCallbacks.GLFWmousebuttonfun mouseButtonEvent)
    {
        glfwNativeWindowHandle.MouseButtonFunc = mouseButtonEvent;
        GlfwDllImport.glfwSetMouseButtonCallback(glfwNativeWindowHandle.WindowHandle, mouseButtonEvent);
    }
    #endregion

    #region Key callback methods
    public static void SetCharCallback(ref GlfwNativeWindowHandle glfwNativeWindowHandle, GlfwCallbacks.GLFWcharfun keyTypedEvent)
    {
        glfwNativeWindowHandle.KeyTypedFunc = keyTypedEvent;
        GlfwDllImport.glfwSetCharCallback(glfwNativeWindowHandle.WindowHandle, keyTypedEvent);
    }

    public static void SetKeyCallback(ref GlfwNativeWindowHandle glfwNativeWindowHandle, GlfwCallbacks.GLFWkeyfun keyEvent)
    {
        glfwNativeWindowHandle.KeyEventFunc = keyEvent;
        GlfwDllImport.glfwSetKeyCallback(glfwNativeWindowHandle.WindowHandle, keyEvent);
    }
    #endregion

    #region Window callback methods
    public static void SetWindowResizeCallback(ref GlfwNativeWindowHandle glfwNativeWindowHandle, GlfwCallbacks.GLFWwindowsizefun windowResizeEvent)
    {
        glfwNativeWindowHandle.WindowResizeEventFunc = windowResizeEvent;
        GlfwDllImport.glfwSetWindowSizeCallback(glfwNativeWindowHandle.WindowHandle, windowResizeEvent);
    }

    public static void SetWindowFocusCallback(ref GlfwNativeWindowHandle glfwNativeWindowHandle, GlfwCallbacks.GLFWwindowfocusfun windowFocusEvent)
    {
        glfwNativeWindowHandle.WindowFocusEventFunc = windowFocusEvent;
        GlfwDllImport.glfwSetWindowFocusCallback(glfwNativeWindowHandle.WindowHandle, windowFocusEvent);
    }

    public static void SetWindowCloseCallback(ref GlfwNativeWindowHandle glfwNativeWindowHandle, GlfwCallbacks.GLFWwindowclosefun windowCloseEvent)
    {
        glfwNativeWindowHandle.WindowCloseEventFunc = windowCloseEvent;
        GlfwDllImport.glfwSetWindowCloseCallback(glfwNativeWindowHandle.WindowHandle, windowCloseEvent);
    }
    #endregion

    public static void MaximizeWindow(GlfwNativeWindowHandle glfwNativeWindowHandle)
    {
        GlfwDllImport.glfwMaximizeWindow(glfwNativeWindowHandle.WindowHandle);
    }

    public static void RestoreWindow(GlfwNativeWindowHandle glfwNativeWindowHandle)
    {
        GlfwDllImport.glfwRestoreWindow(glfwNativeWindowHandle.WindowHandle);
    }

    public static void SetInputMode(GlfwNativeWindowHandle glfwNativeWindowHandle, int mode, int value)
    {
        GlfwDllImport.glfwSetInputMode(glfwNativeWindowHandle.WindowHandle, mode, value);
    }

    public static void GetCursorPos(GlfwNativeWindowHandle glfwNativeWindowHandle, ref double xPos, ref double yPos)
    {
        GlfwDllImport.glfwGetCursorPos(glfwNativeWindowHandle.WindowHandle, ref xPos, ref yPos);
    }

    public static void SetWindowShouldClose(GlfwNativeWindowHandle glfwNativeWindowHandle, int value)
    {
        GlfwDllImport.glfwSetWindowShouldClose(glfwNativeWindowHandle.WindowHandle, value);
    }

    public static bool IsMouseButtonPressed(GlfwNativeWindowHandle glfwNativeWindowHandle, int button)
    {
        return GetMouseButton(glfwNativeWindowHandle, button) == GlfwConstants.GLFW_PRESS;
    }

    public static int GetMouseButton(GlfwNativeWindowHandle glfwNativeWindowHandle, int button)
    {
        return GlfwDllImport.glfwGetMouseButton(glfwNativeWindowHandle.WindowHandle, button);   
    }

    public static bool IsKeyPressed(GlfwNativeWindowHandle glfwNativeWindowHandle, int key)
    {
        return GetKey(glfwNativeWindowHandle, key) == GlfwConstants.GLFW_PRESS;
    }

    public static int GetKey(GlfwNativeWindowHandle glfwNativeWindowHandle, int key)
    {
        return GlfwDllImport.glfwGetKey(glfwNativeWindowHandle.WindowHandle, key);
    }

    public static void SetErrorCallback(GlfwCallbacks.GLFWerrorfun callback)
    {
        GlfwDllImport.glfwSetErrorCallback(callback);
    }

    public static void WindowHint(int target, int hint)
    {
        GlfwDllImport.glfwWindowHint(target, hint);
    }

    public static void SwapBuffers(GlfwNativeWindowHandle glfwNativeWindowHandle)
    {
        GlfwDllImport.glfwSwapBuffers(glfwNativeWindowHandle.WindowHandle);
    }

    public static void PollEvents()
    {
        GlfwDllImport.glfwPollEvents();
    }

    public static bool WindowShouldClose(GlfwNativeWindowHandle glfwNativeWindowHandle)
    {
        return GlfwDllImport.glfwWindowShouldClose(glfwNativeWindowHandle.WindowHandle) == GlfwConstants.GLFW_TRUE;
    }

    public static void MakeContextCurrent(GlfwNativeWindowHandle glfwNativeWindowHandle)
    {
        GlfwDllImport.glfwMakeContextCurrent(glfwNativeWindowHandle.WindowHandle);
    }

    public static double GetTime()
    {
        return GlfwDllImport.glfwGetTime();
    }

    public static IntPtr GetPrimaryMonitor()
    {
        return GlfwDllImport.glfwGetPrimaryMonitor();
    }

    public static GlfwNativeWindowHandle CreateWindow(int width, int height, string title, bool fullscreen = false)
    {
        var fullscreenMonitor = fullscreen ? GetPrimaryMonitor() : IntPtr.Zero;

        var titleBytes = Encoding.UTF8.GetBytes(title);
        var windowHandle = GlfwDllImport.glfwCreateWindow(width, height, titleBytes, fullscreenMonitor, IntPtr.Zero);

        return new GlfwNativeWindowHandle
        {
            WindowHandle = windowHandle
        };
    }

    public static void DestroyWindow(GlfwNativeWindowHandle glfwNativeWindowHandle)
    {
        GlfwDllImport.glfwDestroyWindow(glfwNativeWindowHandle.WindowHandle);
    }

    public static void Terminate()
    {
        GlfwDllImport.glfwTerminate();
    }

    public static bool Init()
    {
        return GlfwDllImport.glfwInit() != GlfwConstants.GLFW_FALSE;
    }
}
