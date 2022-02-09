using DaemonEngine.Extensions.AssimpNet;
using DaemonEngine.Graphics.Factories;

namespace DaemonEngine.Graphics.Renderer;

public class Model
{
    private readonly BufferLayout _bufferLayout;
    private readonly IMeshFactory _meshFactory;

    public Model(IMeshFactory meshFactory, IShader shader, string filepath)
    {
        _meshFactory = meshFactory;

        _bufferLayout = new BufferLayout(new List<BufferElement>
        {
            new BufferElement("POSITION", ShaderDataType.Float3),
            new BufferElement("NORMAL", ShaderDataType.Float3),
            new BufferElement("TEXCOORD", ShaderDataType.Float2)
        });

        Load(shader, filepath);
    }

    public Mesh Mesh { get; private set; } = default!;

    private void Load(IShader shader, string filepath)
    {
        (float[] vertices, uint[] indices) = ModelLoader.LoadModel(filepath);

        Mesh = _meshFactory.CreateMesh(vertices, indices, shader, _bufferLayout);
    }
}
