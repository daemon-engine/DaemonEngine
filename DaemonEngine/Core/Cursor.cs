using DaemonEngine.GraphicsLibraryFramework.DllImport;
using DaemonEngine.Windows;

namespace DaemonEngine.Core;

public enum CursorMode
{
    Normal = 0,
    Disabled,
    Hidden
}

public interface ICursor
{
    void Set(CursorMode mode);

    void Disable();
    void Hide();
    void Show();
}

internal class Cursor : ICursor
{
    private readonly IWindow _window;

    public Cursor(IWindow window)
    {
        _window = window;
    }

    public void Disable()
    {
        Set(CursorMode.Disabled);
    }

    public void Hide()
    {
        Set(CursorMode.Hidden);
    }

    public void Show()
    {
        Set(CursorMode.Normal);
    }

    public void Set(CursorMode cursorMode)
    {
        var value = 0;
        value = cursorMode switch
        {
            CursorMode.Disabled => GlfwConstants.GLFW_CURSOR_DISABLED,
            CursorMode.Hidden => GlfwConstants.GLFW_CURSOR_HIDDEN,
            _ => GlfwConstants.GLFW_CURSOR_NORMAL,
        };
        var glfwWindow = (GraphicsLibraryFramework.DllImport.Structs.GlfwWindow)_window.GetNativeWindowHandle();
        Glfw.SetInputMode(glfwWindow, GlfwConstants.GLFW_CURSOR, value);
    }
}
