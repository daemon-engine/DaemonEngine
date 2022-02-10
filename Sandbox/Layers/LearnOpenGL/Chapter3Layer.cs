using DaemonEngine.Application;
using DaemonEngine.Core;
using DaemonEngine.Core.Inputs;
using DaemonEngine.Core.Layer;
using DaemonEngine.EventSystem;
using DaemonEngine.EventSystem.Events.Key;
using DaemonEngine.EventSystem.Events.Window;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Enums;
using DaemonEngine.Mathematics;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox.Layers.LearnOpenGL;

internal class Chapter3Layer : LayerBase
{
    private Model _cube;
    private Model _sphere;
    private Model _rocket;
    private FPSCamera _camera;
    private IShader _shader;

    private bool _paused = false;
    private Vector3 _lightDirection = new(-0.2f, -1.0f, -0.3f);

    private readonly IMeshFactory _meshFactory;
    private readonly IApplication _application;
    private readonly ICursor _cursor;

    public Chapter3Layer(string name, IServiceProvider serviceProvider)
        : base(name, serviceProvider)
    {
        _meshFactory = ServiceProvider.GetRequiredService<IMeshFactory>();
        _application = ServiceProvider.GetRequiredService<IApplication>();
        _cursor = ServiceProvider.GetRequiredService<ICursor>();
    }

    public override void OnStart()
    {
        _cursor.Disable();
        _camera = new FPSCamera(60.0f, Window.AspectRatio);

        _shader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Chapter3/LitBasic.shader");

        _cube = new Model(_meshFactory, _shader, "Assets/Models/cube.obj");
        _sphere = new Model(_meshFactory, _shader, "Assets/Models/Sphere/sphere.obj");
        _rocket = new Model(_meshFactory, _shader, "Assets/Models/Rocket/rocket.obj");
    }

    public override void OnShutdown()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        if (!_paused)
        {
            _camera.Update(deltaTime);
        }

        Renderer.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);
        Renderer.ClearColor(0.3f, 0.5f, 0.8f, 1.0f);

        _shader.Bind();
        _shader.SetMat4("_View", _camera.ViewMatrix);
        _shader.SetMat4("_Projection", _camera.ProjectionMatrix);

        _shader.SetFloat3("_ViewPos", _camera.Position);

        _shader.SetFloat3("_DirectionalLight.direction", _lightDirection);
        _shader.SetFloat3("_DirectionalLight.ambient", 0.05f, 0.05f, 0.05f);
        _shader.SetFloat3("_DirectionalLight.diffuse", 0.4f, 0.4f, 0.4f);
        _shader.SetFloat3("_DirectionalLight.specular", 1.0f, 1.0f, 1.0f);

        _shader.SetMat4("_Model", Matrix4.Identity * Matrix4.Translate(new Vector3(0.0f, 2.5f, 0.0f)));
        _shader.SetFloat3("_Material.ambient", new Vector3(0.24725f, 0.1995f, 0.0745f));
        _shader.SetFloat3("_Material.diffuse", new Vector3(0.75164f, 0.60648f, 0.22648f));
        _shader.SetFloat3("_Material.specular", new Vector3(0.628281f, 0.555802f, 0.366065f));
        _shader.SetFloat("_Material.shininess", 0.4f * 128.0f);
        Renderer.RenderMesh(_rocket.Mesh);

        _shader.SetMat4("_Model", Matrix4.Identity * Matrix4.Translate(new Vector3(0.0f, -2.5f, 0.0f)));
        _shader.SetFloat3("_Material.ambient", new Vector3(0.24725f, 0.1995f, 0.0745f));
        _shader.SetFloat3("_Material.diffuse", new Vector3(0.75164f, 0.60648f, 0.22648f));
        _shader.SetFloat3("_Material.specular", new Vector3(0.628281f, 0.555802f, 0.366065f));
        _shader.SetFloat("_Material.shininess", 0.4f * 128.0f);
        Renderer.RenderMesh(_cube.Mesh);

        // Emerald sphere
        _shader.SetMat4("_Model", Matrix4.Identity * Matrix4.Translate(new Vector3(2.5f, 0.0f, 2.5f)));
        _shader.SetFloat3("_Material.ambient", new Vector3(0.0215f, 0.1745f, 0.0215f));
        _shader.SetFloat3("_Material.diffuse", new Vector3(0.07568f, 0.61424f, 0.07568f));
        _shader.SetFloat3("_Material.specular", new Vector3(0.633f, 0.727811f, 0.633f));
        _shader.SetFloat("_Material.shininess", 0.6f * 128.0f);
        Renderer.RenderMesh(_sphere.Mesh);

        // Gold sphere
        _shader.SetMat4("_Model", Matrix4.Identity * Matrix4.Translate(new Vector3(0.0f, 0.0f, 0.0f)));
        _shader.SetFloat3("_Material.ambient", new Vector3(0.24725f, 0.1995f, 0.0745f));
        _shader.SetFloat3("_Material.diffuse", new Vector3(0.75164f, 0.60648f, 0.22648f));
        _shader.SetFloat3("_Material.specular", new Vector3(0.628281f, 0.555802f, 0.366065f));
        _shader.SetFloat("_Material.shininess", 0.4f * 128.0f);
        Renderer.RenderMesh(_sphere.Mesh);

        // Bronze sphere
        _shader.SetMat4("_Model", Matrix4.Identity * Matrix4.Translate(new Vector3(-2.5f, 0.0f, -2.5f)));
        _shader.SetFloat3("_Material.ambient", new Vector3(0.2125f, 0.1275f, 0.054f));
        _shader.SetFloat3("_Material.diffuse", new Vector3(0.714f, 0.4284f, 0.18144f));
        _shader.SetFloat3("_Material.specular", new Vector3(0.393548f, 0.271906f, 0.166721f));
        _shader.SetFloat("_Material.shininess", 0.2f * 128.0f);
        Renderer.RenderMesh(_sphere.Mesh);
    }

    public override void OnGUI()
    {
        ImGuiNET.ImGui.Begin("Properties");

        ImGuiNET.ImGui.Text($"State: {(_paused ? "Paused" : "Playing")}");

        System.Numerics.Vector3 vector = _lightDirection;
        ImGuiNET.ImGui.SliderFloat3("Light Direction", ref vector, -1.0f, 1.0f);
        _lightDirection = vector;

        ImGuiNET.ImGui.End();
    }

    public override void OnEvent(IEvent e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowResizeEvent>(OnWindowResizeEvent);
        dispatcher.Dispatch<KeyPressedEvent>(OnKeyPressedEvent);
    }

    private bool OnWindowResizeEvent(WindowResizeEvent e)
    {
        _camera.SetViewport(e.Width, e.Height);
        return true;
    }

    private bool OnKeyPressedEvent(KeyPressedEvent e)
    {
        if (e.KeyCode == (int)Keycode.ESCAPE)
        {
            _application.Stop();
        }

        if (e.KeyCode == (int)Keycode.P)
        {
            _paused = !_paused;
            if(_paused)
            {
                _cursor.Show();
            }
            else
            {
                _cursor.Disable();
            }
        }

        return true;
    }
}
