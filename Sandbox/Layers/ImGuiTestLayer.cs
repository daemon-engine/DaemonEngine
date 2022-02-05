using DaemonEngine.Core.Layer;
using DaemonEngine.Graphics.Renderer.Enums;

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
        Renderer.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
        Renderer.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);
    }

    public override void OnGUI()
    {
        ImGuiNET.ImGui.Begin("Test");
        ImGuiNET.ImGui.Text("Hello, world!!");
        ImGuiNET.ImGui.End();

        ImGuiNET.ImGui.ShowDemoWindow();
    }
}
