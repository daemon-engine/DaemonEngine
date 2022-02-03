using DaemonEngine.Graphics.Renderer;
using DaemonEngine.OpenGL.DllImport;
using DaemonEngine.OpenGL.DllImport.Enums;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLIndexBuffer : IIndexBuffer
{
    private readonly uint _id;

    public OpenGLIndexBuffer(int count, uint[] indices)
    {
        Count = count;

        uint[] ids = new uint[1];
        GL.GenBuffers(1, ref ids);
        _id = ids[0];

        GL.BindBuffer(GLConstants.GL_ELEMENT_ARRAY_BUFFER, _id);
        GL.BufferData(GLConstants.GL_ELEMENT_ARRAY_BUFFER, count * sizeof(uint), indices, GLBufferUsage.StaticDraw);
    }

    public int Count { get; }

    public void Bind()
    {
        GL.BindBuffer(GLConstants.GL_ELEMENT_ARRAY_BUFFER, _id);
    }

    public void Unbind()
    {
        GL.BindBuffer(GLConstants.GL_ELEMENT_ARRAY_BUFFER, 0);
    }
}
