namespace DaemonEngine.GraphicsLibraryFramework.DllImport.Structs;

public struct GlfwNativeWindowHandle
{
    public IntPtr WindowHandle { get; set; }

    #region Window callbacks
    public GlfwCallbacks.GLFWwindowclosefun WindowCloseEventFunc { get; set; }
    public GlfwCallbacks.GLFWwindowfocusfun WindowFocusEventFunc { get; set; }
    public GlfwCallbacks.GLFWwindowsizefun WindowResizeEventFunc { get; set; }
    #endregion

    #region Key callbacks
    public GlfwCallbacks.GLFWkeyfun KeyEventFunc { get; set; }
    public GlfwCallbacks.GLFWcharfun KeyTypedFunc { get; set; }
    #endregion

    #region Mouse callbacks
    public GlfwCallbacks.GLFWscrollfun ScrollFunc { get; set; }
    public GlfwCallbacks.GLFWcursorposfun MouseMovedFunc { get; set; }
    public GlfwCallbacks.GLFWmousebuttonfun MouseButtonFunc { get; set; }
    #endregion

}