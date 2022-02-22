using DaemonEngine.Application;
using Sandbox.Layers;
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
        //AddLayer<Chapter2Layer>("Lighting Layer");
        //AddLayer<Chapter3Layer>("Model Loading Layer");
        //AddLayer<ImGuiTestLayer>("ImGui Test Layer");
        //AddLayer<FramebufferTestLayer>("Framebuffer Test Layer");
        //AddLayer<CubemapTestLayer>("Cubemap Test Layer");
        //AddLayer<SceneTestLayer>("Scene Test Layer");
        //AddLayer<PhysicsTestLayer>("Physics Test Layer");
        AddLayer<LandscapeLayer>("Landscape Layer");
        //AddLayer<SandboxLayer>("Sandbox Layer");
    }

    public override void OnShutdown()
    {
        Logger.Information("Application.OnShutdown");
    }
}
