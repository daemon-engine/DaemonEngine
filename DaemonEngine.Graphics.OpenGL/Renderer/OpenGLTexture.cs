using DaemonEngine.Extensions.OpenGL;
using DaemonEngine.Extensions.SixLabors.ImageSharp;
using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLTexture : ITexture
{
    private readonly uint _id;

    public OpenGLTexture(string filepath)
    {
        var imageData = ImageLoader.Load(filepath);

        uint[] ids = new uint[1];
        GL.GenTextures(1, ref ids);
        _id = ids[0];
        GL.BindTexture(GLConstants.GL_TEXTURE_2D, _id);

        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_S, GLConstants.GL_REPEAT); 
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_T, GLConstants.GL_REPEAT);
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_MIN_FILTER, GLConstants.GL_LINEAR_MIPMAP_LINEAR);
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_MAG_FILTER, GLConstants.GL_LINEAR);

        GL.TexImage2D(GLConstants.GL_TEXTURE_2D, 0, GLConstants.GL_RGBA8, imageData.Width, imageData.Height, 0, GLConstants.GL_RGBA, GLConstants.GL_UNSIGNED_BYTE, imageData.Pixels);
        GL.GenerateMipmap(GLConstants.GL_TEXTURE_2D);
    }

    public void Bind(int slot = 0)
    {
        GL.BindTexture(GLConstants.GL_TEXTURE_2D, _id);
    }

    public void Unbind()
    {
        GL.BindTexture(GLConstants.GL_TEXTURE_2D, 0);
    }
}
