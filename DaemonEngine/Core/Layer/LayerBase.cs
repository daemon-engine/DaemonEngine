using DaemonEngine.EventSystem;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Physics.Worlds;
using DaemonEngine.Windows;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DaemonEngine.Core.Layer;

public abstract class LayerBase : ILayer
{
    public LayerBase(string name, IServiceProvider serviceProvider)
    {
        Name = name;
        ServiceProvider = serviceProvider;

        Logger = ServiceProvider.GetRequiredService<ILogger>();
        Window = ServiceProvider.GetRequiredService<IWindow>();
        Renderer = ServiceProvider.GetRequiredService<IRenderer>();
        World = ServiceProvider.GetRequiredService<IWorld>();
        GraphicsFactory = ServiceProvider.GetRequiredService<IGraphicsFactory>();
    }

    protected string Name { get; }
    protected IServiceProvider ServiceProvider { get; }

    protected ILogger Logger { get; }
    protected IWindow Window { get; }
    protected IRenderer Renderer { get; }
    protected IWorld World { get; }
    protected IGraphicsFactory GraphicsFactory { get; }

    public abstract void OnStart();
    public abstract void OnShutdown();

    public abstract void OnUpdate(float deltaTime);

    public virtual void OnEvent(IEvent e)
    {
    }

    public virtual void OnGUI()
    {
    }
}
