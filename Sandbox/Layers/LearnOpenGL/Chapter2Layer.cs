using DaemonEngine.Application;
using DaemonEngine.Core;
using DaemonEngine.Core.Inputs;
using DaemonEngine.Core.Layer;
using DaemonEngine.EventSystem;
using DaemonEngine.EventSystem.Events.Key;
using DaemonEngine.EventSystem.Events.Window;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Enums;
using Microsoft.Extensions.DependencyInjection;
using System.Numerics;

namespace Sandbox.Layers.LearnOpenGL;

internal class Chapter2Layer : LayerBase
{
    IShader _lightingShader;
    IPipeline _lightingPipeline;

    IShader _lightObjectShader;
    IPipeline _lightObjectPipeline;

    IVertexBuffer _vertexBuffer;
    IIndexBuffer _indexBuffer;

    ITexture _container;
    ITexture _containerSpecular;

    FPSCamera _camera;

    private readonly IApplication _application;
    private readonly ICursor _cursor;

    Vector3[] _cubePositions = new Vector3[10]
    {
        new Vector3( 0.0f,  0.0f,  0.0f),
        new Vector3( 2.0f,  5.0f, -15.0f),
        new Vector3(-1.5f, -2.2f, -2.5f),
        new Vector3(-3.8f, -2.0f, -12.3f),
        new Vector3( 2.4f, -0.4f, -3.5f),
        new Vector3(-1.7f,  3.0f, -7.5f),
        new Vector3( 1.3f, -2.0f, -2.5f),
        new Vector3( 1.5f,  2.0f, -2.5f),
        new Vector3( 1.5f,  0.2f, -1.5f),
        new Vector3(-1.3f,  1.0f, -1.5f)
    };

    Vector3[] _pointLightPositions = {
        new Vector3( 0.7f,  0.2f,  2.0f),
        new Vector3( 2.3f, -3.3f, -4.0f),
        new Vector3(-4.0f,  2.0f, -12.0f),
        new Vector3( 0.0f,  0.0f, -3.0f)
    };

    public Vector3 CopperAmbient = new(0.19125f, 0.0735f, 0.0225f);
    public Vector3 CopperDiffuse = new(0.7038f, 0.27048f, 0.0828f);
    public Vector3 CopperSpecular = new(0.256777f, 0.137622f, 0.086014f);
    public float CopperShininess = 0.1f;

    public Chapter2Layer(string name, IServiceProvider serviceProvider)
        : base(name, serviceProvider)
    {
        _application = ServiceProvider.GetService<IApplication>();
        _cursor = ServiceProvider.GetService<ICursor>();
    }

    public override void OnStart()
    {
        _cursor.Disable();

        _camera = new FPSCamera(45.0f, Window.AspectRatio);

        _container = GraphicsFactory.CreateTexture("Assets/Textures/container2.png");
        _containerSpecular = GraphicsFactory.CreateTexture("Assets/Textures/container2_specular.png");

        _lightingShader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Chapter2/MultipleLightCasters.shader");
        _lightObjectShader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Chapter2/1.LightCube.shader");

        var layout = new BufferLayout(new List<BufferElement>
        {
            new BufferElement("POSITION", ShaderDataType.Float3),
            new BufferElement("NORMAL", ShaderDataType.Float3),
            new BufferElement("TEXCOORD", ShaderDataType.Float2)
        });

        // Cube
        var cubeVertices = Builder.GenerateCubeVertices();
        var cubeIndices = Builder.GenerateCubeIndices();

        _lightingPipeline = GraphicsFactory.CreatePipeline(_lightingShader, layout);
        _lightObjectPipeline = GraphicsFactory.CreatePipeline(_lightObjectShader, layout);

        _vertexBuffer = GraphicsFactory.CreateVertexBuffer(192 * sizeof(float), cubeVertices);
        _indexBuffer = GraphicsFactory.CreateIndexBuffer(36, cubeIndices);

        _lightingShader.Bind();
        _lightingShader.SetInt("_Material.diffuse", 0);
        _lightingShader.SetInt("_Material.specular", 1);

        Window.Maximize();
    }

    public override void OnShutdown()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        if (s_MovedDisabled)
        {
            _camera.Update(deltaTime);
        }

