using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;

namespace DaemonEngine.ECS.Components;

public sealed class MeshRenderer : ComponentBase
{
    public MeshRenderer()
    {
    }

    public override string Name => nameof(MeshRenderer);

    public Model Model { get; set; }
    public IShader Shader { get; set; }

    // TODO: Material stuff - will be moved into material
    public Vector3 Ambient { get; set; }
    public Vector3 Diffuse { get; set; }
    public Vector3 Specular { get; set; }
    public float Shininess { get; set; }
}
