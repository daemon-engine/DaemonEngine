using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.OpenGL.DllImport.Enums;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.OpenGL.DllImport;
using DaemonEngine.OpenGL.DllImport.Enums;
using DaemonEngine.Windows.Inputs;
using Serilog;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ImGuiNET;

public class ImGuiController : IDisposable
{
    private IShader _shader;
    private ITexture _texture;

    private bool _frameBegun;

    private uint _vertexArray;
    private uint _vertexBuffer;
    private int _vertexBufferSize;
    private uint _indexBuffer;
    private int _indexBufferSize;

    private int _width;
    private int _height;

    private Vector2 _scaleFactor = Vector2.One;
    private static int _sizeOfImDrawVert = Unsafe.SizeOf<ImDrawVert>();

    public ImGuiController(ILogger logger, IGraphicsFactory graphicsFactory, IInput input, int width, int height)
    {
        Logger = logger;
        GraphicsFactory = graphicsFactory;
        Input = input;

        _width = width;
        _height = height;

        var context = ImGui.CreateContext();
        ImGui.SetCurrentContext(context);

        var io = ImGui.GetIO();
        io.Fonts.AddFontDefault();

        io.BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset;

        CreateDeviceResources();

        SetPerFrameImGuiData(1f / 60f);

        ImGui.NewFrame();
        _frameBegun = true;
    }

    protected ILogger Logger { get; set; }
    protected IGraphicsFactory GraphicsFactory { get; set; }
    protected IInput Input { get; set; }

    private void CreateDeviceResources()
    {
        _vertexBufferSize = 10000;
        _indexBufferSize = 2000;

        uint[] ids = new uint[1];
        GL.CreateVertexArrays(1, ref ids);
        _vertexArray = ids[0];

        GL.CreateBuffers(1, ref ids);
        _vertexBuffer = ids[0];

        GL.CreateBuffers(1, ref ids);
        _indexBuffer = ids[0];

        GL.NamedBufferData(_vertexBuffer, (IntPtr)_vertexBufferSize, IntPtr.Zero, GLBufferUsage.DynamicDraw);
        GL.NamedBufferData(_indexBuffer, (IntPtr)_indexBufferSize, IntPtr.Zero, GLBufferUsage.DynamicDraw);

        RecreateFontDeviceTexture();

        string VertexSource = @"#version 330 core
uniform mat4 projection_matrix;
layout(location = 0) in vec2 in_position;
layout(location = 1) in vec2 in_texCoord;
layout(location = 2) in vec4 in_color;
out vec4 color;
out vec2 texCoord;
void main()
{
    gl_Position = projection_matrix * vec4(in_position, 0, 1);
    color = in_color;
    texCoord = in_texCoord;
}";
        string FragmentSource = @"#version 330 core
uniform sampler2D in_fontTexture;
in vec4 color;
in vec2 texCoord;
out vec4 outputColor;
void main()
{
    outputColor = color * texture(in_fontTexture, texCoord);
}";

        _shader = GraphicsFactory.CreateShader(VertexSource, FragmentSource);

        GL.VertexArrayVertexBuffer(_vertexArray, 0, _vertexBuffer, IntPtr.Zero, _sizeOfImDrawVert);
        GL.VertexArrayElementBuffer(_vertexArray, _indexBuffer);

        GL.EnableVertexArrayAttrib(_vertexArray, 0);
        GL.VertexArrayAttribBinding(_vertexArray, 0, 0);
        GL.VertexArrayAttribFormat(_vertexArray, 0, 2, GLConstants.GL_FLOAT, false, 0);

        GL.EnableVertexArrayAttrib(_vertexArray, 1);
        GL.VertexArrayAttribBinding(_vertexArray, 1, 0);
        GL.VertexArrayAttribFormat(_vertexArray, 1, 2, GLConstants.GL_FLOAT, false, 8);

        GL.EnableVertexArrayAttrib(_vertexArray, 2);
        GL.VertexArrayAttribBinding(_vertexArray, 2, 0);
        GL.VertexArrayAttribFormat(_vertexArray, 2, 4, GLConstants.GL_UNSIGNED_BYTE, true, 16);

        CheckGLError("End of ImGui setup");
    }

