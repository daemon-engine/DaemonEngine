using DaemonEngine.Extensions.AssimpNet;
using DaemonEngine.Graphics.Factories;

namespace DaemonEngine.Graphics.Renderer;

public class Model
{
    private readonly BufferLayout _bufferLayout;
    private readonly IMeshFactory _meshFactory;

    public Model(IMeshFactory meshFactory, IShader shader, string filepath, bool wireframe = false)
    {
        _meshFactory = meshFactory;

        _bufferLayout = new BufferLayout(new List<BufferElement>
        {
            new BufferElement("POSITION", ShaderDataType.Float3),
            new BufferElement("NORMAL", ShaderDataType.Float3),
            new BufferElement("TEXCOORD", ShaderDataType.Float2)
        });

        Meshes = new List<Mesh>();
        Load(shader, filepath, wireframe);
    }

    public Mesh Mesh { get; private set; } = default!;
    public IList<Mesh> Meshes { get; private set; } = default!;

    private void Load(IShader shader, string filepath, bool wireframe)
    {
        var modelDatas = ModelLoader.LoadModels(filepath);
        foreach (var modelData in modelDatas)
        {
            var mesh = _meshFactory.CreateMesh(modelData.Vertices, modelData.Indices, shader, _bufferLayout, wireframe);
            Meshes.Add(mesh);
        }

        Mesh = Meshes[0];
    }
}
