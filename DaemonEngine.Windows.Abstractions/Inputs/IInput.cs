using System.Numerics;

namespace DaemonEngine.Windows.Inputs;

public interface IInput
{
    bool IsKeyDown(int keycode);
    bool IsButtonDown(int button);

    float GetMousePositionX();
    float GetMousePositionY();
    Vector2 GetMousePosition();
}
