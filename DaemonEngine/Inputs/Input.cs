using DaemonEngine.Extensions.Glfw;
using DaemonEngine.Extensions.Glfw.Structs;
using DaemonEngine.Windows;
using System.Numerics;

namespace DaemonEngine.Inputs;

public interface IInput
{
    Vector2 GetMousePosition();
    bool IsKeyPressed(int keycode);
}

public class Input : IInput
{
    public Input(IWindow window)
    {
        Window = window;
    }

    protected IWindow Window { get; }

    public Vector2 GetMousePosition()
    {
        var xPos = (double)0.0f;
        var yPos = (double)0.0f;
        Glfw.GetCursorPos((GlfwWindow)Window.GetNativeWindowHandle(), ref xPos, ref yPos);
        return new Vector2 ((float)xPos, (float)yPos);
    }

    public bool IsKeyPressed(int keycode)
    {
        return Glfw.IsKeyPressed((GlfwWindow)Window.GetNativeWindowHandle(), keycode);
    }
}
