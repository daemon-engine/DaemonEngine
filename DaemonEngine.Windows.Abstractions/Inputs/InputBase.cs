using System.Numerics;

namespace DaemonEngine.Windows.Inputs;

public abstract class InputBase : IInput
{
    public InputBase()
    {
    }

    public abstract bool IsButtonDown(int button);

    public abstract bool IsKeyDown(int keycode);

    public abstract Vector2 GetMousePosition();

    public virtual float GetMousePositionX()
    {
        return GetMousePosition().X;
    }
    public virtual float GetMousePositionY()
    {
        return GetMousePosition().Y;
    }
}
