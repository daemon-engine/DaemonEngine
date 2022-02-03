using DaemonEngine.Application;
using DaemonEngine.Core;
using DaemonEngine.Core.Layer;
using DaemonEngine.EventSystem;
using DaemonEngine.EventSystem.Events.Key;
using DaemonEngine.EventSystem.Events.Window;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.OpenGL.DllImport.Enums;
using DaemonEngine.Windows.Inputs;
using Microsoft.Extensions.DependencyInjection;
using System.Numerics;

namespace Sandbox.Layers.LearnOpenGL;

internal class Chapter2Layer : LayerBase
{
    IPipeline _quadPipeline;
    IVertexBuffer _quadVertexBuffer;
    IIndexBuffer _quadIndexBuffer;

    IShader _lightingShader;
    IPipeline _lightingPipeline;

    IShader _lightObjectShader;
    IPipeline _lightObjectPipeline;

    IVertexBuffer _vertexBuffer;
    IIndexBuffer _indexBuffer;

    FPSCamera _camera;

    private readonly IApplication _application;

    public Chapter2Layer(string name, IServiceProvider serviceProvider) 
        : base(name, serviceProvider)
    {
        _application = ServiceProvider.GetService<IApplication>();
    }

    public override void OnStart()
    {
        var cursor = ServiceProvider.GetService<ICursor>();
        cursor.Disable();

        var input = ServiceProvider.GetService<IInput>();
        _camera = new FPSCamera(45.0f, Window.AspectRatio, input);

        _lightingShader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Chapter2/Materials.shader");
        _lightObjectShader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Chapter2/1.LightCube.shader");

        var layout = new BufferLayout(new List<BufferElement>
        {
            new BufferElement("POSITION", ShaderDataType.Float3),
            new BufferElement("NORMAL", ShaderDataType.Float3),
            new BufferElement("TEXCOORD", ShaderDataType.Float2)
        });

        var quadVertices = Builder.GenerateQuadVertices();
        var quadIndices = Builder.GenerateQuadIndices();
        _quadPipeline = GraphicsFactory.CreatePipeline(_lightingShader, layout);

        _quadVertexBuffer = GraphicsFactory.CreateVertexBuffer(32 * sizeof(float), quadVertices);
        _quadIndexBuffer = GraphicsFactory.CreateIndexBuffer(6, quadIndices);

        // Cube
        var cubeVertices = Builder.GenerateCubeVertices();
        var cubeIndices = Builder.GenerateCubeIndices();

        _lightingPipeline = GraphicsFactory.CreatePipeline(_lightingShader, layout);
        _lightObjectPipeline = GraphicsFactory.CreatePipeline(_lightObjectShader, layout);

        _vertexBuffer = GraphicsFactory.CreateVertexBuffer(192 * sizeof(float), cubeVertices);
        _indexBuffer = GraphicsFactory.CreateIndexBuffer(36, cubeIndices);

        Window.Maximize();
    }

    public override void OnShutdown()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        var lightPosition = new Vector3(1.2f, 1.0f, 0.2f);
        _camera.Update(deltaTime);

        var model = Matrix4x4.Identity;

