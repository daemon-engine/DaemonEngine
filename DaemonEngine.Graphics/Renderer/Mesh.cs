using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer.Data;

namespace DaemonEngine.Graphics.Renderer;

public class Mesh : IMesh
{
    private IPipeline _pipeline;
    private IVertexBuffer _vertexBuffer;
    private IIndexBuffer _indexBuffer;

    private float[] _vertices;
    private uint[] _indices;
    private int _count;

    internal Mesh(IGraphicsFactory graphicsFactory, float[] vertices, uint[] indices, IPipeline pipeline)
    {
        _vertices = vertices;
        _indices = indices;

        GraphicsFactory = graphicsFactory;

        _pipeline = pipeline;
        Setup();
    }

    internal Mesh(IGraphicsFactory graphicsFactory, float[] vertices, uint[] indices, IShader shader, BufferLayout bufferLayout, bool wireframe = false)
    {
        _vertices = vertices;
        _indices = indices;

        GraphicsFactory = graphicsFactory;

        var pipelineOptions = new PipelineOptions
        {
            BufferLayout = bufferLayout,
            Shader = shader,
            Wireframe = wireframe
        };

        _pipeline = GraphicsFactory.CreatePipeline(pipelineOptions);

        Setup();
    }

    protected IGraphicsFactory GraphicsFactory { get; } = default!;

    public float[] GetVertices()
    {
        return _vertices;
    }

    public uint[] GetIndices()
    {
        return _indices;
    }

    private void Setup()
    {
        int verticesSize = sizeof(float) * _vertices.Length;
        _vertexBuffer = GraphicsFactory.CreateVertexBuffer(verticesSize, _vertices);

        _indexBuffer = GraphicsFactory.CreateIndexBuffer(_indices.Length, _indices);
        _count = _indexBuffer.Count;
    }

    public void Bind()
    {
        _vertexBuffer.Bind();
        _pipeline.Bind();
        _indexBuffer.Bind();
    }

    public void Unbind()
    {
        _vertexBuffer.Unbind();
        _pipeline.Unbind();
        _indexBuffer.Unbind();
    }

    public int GetIndexBufferCount()
    {
        return _count;
    }
}
