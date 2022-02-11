using DaemonEngine.Core;
using DaemonEngine.Core.Inputs;
using DaemonEngine.Core.Layer;
using DaemonEngine.EventSystem;
using DaemonEngine.EventSystem.Events.Key;
using DaemonEngine.EventSystem.Events.Window;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Data;
using DaemonEngine.Mathematics;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox.Layers;

internal class SandboxLayer : LayerBase
{
    ICubemap _cubemap;

    IShader _cubemapShader;
    IPipeline _cubemapPipeline;
    IVertexBuffer _cubemapVertexBuffer;
    IIndexBuffer _cubemapIndexBuffer;

    private IShader _shader;
    private Model _sceneModel;

    private Vector3 _lightDirection = new(-0.2f, -1.0f, -0.3f);

    private FPSCamera _camera;
    private bool _paused = false;

    private readonly ICursor _cursor;

    public SandboxLayer(string name, IServiceProvider serviceProvider)
        : base(name, serviceProvider)
    {
        _cursor = ServiceProvider.GetRequiredService<ICursor>();
    }

    public override void OnStart()
    {
        _cursor.Disable();
        _camera = new FPSCamera(60.0f, Window.AspectRatio);
        _camera.Position.Y = 2.5f;

        _shader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Chapter3/LitBasic.shader");

        var meshFactory = ServiceProvider.GetRequiredService<IMeshFactory>();
        _sceneModel = new Model(meshFactory, _shader, "Assets/Models/BasicScene/basic_scene.obj");

        var cubemapOptions = new CubemapOptions
        {
            Paths = new string[6]
    {
                "Assets/Textures/Skybox/left.jpg",
                "Assets/Textures/Skybox/right.jpg",
                "Assets/Textures/Skybox/top.jpg",
                "Assets/Textures/Skybox/bottom.jpg",
                "Assets/Textures/Skybox/front.jpg",
                "Assets/Textures/Skybox/back.jpg"
    }
        };

        _cubemap = GraphicsFactory.CreateCubemap(cubemapOptions);

        _cubemapShader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Cubemap.shader");
        _cubemapShader.Bind();
        _cubemapShader.SetInt("_Cubemap", 0);

        var bufferLayout = new BufferLayout(new List<BufferElement>
        {
            new BufferElement("POSITION", ShaderDataType.Float3)
        });

        var pipelineOptions = new PipelineOptions
        {
            BufferLayout = bufferLayout,
            Shader = _cubemapShader
        };
        _cubemapPipeline = GraphicsFactory.CreatePipeline(pipelineOptions);

        var vertices = Builder.GenerateCubemapVertices();
        var indices = Builder.GenerateCubemapIndices();

        _cubemapVertexBuffer = GraphicsFactory.CreateVertexBuffer(vertices.Length * sizeof(float), vertices);
        _cubemapIndexBuffer = GraphicsFactory.CreateIndexBuffer(indices.Length, indices);
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

        _shader.SetFloat3("_Material.ambient", new Vector3(0.24725f, 0.1995f, 0.0745f));
        _shader.SetFloat3("_Material.diffuse", new Vector3(0.75164f, 0.60648f, 0.22648f));
        _shader.SetFloat3("_Material.specular", new Vector3(0.628281f, 0.555802f, 0.366065f));
        _shader.SetFloat("_Material.shininess", 0.4f * 128.0f);

        _shader.SetMat4("_Model", Matrix4.Identity);
        Renderer.RenderMesh(_sceneModel.Mesh);

        _cubemap.Bind();
        _cubemapShader.Bind();
        _cubemapShader.SetMat4("_Projection", _camera.ProjectionMatrix);
        var view = new Matrix4(new Matrix3(_camera.ViewMatrix)); // remove translation from the view matrix
        _cubemapShader.SetMat4("_View", view);

        _cubemapPipeline.Bind();
        Renderer.RenderGeometry(_cubemapPipeline, _cubemapVertexBuffer, _cubemapIndexBuffer);
        _cubemap.Unbind();
    }

    public override void OnGUI()
    {
        ImGuiNET.ImGui.Begin("Properties");

        ImGuiNET.ImGui.Text($"Press 'P' or 'Escape' to toggle state.");
        ImGuiNET.ImGui.Text($"State: {(_paused ? "Paused" : "Playing")}");

        ImGuiNET.ImGui.Spacing();

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
        if (e.KeyCode == (int)Keycode.P || e.KeyCode == (int)Keycode.ESCAPE)
        {
            _paused = !_paused;
            if (_paused)
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