    public void ResizeImGuiContext(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public void RecreateFontDeviceTexture()
    {
        ImGuiIOPtr io = ImGui.GetIO();
        io.Fonts.GetTexDataAsRGBA32(out IntPtr pixels, out int width, out int height, out int bytesPerPixel);

        _texture = GraphicsFactory.CreateTexture(width, height); // new Texture("ImGui Text Atlas", width, height, pixels);
        _texture.SetData(width * height * bytesPerPixel, pixels);
        //_texture.SetMagFilter(TextureMagFilter.Linear);
        //_texture.SetMinFilter(TextureMinFilter.Linear);

        io.Fonts.SetTexID((IntPtr)_texture.GetId());

        io.Fonts.ClearTexData();
    }

    public void BeginImGuiFrame()
    {
        if (_frameBegun)
        {
            ImGui.Render();
        }

        _frameBegun = true;
        ImGui.NewFrame();
    }

    public void Render()
    {
        if (_frameBegun)
        {
            _frameBegun = false;
            ImGui.Render();
            RenderImDrawData(ImGui.GetDrawData());
        }
    }

    private void RenderImDrawData(ImDrawDataPtr draw_data)
    {
        if (draw_data.CmdListsCount == 0)
        {
            return;
        }

        for (int i = 0; i < draw_data.CmdListsCount; i++)
        {
            ImDrawListPtr cmd_list = draw_data.CmdListsRange[i];

            int vertexSize = cmd_list.VtxBuffer.Size * _sizeOfImDrawVert;
            if (vertexSize > _vertexBufferSize)
            {
                int newSize = (int)Math.Max(_vertexBufferSize * 1.5f, vertexSize);
                GL.NamedBufferData(_vertexBuffer, new IntPtr(newSize), IntPtr.Zero, GLBufferUsage.DynamicDraw);
                CheckGLError($"Data Vert");
                _vertexBufferSize = newSize;

                Logger.Verbose($"Resized dear imgui vertex buffer to new size {_vertexBufferSize}");
            }

            int indexSize = cmd_list.IdxBuffer.Size * sizeof(ushort);
            if (indexSize > _indexBufferSize)
            {
                int newSize = (int)Math.Max(_indexBufferSize * 1.5f, indexSize);
                GL.NamedBufferData(_indexBuffer, new IntPtr(newSize), IntPtr.Zero, GLBufferUsage.DynamicDraw);
                CheckGLError($"Data Vert");
                _indexBufferSize = newSize;

                Logger.Verbose($"Resized dear imgui index buffer to new size {_indexBufferSize}");
            }
        }

        // Setup orthographic projection matrix into our constant buffer
        ImGuiIOPtr io = ImGui.GetIO();
        Matrix4x4 mvp = Matrix4x4.CreateOrthographicOffCenter(
            0.0f,
            io.DisplaySize.X,
            io.DisplaySize.Y,
            0.0f,
            -1.0f,
            1.0f);

        _shader.Bind();
        _shader.SetMat4("projection_matrix", (DaemonEngine.Mathematics.Matrix4)mvp);
        _shader.SetInt("in_fontTexture", 0);
        CheckGLError("Projection");

        GL.BindVertexArray(_vertexArray);
        CheckGLError("VAO");

        draw_data.ScaleClipRects(io.DisplayFramebufferScale);

        GL.Enable(GLCapabilities.Blend);
        GL.Enable(GLCapabilities.ScissorTest);
        GL.BlendEquation(GLConstants.GL_FUNC_ADD);
        GL.BlendFunc(GLConstants.GL_SRC_ALPHA, GLConstants.GL_ONE_MINUS_SRC_ALPHA);
        GL.Disable(GLCapabilities.CullFace);
        GL.Disable(GLCapabilities.DepthTest);

        // Render command lists
        for (int n = 0; n < draw_data.CmdListsCount; n++)
        {
            ImDrawListPtr cmd_list = draw_data.CmdListsRange[n];

            GL.NamedBufferSubData(_vertexBuffer, IntPtr.Zero, new IntPtr(cmd_list.VtxBuffer.Size * _sizeOfImDrawVert), cmd_list.VtxBuffer.Data);
            CheckGLError($"Data Vert {n}");

            GL.NamedBufferSubData(_indexBuffer, IntPtr.Zero, new IntPtr(cmd_list.IdxBuffer.Size * sizeof(ushort)), cmd_list.IdxBuffer.Data);
            CheckGLError($"Data Idx {n}");

            for (int cmd_i = 0; cmd_i < cmd_list.CmdBuffer.Size; cmd_i++)
            {
                ImDrawCmdPtr pcmd = cmd_list.CmdBuffer[cmd_i];
                if (pcmd.UserCallback != IntPtr.Zero)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    GL.ActiveTexture(GLConstants.GL_TEXTURE0);
                    GL.BindTexture(GLConstants.GL_TEXTURE_2D, (uint)pcmd.TextureId);
                    CheckGLError("Texture");

                    // We do _windowHeight - (int)clip.W instead of (int)clip.Y because gl has flipped Y when it comes to these coordinates
                    var clip = pcmd.ClipRect;
                    GL.Scissor((int)clip.X, _height - (int)clip.W, (int)(clip.Z - clip.X), (int)(clip.W - clip.Y));
                    CheckGLError("Scissor");

                    if ((io.BackendFlags & ImGuiBackendFlags.RendererHasVtxOffset) != 0)
                    {
                        GL.DrawElementsBaseVertex(GLConstants.GL_TRIANGLES, (int)pcmd.ElemCount, GLConstants.GL_UNSIGNED_SHORT, (IntPtr)(pcmd.IdxOffset * sizeof(ushort)), (int)pcmd.VtxOffset);
                    }
                    else
                    {
                        GL.DrawElements(GLConstants.GL_TRIANGLES, (int)pcmd.ElemCount, GLConstants.GL_UNSIGNED_SHORT, (IntPtr)((int)pcmd.IdxOffset * sizeof(ushort)));
                    }
                    CheckGLError("Draw");
                }
            }
        }

        GL.Disable(GLCapabilities.Blend);
        GL.Disable(GLCapabilities.ScissorTest);
        GL.Enable(GLCapabilities.DepthTest);
    }

