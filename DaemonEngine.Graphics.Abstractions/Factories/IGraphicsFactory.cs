using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.Factories;

public interface IGraphicsFactory
{
    IShader CreateShader(string filepath);
    IShader CreateShader(string vertexShaderSource, string fragmentShaderSource);

    IPipeline CreatePipeline(IShader shader, IBufferLayout bufferLayout);
    IVertexBuffer CreateVertexBuffer(int size, float[] vertices);
    IIndexBuffer CreateIndexBuffer(int count, uint[] indices);

    IFramebuffer CreateFramebuffer();

    ITexture CreateTexture(string filepath);
    ITexture CreateTexture(int width, int height);
}
