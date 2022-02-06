using Autofac;
using DaemonEngine.DependencyInjection;
using DaemonEngine.Windows.Inputs;
using Serilog;

namespace DaemonEngine.Windows.GraphicsLibraryFramework.DependencyInjection;

public static class GlfwDaemonEngineContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterGlfwWindow(this IDaemonEngineContainerBuilder builder, WindowOptions windowOptions)
    {
        builder.ContainerBuilder
            .RegisterType<GlfwWindow>()
            .UsingConstructor(typeof(ILogger), typeof(WindowOptions))
            .WithParameter(TypedParameter.From(windowOptions))
            .As<IWindow>()
            .InstancePerLifetimeScope();

        return builder;
    }

    public static IDaemonEngineContainerBuilder RegisterGlfwInput(this IDaemonEngineContainerBuilder builder)
    {
        builder.ContainerBuilder
            .RegisterType<GlfwInput>()
            .As<IInput>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder;
    }
}
