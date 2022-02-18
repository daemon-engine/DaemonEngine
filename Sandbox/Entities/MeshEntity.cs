using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;

namespace Sandbox.Entities;

internal class MeshEntity : EntityBase
{
    public MeshEntity(string filepath, Vector3 position, IMeshFactory meshFactory, IShader shader) 
        : base("Model")
    {
        var transform = AddComponent<Transform>();
        transform.Position = position;

        var meshRenderer = AddComponent<MeshRenderer>();

        meshRenderer.Shader = shader;
        meshRenderer.Ambient = new Vector3(0.1f, 0.18725f, 0.1745f);
        meshRenderer.Diffuse = new Vector3(0.396f, 0.74151f, 0.69102f);
        meshRenderer.Specular = new Vector3(0.297254f, 0.30829f, 0.306678f);
        meshRenderer.Shininess = 0.1f;

        meshRenderer.Model = new Model(meshFactory, shader, filepath);

        var rigidbody = AddComponent<Rigidbody>();

        var collider = AddComponent<BoxCollider>();
        collider.Size = new Vector3(10.0f, 0.01f, 10.0f);
    }
}
