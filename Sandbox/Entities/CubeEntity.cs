using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;

namespace Sandbox.Entities;

internal class CubeEntity : EntityBase
{
    public CubeEntity(IMeshFactory meshFactory, IShader shader, string modelFilepath)
        : base("Cube")
    {
        var transform = AddComponent<Transform>();
        transform.Position = new Vector3(0.0f, 1.0f, 0.0f);

        var meshRenderer = AddComponent<MeshRenderer>();

        meshRenderer.Shader = shader;
        meshRenderer.Ambient = new Vector3(0.1745f, 0.01175f, 0.01175f);
        meshRenderer.Diffuse = new Vector3(0.61424f, 0.04136f, 0.04136f);
        meshRenderer.Specular = new Vector3(0.727811f, 0.626959f, 0.626959f);
        meshRenderer.Shininess = 0.6f;

        meshRenderer.Model = new Model(meshFactory, shader, modelFilepath);
    }
}