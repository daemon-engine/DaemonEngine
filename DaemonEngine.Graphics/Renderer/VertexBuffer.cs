using DaemonEngine.Extensions.OpenGL;
using DaemonEngine.Extensions.OpenGL.Enums;
using Serilog;

namespace DaemonEngine.Graphics.Renderer;

public class VertexBuffer : GLBase
{
    private readonly uint _id;

    public VertexBuffer(ILogger logger, int size, float[] vertices)
        : base(logger)
    {
        uint[] ids = new uint[1];
        GL.GenBuffers(1, ref ids);
        _id = ids[0];

        GL.BindBuffer(GLConstants.GL_ARRAY_BUFFER, _id);
        GL.BufferData(GLConstants.GL_ARRAY_BUFFER, size, vertices, GLBufferUsage.StaticDraw);
    }

    public override void Bind()
    {
        GL.BindBuffer(GLConstants.GL_ARRAY_BUFFER, _id);
    }

    public override void Unbind()
    {
        GL.BindBuffer(GLConstants.GL_ARRAY_BUFFER, 0);
    }
}
