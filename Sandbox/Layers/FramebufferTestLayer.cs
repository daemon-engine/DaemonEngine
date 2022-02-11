using DaemonEngine.Core.Layer;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox.Layers;
internal class FramebufferTestLayer : LayerBase
{
    Model _sphere;

    FPSCamera _camera;
    IFramebuffer _framebuffer;
    IShader _shader;

    public FramebufferTestLayer(string name, IServiceProvider serviceProvider)
        : base(name, serviceProvider)
    {
    }

    public override void OnStart()
    {
        _camera = new FPSCamera(60.0f, Window.AspectRatio);
        _camera.Position = new Vector3(0.0f, 0.0f, 0.0f);

        var meshFactory = ServiceProvider.GetRequiredService<IMeshFactory>();
        _shader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Chapter3/Basic.shader");

        _framebuffer = GraphicsFactory.CreateFramebuffer();

        _sphere = new Model(meshFactory, _shader, "Assets/Models/Sphere/sphere.obj");
    }

    public override void OnShutdown()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        _framebuffer.Bind();

        Renderer.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
        Renderer.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);

        _shader.Bind();
        _shader.SetMat4("_Model", Matrix4.Identity);
        _shader.SetMat4("_View", _camera.ViewMatrix);
        _shader.SetMat4("_Projection", _camera.ProjectionMatrix);
        Renderer.RenderMesh(_sphere.Mesh);

        _framebuffer.Unbind();

        Renderer.ClearColor(0.8f, 0.8f, 0.8f, 1.0f);
        Renderer.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);
    }

    public override void OnGUI()
    {
        ImGuiNET.ImGui.Begin("Test");
        ImGuiNET.ImGui.Text("Framebuffer Test");

        var ptr = (IntPtr)_framebuffer.GetColorAttachment();
        ImGuiNET.ImGui.Image(ptr, new System.Numerics.Vector2(800, 600));
        ImGuiNET.ImGui.End();

        ImGuiNET.ImGui.ShowDemoWindow();
    }
}
