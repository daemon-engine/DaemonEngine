using DaemonEngine.Windows;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using Serilog;
using System.Numerics;
using DaemonEngine.EventSystem;
using DaemonEngine.EventSystem.Events.Window;
using DaemonEngine.Windows.Inputs;
using DaemonEngine.Application;
using Sandbox.Layers;

namespace Sandbox;

public class Application : ApplicationBase
{
    public Application(ILogger logger, IWindow window, IInput input, IRenderer renderer, IGraphicsFactory graphicsFactory, IServiceProvider serviceProvider)
        : base(logger, window, input, renderer, graphicsFactory, serviceProvider)
    {
    }

    public override void OnStart()
    {
        Logger.Information("Application.OnStart");
        AddLayer<Chapter1Layer>("Chapter 1 Layer");
    }

    public override void OnShutdown()
    {
        Logger.Information("Application.OnShutdown");
    }

    public override void OnUpdate(float deltaTime)
    {
    }

    public override void OnEvent(IEvent e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowCloseEvent>(OnWindowCloseEvent);

        base.OnEvent(e);
    }

    private bool OnWindowCloseEvent(WindowCloseEvent e)
    {
        Close();
        return true;
    }
}
