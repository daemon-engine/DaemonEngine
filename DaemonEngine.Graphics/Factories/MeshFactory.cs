using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.Factories;

public interface IMeshFactory
{
    Mesh CreateMesh(float[] vertices, uint[] indices, IShader shader, BufferLayout bufferLayout);
    Mesh CreateMesh(float[] vertices, uint[] indices, IShader shader, BufferLayout bufferLayout, bool wireframe);
    Mesh CreateMesh(float[] vertices, uint[] indices, IPipeline pipeline);
}

internal class MeshFactory : IMeshFactory
{
    public MeshFactory(IGraphicsFactory graphicsFactory)
    {
        GraphicsFactory = graphicsFactory;
    }

    protected IGraphicsFactory GraphicsFactory { get; set; }

    public Mesh CreateMesh(float[] vertices, uint[] indices, IShader shader, BufferLayout bufferLayout)
    {
        return new Mesh(GraphicsFactory, vertices, indices, shader, bufferLayout);
    }

    public Mesh CreateMesh(float[] vertices, uint[] indices, IShader shader, BufferLayout bufferLayout, bool wireframe)
    {
        return new Mesh(GraphicsFactory, vertices, indices, shader, bufferLayout, wireframe);
    }

    public Mesh CreateMesh(float[] vertices, uint[] indices, IPipeline pipeline)
    {
        return new Mesh(GraphicsFactory, vertices, indices, pipeline);
    }
}
