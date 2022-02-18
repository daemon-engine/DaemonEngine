using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;

namespace Sandbox.Entities;

internal class SphereEntity : EntityBase
{
    public SphereEntity(IMeshFactory meshFactory, IShader shader, Vector3 position) 
        : base("Sphere with Physics!")
    {
        var transform = AddComponent<Transform>();
        transform.Position = position;

        var meshRenderer = AddComponent<MeshRenderer>();

        meshRenderer.Shader = shader;
        meshRenderer.Ambient = new Vector3(0.1745f, 0.01175f, 0.01175f);
        meshRenderer.Diffuse = new Vector3(0.61424f, 0.04136f, 0.04136f);
        meshRenderer.Specular = new Vector3(0.727811f, 0.626959f, 0.626959f);
        meshRenderer.Shininess = 0.6f;

        var filepath = "Assets/Models/Sphere/sphere.obj";
        meshRenderer.Model = new Model(meshFactory, shader, filepath);

        var rigidbody = AddComponent<Rigidbody>();
        rigidbody.Type = RigidbodyType.Dynamic;
        rigidbody.Mass = 1.0f;

        var collider = AddComponent<SphereCollider>();
    }
}
