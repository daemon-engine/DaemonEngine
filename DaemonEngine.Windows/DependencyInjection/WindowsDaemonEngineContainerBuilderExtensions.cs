using DaemonEngine.DependencyInjection;
using DaemonEngine.Windows.GraphicsLibraryFramework.DependencyInjection;

namespace DaemonEngine.Windows.DependencyInjection;

public static class WindowsDaemonEngineContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterWindow(this IDaemonEngineContainerBuilder builder, WindowApi windowApi)
    {
        switch (windowApi)
        {
            case WindowApi.Glfw:    builder.RegisterGlfwWindow(); break;
            case WindowApi.Win32:
            case WindowApi.None:
            default: break;
        }

        return builder;
    }
}


