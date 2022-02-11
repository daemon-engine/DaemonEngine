using DaemonEngine.Core.Layer;

namespace Sandbox.Layers;

internal class ImGuiTestLayer : LayerBase
{
    public ImGuiTestLayer(string name, IServiceProvider serviceProvider) 
        : base(name, serviceProvider)
    {
    }

    public override void OnStart()
    {
    }

    public override void OnShutdown()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
    }

    public override void OnGUI()
    {
        ImGuiNET.ImGui.Begin("Test");
        ImGuiNET.ImGui.Text("Hello world!");
        ImGuiNET.ImGui.ShowDemoWindow();

        ImGuiNET.ImGui.ShowDemoWindow();
    }
}
