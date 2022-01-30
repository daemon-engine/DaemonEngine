using DaemonEngine.Extensions.OpenGL;
using DaemonEngine.Extensions.OpenGL.Enums;
using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLVertexBuffer : IVertexBuffer
{
    private readonly uint _id;

    public OpenGLVertexBuffer(int size, float[] vertices)
    {
        uint[] ids = new uint[1];
        GL.GenBuffers(1, ref ids);
        _id = ids[0];

        GL.BindBuffer(GLConstants.GL_ARRAY_BUFFER, _id);
        GL.BufferData(GLConstants.GL_ARRAY_BUFFER, size, vertices, GLBufferUsage.StaticDraw);
    }

    public void Bind()
    {
        GL.BindBuffer(GLConstants.GL_ARRAY_BUFFER, _id);
    }

    public void Unbind()
    {
        GL.BindBuffer(GLConstants.GL_ARRAY_BUFFER, 0);
    }
}

