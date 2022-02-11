using DaemonEngine.Graphics.Renderer;
using DaemonEngine.OpenGL.DllImport;
using DaemonEngine.OpenGL.DllImport.Enums;
using Serilog;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLIndexBuffer : IndexBufferBase
{
    private readonly uint _id;

    public OpenGLIndexBuffer(ILogger logger, int count, uint[] indices)
        : base(logger)
    {
        Count = count;

        uint[] ids = new uint[1];
        GL.GenBuffers(1, ref ids);
        _id = ids[0];

        GL.BindBuffer(GLConstants.GL_ELEMENT_ARRAY_BUFFER, _id);
        GL.BufferData(GLConstants.GL_ELEMENT_ARRAY_BUFFER, count * sizeof(uint), indices, GLBufferUsage.StaticDraw);
    }

    public override void Bind()
    {
        GL.BindBuffer(GLConstants.GL_ELEMENT_ARRAY_BUFFER, _id);
    }

    public override void Unbind()
    {
        GL.BindBuffer(GLConstants.GL_ELEMENT_ARRAY_BUFFER, 0);
    }
}
