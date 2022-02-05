using DaemonEngine.Core.Layer;
using DaemonEngine.EventSystem;
using DaemonEngine.EventSystem.Events.Window;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Windows;
using DaemonEngine.Windows.Inputs;
using ImGuiNET;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DaemonEngine.Core.ImGui;
public class ImGuiLayer : LayerBase
{
    private ImGuiController _imGuiController;
    private float _deltaTime = 0.0f;

    public ImGuiLayer(string name, IServiceProvider serviceProvider) 
        : base(name, serviceProvider)
    {
        Logger = ServiceProvider.GetRequiredService<ILogger>();
        GraphicsFactory = ServiceProvider.GetRequiredService<IGraphicsFactory>();
    }

    protected ILogger Logger { get; }
    protected IGraphicsFactory GraphicsFactory { get; }

    public override void OnStart()
    {
        var window = ServiceProvider.GetRequiredService<IWindow>();
        var input = ServiceProvider.GetRequiredService<IInput>();

        _imGuiController = new ImGuiController(Logger, GraphicsFactory, input, window.Width, window.Height);
    }

    public override void OnShutdown()
    {
    }

    public void Start()
    {
        _imGuiController.Update(_deltaTime);
    }

    public void End()
    {
        _imGuiController.Render();
    }

    public override void OnUpdate(float deltaTime)
    {
        _deltaTime = deltaTime;
    }

    public override void OnEvent(IEvent e)
    {
        EventDispatcher dispatcher = new(e);
        dispatcher.Dispatch<WindowResizeEvent>(OnWindowResizeEvent);
    }

    private bool OnWindowResizeEvent(WindowResizeEvent e)
    {
        _imGuiController.ResizeImGuiContext(e.Width, e.Height);
        return true;
    }
}
