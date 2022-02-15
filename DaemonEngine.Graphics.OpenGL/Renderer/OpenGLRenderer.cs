using DaemonEngine.Extensions.Runtime;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.OpenGL.Helpers;
using DaemonEngine.OpenGL.DllImport;
using DaemonEngine.OpenGL.DllImport.Enums;
using Serilog;
using DaemonEngine.Mathematics;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

public struct CameraData
{
    public float[] ProjectionMatrix { get; set; }    // Matrix4 (4 * 4 * sizeof(float)) = 64 bytes
    public float[] ViewMatrix { get; set; }          // Matrix4 (4 * 4 * sizeof(float)) = 64 bytes
    public float[] ViewPosition { get; set; }        // Vector3 (3 * sizeof(float)) = 12 bytes
}

internal class OpenGLRenderer : RendererBase
{
    private CameraData _cameraData;

    public OpenGLRenderer(ILogger logger)
        : base(logger)
    {
    }

    protected IUniformBuffer CameraUniformBuffer { get; set; }

    public override void Initialize()
    {
        GL.Enable(GLCapabilities.DepthTest);

        _cameraData = new CameraData();

        CameraUniformBuffer = new OpenGLUniformBuffer(140, 0);
    }

    public override void Shutdown()
    {
    }

    public override void BeginScene(ICamera camera)
    {
        _cameraData.ProjectionMatrix = camera.ProjectionMatrix.ToFloatArray();
        _cameraData.ViewMatrix = camera.ViewMatrix.ToFloatArray();
        _cameraData.ViewPosition = camera.Position.ToFloatArray();

        CameraUniformBuffer.SetData(_cameraData.ProjectionMatrix, (4 * 4) * sizeof(float));
        CameraUniformBuffer.SetData(_cameraData.ViewMatrix, (4 * 4) * sizeof(float), 64);
        CameraUniformBuffer.SetData(_cameraData.ViewPosition, 3 * sizeof(float), 128);
    }

    public override void EndScene()
    {
    }

    public override void RenderGeometry(IPipeline pipeline, IVertexBuffer vertexBuffer, IIndexBuffer indexBuffer, int indexCount = 0)
    {
        Throw.IfNull(pipeline, nameof(pipeline));
        Throw.IfNull(vertexBuffer, nameof(vertexBuffer));
        Throw.IfNull(indexBuffer, nameof(indexBuffer));

        vertexBuffer.Bind();
        pipeline.Bind();
        indexBuffer.Bind();

        var count = indexCount == 0 ? indexBuffer.Count : indexCount;
        GL.DrawElements(GLConstants.GL_TRIANGLES, count, GLConstants.GL_UNSIGNED_INT);
        GL.BindTexture(GLConstants.GL_TEXTURE_2D, 0);
    }

    public override void RenderMesh(IMesh mesh)
    {
        Throw.IfNull(mesh, nameof(mesh));
        var count = mesh.GetIndexBufferCount();

        mesh.Bind();
        CameraUniformBuffer.Bind();

        GL.DrawElements(GLConstants.GL_TRIANGLES, count, GLConstants.GL_UNSIGNED_INT);
        GL.BindTexture(GLConstants.GL_TEXTURE_2D, 0);
    }

    public override void SubmitFullscreenQuad(uint colorAttachment, IPipeline pipeline, IVertexBuffer vertexBuffer, IIndexBuffer indexBuffer)
    {
        Throw.IfNull(pipeline, nameof(pipeline));
        Throw.IfNull(vertexBuffer, nameof(vertexBuffer));
        Throw.IfNull(indexBuffer, nameof(indexBuffer));

        GL.Disable(GLCapabilities.DepthTest);

        vertexBuffer.Bind();
        pipeline.Bind();
        indexBuffer.Bind();

        GL.BindTexture(GLConstants.GL_TEXTURE_2D, colorAttachment);
        GL.DrawElements(GLConstants.GL_TRIANGLES, indexBuffer.Count, GLConstants.GL_UNSIGNED_INT);
        GL.BindTexture(GLConstants.GL_TEXTURE_2D, 0);
    }

    public override void SetViewport(int x, int y, int width, int height)
    {
        GL.Viewport(x, y, width, height);
    }

    public override void Clear(ClearMask clearMask)
    {
        GL.Clear(OpenGLHelper.ClearMaskToOpenGLType(clearMask));
    }

    public override void ClearColor(float red, float green, float blue, float alpha)
    {
        GL.ClearColor(red, green, blue, alpha);
    }
}
