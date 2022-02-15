using DaemonEngine.Graphics;

namespace DaemonEngine.ECS.Components;

public class Camera : ComponentBase
{
    public Camera()
    {
        Primary = true;
    }

    public bool Primary { get; set; }
    public ICamera MainCamera { get; set; }
}

