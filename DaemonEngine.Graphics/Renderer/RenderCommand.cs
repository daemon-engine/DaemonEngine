using DaemonEngine.Extensions.OpenGL;
using DaemonEngine.Extensions.OpenGL.Enums;

namespace DaemonEngine.Graphics.Renderer;

public static class RenderCommand
{
    public static void DrawIndexed(Pipeline pipeline, IndexBuffer indexBuffer, int indexCount = 0)
    {
        pipeline.Bind();
        indexBuffer.Bind();

        var count = indexCount != 0 ? indexBuffer.Count : indexCount;
        GL.DrawElements(GLConstants.GL_TRIANGLES, count, GLConstants.GL_UNSIGNED_INT);
    }

    public static void ClearColor(float red, float green, float blue, float alpha)
    {
        GL.ClearColor(red, green, blue, alpha);
    }

    public static void Clear()
    {
        GL.Clear(GLClearMask.ColorBufferBit);
    }
}
