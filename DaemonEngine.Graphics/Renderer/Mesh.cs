using DaemonEngine.Graphics.Factories;

namespace DaemonEngine.Graphics.Renderer;

public class Mesh : IMesh
{
    private IPipeline _pipeline;
    private IVertexBuffer _vertexBuffer;
    private IIndexBuffer _indexBuffer;

    internal Mesh(IGraphicsFactory graphicsFactory, float[] vertices, uint[] indices, IShader shader, BufferLayout bufferLayout)
    {
        GraphicsFactory = graphicsFactory; 
        Setup(vertices, indices, shader, bufferLayout);
    }

    protected IGraphicsFactory GraphicsFactory { get; }

    private void Setup(float[] vertices, uint[] indices, IShader shader, BufferLayout bufferLayout)
    {
        _pipeline = GraphicsFactory.CreatePipeline(shader, bufferLayout);

        int verticesSize = sizeof(float) * vertices.Length;
        _vertexBuffer = GraphicsFactory.CreateVertexBuffer(verticesSize, vertices);

        _indexBuffer = GraphicsFactory.CreateIndexBuffer(indices.Length, indices);
    }

    public void Bind()
    {
        _pipeline.Bind();
        _vertexBuffer.Bind();
        _indexBuffer.Bind();
    }

    public int GetIndexBufferCount()
    {
        return _indexBuffer.Count;
    }
}