        Renderer.Clear(GLClearMask.ColorBufferBit | GLClearMask.DepthBufferBit);
        Renderer.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);

        DrawQuad(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(-90.0f, 0.0f, 0.0f), 5.0f, _camera.Position, lightPosition);
        DrawQuad(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(90.0f, 0.0f, 0.0f), 5.0f, _camera.Position, lightPosition);
        //DrawQuad(new Vector3(-90.0f, 0.0f, 0.0f), 5.0f, _camera.Position, lightPosition);

        _lightingShader.Bind();
        _lightingShader.SetMat4("_View", _camera.ViewMatrix);
        _lightingShader.SetMat4("_Projection", _camera.ProjectionMatrix);

        model = Matrix4x4.Identity;
        model *= Matrix4x4.CreateRotationX(-90.0f * (3.14f / 180.0f));
        model *= Matrix4x4.CreateScale(5.0f);
        _lightingShader.SetMat4("_Model", model);

        _lightingShader.SetFloat3("_Material.ambient", 1.0f, 0.5f, 0.31f);
        _lightingShader.SetFloat3("_Material.diffuse", 1.0f, 0.5f, 0.31f);
        _lightingShader.SetFloat3("_Material.specular", 0.5f, 0.5f, 0.5f);
        _lightingShader.SetFloat("_Material.shininess", 32.0f);

        _lightingShader.SetFloat3("_Light.ambient", 0.2f, 0.2f, 0.2f);
        _lightingShader.SetFloat3("_Light.diffuse", 0.5f, 0.5f, 0.5f);
        _lightingShader.SetFloat3("_Light.specular", 1.0f, 1.0f, 1.0f);

        _lightingShader.SetFloat3("_LightPos", lightPosition.X, lightPosition.Y, lightPosition.Z);
        _lightingShader.SetFloat3("_ViewPos", _camera.Position.X, _camera.Position.Y, _camera.Position.Z);
        Renderer.RenderGeometry(_quadPipeline, _quadVertexBuffer, _quadIndexBuffer);

        _lightingShader.Bind();
        _lightingShader.SetMat4("_View", _camera.ViewMatrix);
        _lightingShader.SetMat4("_Projection", _camera.ProjectionMatrix);

        model = Matrix4x4.Identity;
        _lightingShader.SetMat4("_Model", model);

        _lightingShader.SetFloat3("_Material.ambient", 1.0f, 0.5f, 0.31f);
        _lightingShader.SetFloat3("_Material.diffuse", 1.0f, 0.5f, 0.31f);
        _lightingShader.SetFloat3("_Material.specular", 0.5f, 0.5f, 0.5f);
        _lightingShader.SetFloat("_Material.shininess", 32.0f);

        _lightingShader.SetFloat3("_Light.ambient", 0.2f, 0.2f, 0.2f);
        _lightingShader.SetFloat3("_Light.diffuse", 0.5f, 0.5f, 0.5f);
        _lightingShader.SetFloat3("_Light.specular", 1.0f, 1.0f, 1.0f);

        _lightingShader.SetFloat3("_LightPos", lightPosition.X, lightPosition.Y, lightPosition.Z);
        _lightingShader.SetFloat3("_ViewPos", _camera.Position.X, _camera.Position.Y, _camera.Position.Z);
        Renderer.RenderGeometry(_lightingPipeline, _vertexBuffer, _indexBuffer);

        _lightObjectShader.Bind();
        _lightObjectShader.SetMat4("_View", _camera.ViewMatrix);
        _lightObjectShader.SetMat4("_Projection", _camera.ProjectionMatrix);

        model = Matrix4x4.Identity;
        model *= Matrix4x4.CreateScale(0.2f, Vector3.Zero);
        model *= Matrix4x4.CreateTranslation(lightPosition);
        _lightObjectShader.SetMat4("_Model", model);
        Renderer.RenderGeometry(_lightObjectPipeline, _vertexBuffer, _indexBuffer);
    }

    private void DrawQuad(Vector3 position, Vector3 rotation, float scale, Vector3 cameraPosition, Vector3 lightPosition)
    {
        Matrix4x4 model = Matrix4x4.Identity;
        model *= Matrix4x4.CreateTranslation(position);
        model *= Matrix4x4.CreateScale(scale, position);
        model *= Matrix4x4.CreateRotationX(rotation.X * (3.14f / 180.0f), position);
        model *= Matrix4x4.CreateRotationX(rotation.Y * (3.14f / 180.0f), position);
        model *= Matrix4x4.CreateRotationX(rotation.Z * (3.14f / 180.0f), position);
        _lightingShader.SetMat4("_Model", model);

        _lightingShader.SetFloat3("_Material.ambient", 1.0f, 0.5f, 0.31f);
        _lightingShader.SetFloat3("_Material.diffuse", 1.0f, 0.5f, 0.31f);
        _lightingShader.SetFloat3("_Material.specular", 0.5f, 0.5f, 0.5f);
        _lightingShader.SetFloat("_Material.shininess", 32.0f);

        _lightingShader.SetFloat3("_Light.ambient", 0.2f, 0.2f, 0.2f);
        _lightingShader.SetFloat3("_Light.diffuse", 0.5f, 0.5f, 0.5f);
        _lightingShader.SetFloat3("_Light.specular", 1.0f, 1.0f, 1.0f);

        _lightingShader.SetFloat3("_LightPos", lightPosition.X, lightPosition.Y, lightPosition.Z);
        _lightingShader.SetFloat3("_ViewPos", cameraPosition.X, cameraPosition.Y, cameraPosition.Z);
        Renderer.RenderGeometry(_quadPipeline, _quadVertexBuffer, _quadIndexBuffer);
    }

    public override void OnEvent(IEvent e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowResizeEvent>(OnWindowResizeEvent);
        dispatcher.Dispatch<KeyPressedEvent>(OnKeyPressedEvent);
    }

    private static bool s_Maximized = true;

    private bool OnKeyPressedEvent(KeyPressedEvent e)
    {
        if(e.KeyCode == (int)Keycode.ESCAPE)
        {
            _application.Stop();
        }

        if(e.KeyCode == (int)Keycode.P)
        {
            s_Maximized = !s_Maximized;
            if(s_Maximized)
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
