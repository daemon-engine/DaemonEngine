using DaemonEngine.Application;
using Sandbox.Layers.LearnOpenGL;

namespace Sandbox;

public class Application : ApplicationBase
{
    public Application(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public override void OnStart()
    {
        Logger.Information("Application.OnStart");
        //AddLayer<Chapter1Layer>("Getting Started Layer");
        AddLayer<Chapter2Layer>("Lighting Layer");
        //AddLayer<ImGuiTestLayer>("ImGui Test Layer");
    }

    public override void OnShutdown()
    {
        Logger.Information("Application.OnShutdown");
    }
}
