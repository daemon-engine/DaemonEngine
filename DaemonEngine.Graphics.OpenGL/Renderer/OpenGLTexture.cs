using DaemonEngine.Extensions.SixLabors.ImageSharp;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.OpenGL.DllImport;
using Serilog;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLTexture : TextureBase, ITexture
{
    private readonly uint _id;

    public OpenGLTexture(ILogger logger, string filepath)
        : base(logger)
    {
        var imageData = ImageLoader.Load(filepath, true);

        uint[] ids = new uint[1];
        GL.GenTextures(1, ref ids);
        _id = ids[0];
        GL.BindTexture(GLConstants.GL_TEXTURE_2D, _id);

        SetTextureParameters();

        GL.TexImage2D(GLConstants.GL_TEXTURE_2D, 0, GLConstants.GL_RGBA8, imageData.Width, imageData.Height, 0, GLConstants.GL_RGBA, GLConstants.GL_UNSIGNED_BYTE, imageData.Pixels);
        GL.GenerateMipmap(GLConstants.GL_TEXTURE_2D);
    }
    public OpenGLTexture(ILogger logger, int width, int height)
        : base(logger)
    {
        Width = width;
        Height = height;

        uint[] ids = new uint[1];
        GL.GenTextures(1, ref ids);
        _id = ids[0];
        GL.BindTexture(GLConstants.GL_TEXTURE_2D, _id);

        GL.TextureStorage2D(_id, 1, GLConstants.GL_RGBA8, Width, Height);

        SetTextureParameters();
    }

    protected int Width { get; private set; }
    protected int Height { get; private set; }

    private void SetTextureParameters()
    {
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_S, GLConstants.GL_REPEAT);
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_T, GLConstants.GL_REPEAT);
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_MIN_FILTER, GLConstants.GL_LINEAR);
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_MAG_FILTER, GLConstants.GL_LINEAR);
    }

    public override uint GetId()
    {
        return _id;
    }

    public override void SetData(int size, IntPtr data)
    {
        GL.TextureSubImage2D(_id, 0, 0, 0, Width, Height, GLConstants.GL_RGBA, GLConstants.GL_UNSIGNED_BYTE, data);
        GL.GenerateMipmap(GLConstants.GL_TEXTURE_2D);
    }

    public override void Bind(uint slot = 0)
    {
        GL.ActiveTexture(GLConstants.GL_TEXTURE0 + slot);
        GL.BindTexture(GLConstants.GL_TEXTURE_2D, _id);
    }

    public override void Unbind()
    {
        GL.BindTexture(GLConstants.GL_TEXTURE_2D, 0);
    }
}
