using DaemonEngine.Core.Layer;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Data;
using DaemonEngine.Mathematics;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox.Layers;
internal class FramebufferTestLayer : LayerBase
{
    Model _sphere;

    FPSCamera _camera;
    IFramebuffer _framebuffer;
    IShader _shader;

    IShader _fullscreenQuadShader;
    IPipeline _fullscreenQuadPipeline;
    IVertexBuffer _fullscreenQuadVertexBuffer;
    IIndexBuffer _fullscreenQuadIndexBuffer;

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
        _fullscreenQuadShader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/FullscreenQuadGrayScale.shader");

        _fullscreenQuadShader.Bind();
        _fullscreenQuadShader.SetInt("_ScreenTexture", 0);

        var framebufferOptions = new FramebufferOptions
        {
            Width = Window.Width,
            Height = Window.Height,
            ClearColor = new Vector4(0.3f, 0.5f, 0.85f, 1.0f),
            Attachments = new List<FramebufferAttachmentType>
            {
                FramebufferAttachmentType.RGBA8,
                FramebufferAttachmentType.DEPTH24STENCIL8
            }
        };
        _framebuffer = GraphicsFactory.CreateFramebuffer(framebufferOptions);

        _sphere = new Model(meshFactory, _shader, "Assets/Models/Sphere/sphere.obj");

        var bufferLayout = new BufferLayout(new List<BufferElement>()
        {
            new BufferElement("POSITION", ShaderDataType.Float2),
            new BufferElement("TEXCOORD", ShaderDataType.Float2)
        });
        _fullscreenQuadPipeline = GraphicsFactory.CreatePipeline(_fullscreenQuadShader, bufferLayout);

        var vertices = Builder.GenerateFullscreenQuadVertices();
        var indices = Builder.GenerateFullscreenQuadIndices();

        _fullscreenQuadVertexBuffer = GraphicsFactory.CreateVertexBuffer(vertices.Length * sizeof(float), vertices);
        _fullscreenQuadIndexBuffer = GraphicsFactory.CreateIndexBuffer(indices.Length, indices);
    }

    public override void OnShutdown()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        _framebuffer.Bind();
        _framebuffer.Clear();

        _shader.Bind();
        _shader.SetMat4("_Model", Matrix4.Identity);
        _shader.SetMat4("_View", _camera.ViewMatrix);
        _shader.SetMat4("_Projection", _camera.ProjectionMatrix);
        Renderer.RenderMesh(_sphere.Mesh);

        _shader.Bind();
        _shader.SetMat4("_Model", Matrix4.Identity * Matrix4.Translate(new Vector3(3.0f, 0.0f, 0.0f)));
        _shader.SetMat4("_View", _camera.ViewMatrix);
        _shader.SetMat4("_Projection", _camera.ProjectionMatrix);
        Renderer.RenderMesh(_sphere.Mesh);

        _framebuffer.Unbind();

        Renderer.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
        Renderer.Clear(ClearMask.ColorBufferBit);

        var colorAttachment = _framebuffer.GetColorAttachment(0);
        Renderer.SubmitFullscreenQuad(colorAttachment, _fullscreenQuadPipeline, _fullscreenQuadVertexBuffer, _fullscreenQuadIndexBuffer);
    }

    public override void OnGUI()
    {
        ImGuiNET.ImGui.Begin("Framebuffer Test");

        var ptr = (IntPtr)_framebuffer.GetColorAttachment(0);
        ImGuiNET.ImGui.Image(ptr, new System.Numerics.Vector2(800, 600));

        ImGuiNET.ImGui.End();
    }
}
