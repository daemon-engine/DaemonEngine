using DaemonEngine.Core.Layer;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Data;
using DaemonEngine.Mathematics;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox.Layers;

internal class CubemapTestLayer : LayerBase
{
    ICubemap _cubemap;

    IShader _cubemapShader;
    IPipeline _cubemapPipeline;
    IVertexBuffer _cubemapVertexBuffer;
    IIndexBuffer _cubemapIndexBuffer;

    IShader _shader;
    Model _sphere;
    FPSCamera _camera;

    public CubemapTestLayer(string name, IServiceProvider serviceProvider)
        : base(name, serviceProvider)
    {
    }

    public override void OnStart()
    {
        _camera = new FPSCamera(60.0f, Window.AspectRatio);

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
        var bufferLayout = new BufferLayout(new List<BufferElement>
        {
            new BufferElement("POSITION", ShaderDataType.Float3)
        });

        _cubemapPipeline = GraphicsFactory.CreatePipeline(_cubemapShader, bufferLayout);

        var vertices = Builder.GenerateCubemapVertices();
        var indices = Builder.GenerateCubemapIndices();

        _cubemapVertexBuffer = GraphicsFactory.CreateVertexBuffer(vertices.Length * sizeof(float), vertices);
        _cubemapIndexBuffer = GraphicsFactory.CreateIndexBuffer(indices.Length, indices);

        var meshFactory = ServiceProvider.GetRequiredService<IMeshFactory>();
        _shader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Chapter3/Basic.shader");
        _sphere = new Model(meshFactory, _shader, "Assets/Models/Sphere/sphere.obj");
    }

    public override void OnShutdown()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        _camera.Update(deltaTime);

        Renderer.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);

        _shader.Bind();
        _shader.SetMat4("_Model", Matrix4.Identity);
        _shader.SetMat4("_Projection", _camera.ProjectionMatrix);
        _shader.SetMat4("_View", _camera.ViewMatrix);
        Renderer.RenderMesh(_sphere.Mesh);

        _cubemap.Bind();
        _cubemapShader.Bind();
        _cubemapShader.SetMat4("_Projection", _camera.ProjectionMatrix);
        var view = new Matrix4(new Matrix3(_camera.ViewMatrix)); // remove translation from the view matrix
        _cubemapShader.SetMat4("_View", view);

        _cubemapPipeline.Bind();
        Renderer.RenderGeometry(_cubemapPipeline, _cubemapVertexBuffer, _cubemapIndexBuffer);
        _cubemap.Unbind();
    }
}
