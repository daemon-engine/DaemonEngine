using Autofac;
using Autofac.Extensions.DependencyInjection;
using DaemonEngine.Application;
using DaemonEngine.Core.Layer;
using DaemonEngine.Factories;
using DaemonEngine.Windows;
using DaemonEngine.Windows.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DaemonEngine.DependencyInjection;

public static class DaemonEngineContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterApplication<TApplication>(this IDaemonEngineContainerBuilder builder)
        where TApplication : class, IApplication
    {
        builder.ContainerBuilder
            .RegisterType<TApplication>()
            .As<IApplication>()
            .AutoActivate()
            .InstancePerLifetimeScope();

        builder.ContainerBuilder
            .RegisterType<LayerFactory>()
            .As<ILayerFactory>()
            .InstancePerLifetimeScope();

        var windowOptions = new WindowOptions
        {
            Title = "Test Window",
            Width = 1024,
            Height = 768
        };
        builder.RegisterWindow(windowOptions);

        return builder;
    }

    public static IDaemonEngineContainerBuilder RegisterLogging(this IDaemonEngineContainerBuilder builder)
    {
        builder.ContainerBuilder
            .Register((cc) =>
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
        builder.ContainerBuilder.Populate(builder.ServiceCollection);
        var container = builder.ContainerBuilder.Build();

        builder.ContainerBuilder
            .Register((cc) =>
            {
                var serviceProvider = new AutofacServiceProvider(container);
                return serviceProvider;
            })
            .As<IServiceProvider>()
            .InstancePerLifetimeScope();


        using var scope = container.BeginLifetimeScope();
        var application = scope.Resolve<IApplication>();
        application.Run();
    }
}
