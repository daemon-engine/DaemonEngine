using Autofac;
using DaemonEngine.DependencyInjection;
using DaemonEngine.Graphics.Windows;
using DaemonEngine.Windows;
using Serilog;

namespace DaemonEngine.Graphics.DependencyInjection;

public static class DaemonEngineContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterWindow(this IDaemonEngineContainerBuilder builder, WindowOptions windowOptions)
    {
        builder.ContainerBuilder
            .RegisterType<GLWindow>()
            .UsingConstructor(typeof(ILogger), typeof(WindowOptions))
            .WithParameter(TypedParameter.From(windowOptions))
            .As<IWindow>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder;
    }
}
