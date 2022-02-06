using DaemonEngine.DependencyInjection;
using DaemonEngine.Graphics.OpenGL.DependencyInjection;
using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.DependencyInjection;

public static class GraphicsDaemonEngineContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterGraphicsModule(this IDaemonEngineContainerBuilder builder, RendererApi rendererApi)
    {
        switch (rendererApi)
        {
            case RendererApi.OpenGL: builder.RegisterOpenGL(); break;
            case RendererApi.Vulkan:
            case RendererApi.DirectX:
            case RendererApi.None:
            default: break;
        }

        return builder;
    }
}
