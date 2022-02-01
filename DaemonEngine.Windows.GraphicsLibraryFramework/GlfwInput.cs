using DaemonEngine.Extensions.Glfw;
using DaemonEngine.Windows.Inputs;
using System.Numerics;

namespace DaemonEngine.Windows.GraphicsLibraryFramework;

internal class GlfwInput : IInput
{
    private readonly IWindow _window;

    public GlfwInput(IWindow window)
    {
        _window = window;
    }

    public bool IsButtonDown(MouseButton button)
    {
        var glfwNativeWindow = (Extensions.Glfw.Structs.GlfwWindow)_window.GetNativeWindowHandle();
        return Glfw.IsMouseButtonPressed(glfwNativeWindow, (int)button);
    }

    public bool IsKeyDown(Keycode keycode)
    {
        var glfwNativeWindow = (Extensions.Glfw.Structs.GlfwWindow)_window.GetNativeWindowHandle();
        return Glfw.IsKeyPressed(glfwNativeWindow, (int)keycode);
    }

    public float GetMousePositionX()
    {
        return GetMousePosition().X;
    }

    public float GetMousePositionY()
    {
        return GetMousePosition().Y;
    }

    public Vector2 GetMousePosition()
    {
        var glfwNativeWindow = (Extensions.Glfw.Structs.GlfwWindow)_window.GetNativeWindowHandle();
        double mouseX = 0.0, mouseY = 0.0;
        Glfw.GetCursorPos(glfwNativeWindow, ref mouseX, ref mouseY);
        return new Vector2 { X = (float)mouseX, Y = (float)mouseY };
    }
}
