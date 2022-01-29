using Autofac;
using Serilog;

namespace DaemonEngine.DependencyInjection;

public static class DaemonEngineContainerExtensions
{
    public static IDaemonEngineContainerBuilder RegisterApplication<TApplication>(this IDaemonEngineContainerBuilder builder)
        where TApplication : class, IApplication
    {
        builder.ContainerBuilder.RegisterType<TApplication>()
            .As<IApplication>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder;
    }

    public static IDaemonEngineContainerBuilder RegisterLogging(this IDaemonEngineContainerBuilder builder)
    {
        builder.ContainerBuilder.Register((cc) =>
        {
            return new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        })
            .As<ILogger>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder;
    }

    public static void BuildAndRun(this IDaemonEngineContainerBuilder builder)
    {
        var container = builder.ContainerBuilder.Build();

        using var scope = container.BeginLifetimeScope();
        var application = scope.Resolve<IApplication>();
        application.Run();
        scope.Dispose();
    }
}
