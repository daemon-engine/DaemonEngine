using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Data;

namespace DaemonEngine.Graphics.Factories;

public interface IGraphicsFactory
{
    IShader CreateShader(string filepath);
    IShader CreateShader(string vertexShaderSource, string fragmentShaderSource);

    IPipeline CreatePipeline(PipelineOptions pipelineOptions);
    IVertexBuffer CreateVertexBuffer(int size, float[] vertices);
    IIndexBuffer CreateIndexBuffer(int count, uint[] indices);

    IFramebuffer CreateFramebuffer(FramebufferOptions framebufferOptions);

    ICubemap CreateCubemap(CubemapOptions cubemapOptions);

    IUniformBuffer CreateUniformBuffer(int size, uint binding);

    ITexture CreateTexture(string filepath);
    ITexture CreateTexture(int width, int height);
}
