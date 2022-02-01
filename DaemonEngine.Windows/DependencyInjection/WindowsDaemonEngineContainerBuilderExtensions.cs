using DaemonEngine.DependencyInjection;
using DaemonEngine.Windows.GraphicsLibraryFramework.DependencyInjection;

namespace DaemonEngine.Windows.DependencyInjection;

public static class WindowsDaemonEngineContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterWindow(this IDaemonEngineContainerBuilder builder, WindowOptions windowOptions)
    {
        builder.RegisterGlfwWindow(windowOptions);
        builder.RegisterGlfwInput();
        return builder;
    }
}
