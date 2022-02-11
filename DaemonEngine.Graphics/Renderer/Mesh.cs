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

    internal Mesh(IGraphicsFactory graphicsFactory, float[] vertices, uint[] indices, IShader shader, BufferLayout bufferLayout)
    {
        _vertices = vertices;
        _indices = indices;

        GraphicsFactory = graphicsFactory;
        Setup(shader, bufferLayout);
    }

    protected IGraphicsFactory GraphicsFactory { get; } = default!;

    private void Setup(IShader shader, BufferLayout bufferLayout)
    {
        var pipelineOptions = new PipelineOptions
        {
            BufferLayout = bufferLayout,
            Shader = shader
        };

        _pipeline = GraphicsFactory.CreatePipeline(pipelineOptions);

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