        Renderer.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);
        Renderer.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);

        _lightingShader.Bind();
        _lightingShader.SetMat4("_Model", Matrix4x4.Identity);
        _lightingShader.SetMat4("_View", _camera.ViewMatrix);
        _lightingShader.SetMat4("_Projection", _camera.ProjectionMatrix);
        _lightingShader.SetFloat3("_ViewPos", _camera.Position.X, _camera.Position.Y, _camera.Position.Z);
        _lightingShader.SetFloat("_Material.shininess", 32.0f);

        _lightingShader.SetFloat3("_DirLight.direction", -0.2f, -1.0f, -0.3f);
        _lightingShader.SetFloat3("_DirLight.ambient", 0.05f, 0.05f, 0.05f);
        _lightingShader.SetFloat3("_DirLight.diffuse", 0.4f, 0.4f, 0.4f);
        _lightingShader.SetFloat3("_DirLight.specular", 0.5f, 0.5f, 0.5f);

        for (int i = 0; i < _pointLightPositions.Length; i++)
        {
            SetPointLight(i);
        }

        // spotLight
        _lightingShader.SetFloat3("_SpotLight.position", _camera.Position.X, _camera.Position.Y, _camera.Position.Z);
        _lightingShader.SetFloat3("_SpotLight.direction", _camera.Front.X, _camera.Front.Y, _camera.Front.Z);
        _lightingShader.SetFloat3("_SpotLight.ambient", 0.0f, 0.0f, 0.0f);
        _lightingShader.SetFloat3("_SpotLight.diffuse", 1.0f, 1.0f, 1.0f);
        _lightingShader.SetFloat3("_SpotLight.specular", 1.0f, 1.0f, 1.0f);
        _lightingShader.SetFloat("_SpotLight.constant", 1.0f);
        _lightingShader.SetFloat("_SpotLight.linear", 0.09f);
        _lightingShader.SetFloat("_SpotLight.quadratic", 0.032f);
        _lightingShader.SetFloat("_SpotLight.cutOff", (float)Math.Cos(12.5f * 0.01745329251f));
        _lightingShader.SetFloat("_SpotLight.outerCutOff", (float)Math.Cos(15.0f * 0.01745329251f));

        var model = Matrix4x4.Identity;

        _container.Bind();
        _containerSpecular.Bind(1);

        for (int i = 0; i < _cubePositions.Length; i++)
        {
            float angle = 20.0f * i;

            model = Matrix4x4.Identity
                * Matrix4x4.CreateRotationX(angle * 0.01745329251f)
                * Matrix4x4.CreateRotationY(angle * 0.01745329251f)
                * Matrix4x4.CreateRotationZ(angle * 0.01745329251f)
                * Matrix4x4.CreateTranslation(_cubePositions[i]);

            _lightingShader.SetMat4("_Model", model);
            Renderer.RenderGeometry(_lightingPipeline, _vertexBuffer, _indexBuffer);
        }

        // Point light cubes
        _lightObjectShader.Bind();
        _lightObjectShader.SetMat4("_Model", Matrix4x4.Identity);
        _lightObjectShader.SetMat4("_View", _camera.ViewMatrix);
        _lightObjectShader.SetMat4("_Projection", _camera.ProjectionMatrix);

        for (int i = 0; i < _pointLightPositions.Length; i++)
        {
            model = Matrix4x4.Identity
                * Matrix4x4.CreateScale(0.2f)
                * Matrix4x4.CreateTranslation(_pointLightPositions[i]);

            _lightObjectShader.SetMat4("_Model", model);

            Renderer.RenderGeometry(_lightObjectPipeline, _vertexBuffer, _indexBuffer);
        }
    }

    private void SetPointLight(int index)
    {
        _lightingShader.Bind();
        _lightingShader.SetFloat3($"_PointLights[{index}].position", _pointLightPositions[index].X, _pointLightPositions[index].Y, _pointLightPositions[index].Z);
        _lightingShader.SetFloat3($"_PointLights[{index}].ambient", 0.05f, 0.05f, 0.05f);
        _lightingShader.SetFloat3($"_PointLights[{index}].diffuse", 0.8f, 0.8f, 0.8f);
        _lightingShader.SetFloat3($"_PointLights[{index}].specular", 1.0f, 1.0f, 1.0f);
        _lightingShader.SetFloat($"_PointLights[{index}].constant", 1.0f);
        _lightingShader.SetFloat($"_PointLights[{index}].linear", 0.09f);
        _lightingShader.SetFloat($"_PointLights[{index}].quadratic", 0.032f);
    }

    public override void OnGUI()
    {
        var io = ImGuiNET.ImGui.GetIO();

        ImGuiNET.ImGui.Begin("Performace");

        ImGuiNET.ImGui.Text($"State: {(s_MovedDisabled ? "Playing" : "Paused")}");
        ImGuiNET.ImGui.Text($"Delta Time: {(io.DeltaTime * 1000.0f):f4}ms/frame");
        ImGuiNET.ImGui.Text($"FPS: {1.0f / io.DeltaTime:f1}");

        ImGuiNET.ImGui.End();

        ImGuiNET.ImGui.ShowDemoWindow();
    }

    public override void OnEvent(IEvent e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowResizeEvent>(OnWindowResizeEvent);
        dispatcher.Dispatch<KeyPressedEvent>(OnKeyPressedEvent);
    }

    private static bool s_Maximized = true;
    private static bool s_MovedDisabled = true;

    private bool OnKeyPressedEvent(KeyPressedEvent e)
    {
        if (e.KeyCode == (int)Keycode.ESCAPE)
        {
            _application.Stop();
        }

        if (e.KeyCode == (int)Keycode.O)
        {
            s_MovedDisabled = !s_MovedDisabled;
            if (s_MovedDisabled)
            {
                _cursor.Disable();
            }
            else
            {
                _cursor.Show();
            }
        }

        if (e.KeyCode == (int)Keycode.P)
        {
            s_Maximized = !s_Maximized;
            if (s_Maximized)
            {
                Window.Maximize();
            }
            else
            {
                Window.Restore();
            }
        }
        return true;
    }

    private bool OnWindowResizeEvent(WindowResizeEvent e)
    {
        _camera.SetViewport(e.Width, e.Height);
        return true;
    }
}
