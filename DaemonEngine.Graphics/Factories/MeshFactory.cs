using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.Factories;

public interface IMeshFactory
{
    Mesh CreateMesh(float[] vertices, uint[] indices, IShader shader, BufferLayout bufferLayout);
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
}
