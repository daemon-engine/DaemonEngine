using DaemonEngine.Graphics.Renderer;
using DaemonEngine.OpenGL.DllImport;
using DaemonEngine.OpenGL.DllImport.Enums;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLUniformBuffer : UniformBufferBase
{
    private readonly uint _id;

    public OpenGLUniformBuffer(int size, uint binding)
    {
        _id = GL.GenBuffer();

        GL.BindBuffer(GLConstants.GL_UNIFORM_BUFFER, _id);

        GL.BufferData1(GLConstants.GL_UNIFORM_BUFFER, size, null, GLBufferUsage.DynamicDraw);

        GL.BindBufferBase(GLConstants.GL_UNIFORM_BUFFER, binding, _id);
    }

    public override void SetData(float[] data, int size, int offset)
    {
        GL.BindBuffer(GLConstants.GL_UNIFORM_BUFFER, _id);

        GL.BufferSubData(GLConstants.GL_UNIFORM_BUFFER, offset, size, data);
    }

    public override void Bind()
    {
        GL.BindBuffer(GLConstants.GL_UNIFORM_BUFFER, _id);
    }

    public override void Unbind()
    {
        GL.BindBuffer(GLConstants.GL_UNIFORM_BUFFER, 0);
    }
}
