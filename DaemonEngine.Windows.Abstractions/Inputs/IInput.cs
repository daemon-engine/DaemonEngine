using System.Numerics;

namespace DaemonEngine.Windows.Inputs;

public interface IInput
{
    bool IsKeyDown(Keycode keycode);
    bool IsButtonDown(MouseButton button);

    float GetMousePositionX();
    float GetMousePositionY();
    Vector2 GetMousePosition();
}
