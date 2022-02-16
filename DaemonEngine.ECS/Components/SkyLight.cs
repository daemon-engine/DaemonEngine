using DaemonEngine.Mathematics;

namespace DaemonEngine.ECS.Components;

public class SkyLight : ComponentBase
{
    public SkyLight()
    {
    }

    public override string Name => nameof(SkyLight);

    public Vector3 Direction { get; set; }
    public Vector3 Ambient { get; set; }
    public Vector3 Diffuse { get; set; }
    public Vector3 Specular { get; set; }
}
