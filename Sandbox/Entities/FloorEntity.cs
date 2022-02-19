using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;

namespace Sandbox.Entities;

internal class FloorEntity : EntityBase
{
    public FloorEntity(IMeshFactory meshFactory, IShader shader, string modelFilepath)
        : base("Floor")
    {
        var transform = AddComponent<Transform>();
        transform.Position = new Vector3(0.0f, 0.0f, 0.0f);
        transform.Scale = Vector3.One * 10.0f;

        var meshRenderer = AddComponent<MeshRenderer>();

        meshRenderer.Shader = shader;
        meshRenderer.Ambient = new Vector3(0.1f, 0.18725f, 0.1745f);
        meshRenderer.Diffuse = new Vector3(0.396f, 0.74151f, 0.69102f);
        meshRenderer.Specular = new Vector3(0.297254f, 0.30829f, 0.306678f);
        meshRenderer.Shininess = 0.1f;

        meshRenderer.Model = new Model(meshFactory, shader, modelFilepath);

        var rigidbody = AddComponent<Rigidbody>();

        var collider = AddComponent<BoxCollider>();
        collider.Size = new Vector3(10.0f, 0.001f, 10.0f) * 10.0f;
    }
}