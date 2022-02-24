using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;

namespace Sandbox.Entities;

internal class PhysicsEntity : EntityBase
{
    public PhysicsEntity(string name, IMeshFactory meshFactory, IShader shader, string modelFilepath, Vector3 position, float mass = 1.0f)
        : base(name)
    {
        var transform = AddComponent<Transform>();
        transform.Position = position;

        var meshRenderer = AddComponent<MeshRenderer>();

        meshRenderer.Shader = shader;
        meshRenderer.Ambient = new Vector3(0.1745f, 0.01175f, 0.01175f);
        meshRenderer.Diffuse = new Vector3(0.61424f, 0.04136f, 0.04136f);
        meshRenderer.Specular = new Vector3(0.727811f, 0.626959f, 0.626959f);
        meshRenderer.Shininess = 0.6f;

        meshRenderer.Model = new Model(meshFactory, shader, modelFilepath);

        var rigidbody = AddComponent<Rigidbody>();
        rigidbody.Type = RigidbodyType.Dynamic;
        rigidbody.Mass = mass;

        var collider = AddComponent<BoxCollider>();
        collider.Size = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