    public void SetPerFrameImGuiData(float deltaTime)
    {
        ImGuiIOPtr io = ImGui.GetIO();
        io.DisplaySize = new Vector2(_width / _scaleFactor.X, _height / _scaleFactor.Y);
        io.DisplayFramebufferScale = _scaleFactor;
        io.DeltaTime = deltaTime; // DeltaTime is in seconds.
    }

    readonly List<char> PressedChars = new List<char>();

    public void UpdateImGuiKeyDownState(int keycode, bool isDown)
    {
        ImGuiIOPtr io = ImGui.GetIO();

        io.KeysDown[keycode] = isDown;
    }

    public void UpdateImGuiMouseButtonDownState(int button, bool isDown)
    {
        ImGuiIOPtr io = ImGui.GetIO();

        io.MouseDown[button] = isDown;
    }

    public void UpdateImGuiMousePosition(Vector2 mousePosition)
    {
        ImGuiIOPtr io = ImGui.GetIO();

        io.MousePos = mousePosition;
    }

    public void UpdateImGuiControlKeyDownState(bool isDown)
    {
        ImGuiIOPtr io = ImGui.GetIO();

        io.KeyCtrl = isDown;
    }

    public void UpdateImGuiAltKeyDownState(bool isDown)
    {
        ImGuiIOPtr io = ImGui.GetIO();

        io.KeyAlt = isDown;
    }

    public void UpdateImGuiShiftKeyDownState(bool isDown)
    {
        ImGuiIOPtr io = ImGui.GetIO();

        io.KeyShift = isDown;
    }

    public void UpdateImGuiSuperKeyDownState(bool isDown)
    {
        ImGuiIOPtr io = ImGui.GetIO();

        io.KeySuper = isDown;
    }

    public void UpdateImGuiPressedCharacters()
    {
        ImGuiIOPtr io = ImGui.GetIO();

        foreach (var c in PressedChars)
        {
            io.AddInputCharacter(c);
        }
        PressedChars.Clear();
    }

    public void PressKey(char keyChar)
    {
        PressedChars.Add(keyChar);
    }

    public void UpdateMouseScroll(Vector2 offset)
    {
        ImGuiIOPtr io = ImGui.GetIO();

        io.MouseWheel = offset.Y;
        io.MouseWheelH = offset.X;
    }

    public void MapImGuiKey(ImGuiKey imGuiKey, int keycode)
    {
        ImGuiIOPtr io = ImGui.GetIO();

        io.KeyMap[(int)imGuiKey] = keycode;
    }

    private void CheckGLError(string title)
    {
        var error = GL.GetError();
        if (error != GLError.NoError)
        {
            Logger.Fatal($"ImGuiController: ({error}|{(uint)error}) {title}");
        }
    }

    public void Dispose()
    {
    }
}
