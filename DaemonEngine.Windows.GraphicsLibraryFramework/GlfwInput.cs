using DaemonEngine.GraphicsLibraryFramework.DllImport;
using DaemonEngine.GraphicsLibraryFramework.DllImport.Structs;
using DaemonEngine.Windows.Inputs;
using System.Numerics;

namespace DaemonEngine.Windows.GraphicsLibraryFramework;

internal class GlfwInput : InputBase
{
    private readonly IWindow _window;

    public GlfwInput(IWindow window)
    {
        _window = window;
    }

    public override bool IsButtonDown(int button)
    {
        var glfwNativeWindow = (GlfwNativeWindowHandle)_window.GetNativeWindowHandle();
        return Glfw.IsMouseButtonPressed(glfwNativeWindow, button);
    }

    public override bool IsKeyDown(int keycode)
    {
        var glfwNativeWindow = (GlfwNativeWindowHandle)_window.GetNativeWindowHandle();
        return Glfw.IsKeyPressed(glfwNativeWindow, keycode);
    }

    public override Vector2 GetMousePosition()
    {
        var glfwNativeWindow = (GlfwNativeWindowHandle)_window.GetNativeWindowHandle();
        double mouseX = 0.0, mouseY = 0.0;
        Glfw.GetCursorPos(glfwNativeWindow, ref mouseX, ref mouseY);
        return new Vector2 { X = (float)mouseX, Y = (float)mouseY };
    }
}
