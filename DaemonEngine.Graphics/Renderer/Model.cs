using DaemonEngine.Extensions.AssimpNet;
using DaemonEngine.Graphics.Factories;

namespace DaemonEngine.Graphics.Renderer;

public class Model
{
    private Mesh _mesh;
    private readonly IMeshFactory _meshFactory;

    public Model(IMeshFactory meshFactory, IShader shader, BufferLayout bufferLayout, string filepath)
    {
        _meshFactory = meshFactory;

        Load(filepath, shader, bufferLayout);
    }

    public Mesh Mesh => _mesh;

    private void Load(string filepath, IShader shader, BufferLayout bufferLayout)
    {
        (float[] vertices, uint[] indices) = ModelLoader.LoadModel(filepath);
        _mesh = _meshFactory.CreateMesh(vertices, indices, shader, bufferLayout);
    }
}
