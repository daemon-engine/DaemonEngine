using DaemonEngine.Windows.Inputs;
using System.Numerics;

namespace DaemonEngine.Core.Inputs;

public sealed class Input
{
    private static Input s_Instance;

    private IInput _inputImpl;

    public Input(IInput input)
    {
        s_Instance = this;
        _inputImpl = input;
    }

    public static bool IsKeyPressed(Keycode keycode)
    {
        return s_Instance._inputImpl.IsKeyDown((int)keycode);
    }

    public static bool IsMouseButtonPressed(MouseButton mouseButton)
    {
        return s_Instance._inputImpl.IsButtonDown((int)mouseButton);
    }

    public static Vector2 GetMousePosition()
    {
        return s_Instance._inputImpl.GetMousePosition();
    }
}
