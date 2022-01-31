using DaemonEngine.Extensions.Glfw.Structs;
using System.Text;

namespace DaemonEngine.Extensions.Glfw;

public static class Glfw
{
    #region Mouse callback methods
    public static void SetScrollCallback(ref GlfwWindow glfwWindow, GlfwCallbacks.GLFWscrollfun scrollEvent)
    {
        glfwWindow.ScrollFunc = scrollEvent;
        GlfwDllImport.glfwSetScrollCallback(glfwWindow.WindowHandle, scrollEvent);
    }

    public static void SetCursorPosCallback(ref GlfwWindow glfwWindow, GlfwCallbacks.GLFWcursorposfun mouseMovedEvent)
    {
        glfwWindow.MouseMovedFunc = mouseMovedEvent;
        GlfwDllImport.glfwSetCursorPosCallback(glfwWindow.WindowHandle, mouseMovedEvent);
    }

    public static void SetMouseButtonCallback(ref GlfwWindow glfwWindow, GlfwCallbacks.GLFWmousebuttonfun mouseButtonEvent)
    {
        glfwWindow.MouseButtonFunc = mouseButtonEvent;
        GlfwDllImport.glfwSetMouseButtonCallback(glfwWindow.WindowHandle, mouseButtonEvent);
    }
    #endregion

    #region Key callback methods
    public static void SetCharCallback(ref GlfwWindow glfwWindow, GlfwCallbacks.GLFWcharfun keyTypedEvent)
    {
        glfwWindow.KeyTypedFunc = keyTypedEvent;
        GlfwDllImport.glfwSetCharCallback(glfwWindow.WindowHandle, keyTypedEvent);
    }

    public static void SetKeyCallback(ref GlfwWindow glfwWindow, GlfwCallbacks.GLFWkeyfun keyEvent)
    {
        glfwWindow.KeyEventFunc = keyEvent;
        GlfwDllImport.glfwSetKeyCallback(glfwWindow.WindowHandle, keyEvent);
    }
    #endregion

    #region Window callback methods
    public static void SetWindowResizeCallback(ref GlfwWindow glfwWindow, GlfwCallbacks.GLFWwindowsizefun windowResizeEvent)
    {
        glfwWindow.WindowResizeEventFunc = windowResizeEvent;
        GlfwDllImport.glfwSetWindowSizeCallback(glfwWindow.WindowHandle, windowResizeEvent);
    }

    public static void SetWindowFocusCallback(ref GlfwWindow glfwWindow, GlfwCallbacks.GLFWwindowfocusfun windowFocusEvent)
    {
        glfwWindow.WindowFocusEventFunc = windowFocusEvent;
        GlfwDllImport.glfwSetWindowFocusCallback(glfwWindow.WindowHandle, windowFocusEvent);
    }

    public static void SetWindowCloseCallback(ref GlfwWindow glfwWindow, GlfwCallbacks.GLFWwindowclosefun windowCloseEvent)
    {
        glfwWindow.WindowCloseEventFunc = windowCloseEvent;
        GlfwDllImport.glfwSetWindowCloseCallback(glfwWindow.WindowHandle, windowCloseEvent);
    }
    #endregion 

    public static void SetWindowShouldClose(GlfwWindow glfwWindow, int value)
    {
        GlfwDllImport.glfwSetWindowShouldClose(glfwWindow.WindowHandle, value);
    }

    public static bool IsKeyPressed(GlfwWindow glfwWindow, int key)
    {
        return GetKey(glfwWindow, key) == GlfwConstants.GLFW_PRESS;
    }

    public static int GetKey(GlfwWindow glfwWindow, int key)
    {
        return GlfwDllImport.glfwGetKey(glfwWindow.WindowHandle, key);
    }

    public static void SetErrorCallback(GlfwCallbacks.GLFWerrorfun callback)
    {
        GlfwDllImport.glfwSetErrorCallback(callback);
    }

    public static void WindowHint(int target, int hint)
    {
        GlfwDllImport.glfwWindowHint(target, hint);
    }

    public static void SwapBuffers(GlfwWindow glfwWindow)
    {
        GlfwDllImport.glfwSwapBuffers(glfwWindow.WindowHandle);
    }

    public static void PollEvents()
    {
        GlfwDllImport.glfwPollEvents();
    }

    public static bool WindowShouldClose(GlfwWindow glfwWindow)
    {
        return GlfwDllImport.glfwWindowShouldClose(glfwWindow.WindowHandle) == GlfwConstants.GLFW_TRUE;
    }

    public static void MakeContextCurrent(GlfwWindow glfwWindow)
    {
        GlfwDllImport.glfwMakeContextCurrent(glfwWindow.WindowHandle);
    }

    public static double GetTime()
    {
        return GlfwDllImport.glfwGetTime();
    }

    public static GlfwWindow CreateWindow(int width, int height, string title)
    {
        var titleBytes = Encoding.UTF8.GetBytes(title);
        var windowHandle = GlfwDllImport.glfwCreateWindow(width, height, titleBytes, IntPtr.Zero, IntPtr.Zero);

        return new GlfwWindow
        {
            WindowHandle = windowHandle
        };
    }

    public static void DestroyWindow(GlfwWindow glfwWindow)
    {
        GlfwDllImport.glfwDestroyWindow(glfwWindow.WindowHandle);
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
