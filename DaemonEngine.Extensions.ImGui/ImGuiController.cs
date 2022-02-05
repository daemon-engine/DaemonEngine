using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.GraphicsLibraryFramework.DllImport;
using DaemonEngine.OpenGL.DllImport;
using DaemonEngine.OpenGL.DllImport.Enums;
using DaemonEngine.Windows.Inputs;
using Serilog;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ImGuiNET;

public class ImGuiController
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
        SetKeyMappings();

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

        GL.VertexArrayVertexBuffer(_vertexArray, 0, _vertexBuffer, IntPtr.Zero, Unsafe.SizeOf<ImDrawVert>());
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

    public void Update(float deltaTime)
    {
        if (_frameBegun)
        {
            ImGui.Render();
        }

        SetPerFrameImGuiData(deltaTime);
        UpdateImGuiInput();

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

    private void SetPerFrameImGuiData(float deltaTime)
    {
        ImGuiIOPtr io = ImGui.GetIO();
        io.DisplaySize = new Vector2(
            _width / _scaleFactor.X,
            _height / _scaleFactor.Y);
        io.DisplayFramebufferScale = _scaleFactor;
        io.DeltaTime = deltaTime; // DeltaTime is in seconds.
    }

    readonly List<char> PressedChars = new List<char>();

    private void UpdateImGuiInput()
    {
        var mousePosition = Input.GetMousePosition();

        ImGuiIOPtr io = ImGui.GetIO();

        io.MouseDown[0] = Input.IsButtonDown(GlfwConstants.GLFW_MOUSE_BUTTON_LEFT);
        io.MouseDown[1] = Input.IsButtonDown(GlfwConstants.GLFW_MOUSE_BUTTON_RIGHT);
        io.MouseDown[2] = Input.IsButtonDown(GlfwConstants.GLFW_MOUSE_BUTTON_MIDDLE);

        var screenPoint = mousePosition;
        var point = screenPoint;//wnd.PointToClient(screenPoint);
        io.MousePos = new Vector2(point.X, point.Y);

        //foreach (Keycode key in Enum.GetValues(typeof(Keys)))
        //{
        //    if (key == GlfwConstants.GLFW_KEY_UNKNOWN)
        //    {
        //        continue;
        //    }
        //    io.KeysDown[(int)key] = Input.IsKeyDown(key);
        //}

        foreach (var c in PressedChars)
        {
            io.AddInputCharacter(c);
        }
        PressedChars.Clear();

        io.KeyCtrl = Input.IsKeyDown(GlfwConstants.GLFW_KEY_LEFT_CONTROL) || Input.IsKeyDown(GlfwConstants.GLFW_KEY_RIGHT_CONTROL);
        io.KeyAlt = Input.IsKeyDown(GlfwConstants.GLFW_KEY_LEFT_ALT) || Input.IsKeyDown(GlfwConstants.GLFW_KEY_RIGHT_ALT);
        io.KeyShift = Input.IsKeyDown(GlfwConstants.GLFW_KEY_LEFT_SHIFT) || Input.IsKeyDown(GlfwConstants.GLFW_KEY_RIGHT_SHIFT);
        io.KeySuper = Input.IsKeyDown(GlfwConstants.GLFW_KEY_LEFT_SUPER) || Input.IsKeyDown(GlfwConstants.GLFW_KEY_RIGHT_SUPER);
    }

    internal void PressChar(char keyChar)
    {
        PressedChars.Add(keyChar);
    }

    internal void MouseScroll(Vector2 offset)
    {
        ImGuiIOPtr io = ImGui.GetIO();

        io.MouseWheel = offset.Y;
        io.MouseWheelH = offset.X;
    }

    private static void SetKeyMappings()
    {
        ImGuiIOPtr io = ImGui.GetIO();
        io.KeyMap[(int)ImGuiKey.Tab] = (int)GlfwConstants.GLFW_KEY_TAB;
        io.KeyMap[(int)ImGuiKey.LeftArrow] = (int)GlfwConstants.GLFW_KEY_LEFT;
        io.KeyMap[(int)ImGuiKey.RightArrow] = (int)GlfwConstants.GLFW_KEY_RIGHT;
        io.KeyMap[(int)ImGuiKey.UpArrow] = (int)GlfwConstants.GLFW_KEY_UP;
        io.KeyMap[(int)ImGuiKey.DownArrow] = (int)GlfwConstants.GLFW_KEY_DOWN;
        io.KeyMap[(int)ImGuiKey.PageUp] = (int)GlfwConstants.GLFW_KEY_PAGE_UP;
        io.KeyMap[(int)ImGuiKey.PageDown] = (int)GlfwConstants.GLFW_KEY_PAGE_DOWN;
        io.KeyMap[(int)ImGuiKey.Home] = (int)GlfwConstants.GLFW_KEY_HOME;
        io.KeyMap[(int)ImGuiKey.End] = (int)GlfwConstants.GLFW_KEY_END;
        io.KeyMap[(int)ImGuiKey.Delete] = (int)GlfwConstants.GLFW_KEY_DELETE;
        io.KeyMap[(int)ImGuiKey.Backspace] = (int)GlfwConstants.GLFW_KEY_BACKSPACE;
        io.KeyMap[(int)ImGuiKey.Enter] = (int)GlfwConstants.GLFW_KEY_ENTER;
        io.KeyMap[(int)ImGuiKey.Escape] = (int)GlfwConstants.GLFW_KEY_ESCAPE;
        io.KeyMap[(int)ImGuiKey.A] = GlfwConstants.GLFW_KEY_A;
        io.KeyMap[(int)ImGuiKey.C] = GlfwConstants.GLFW_KEY_C;
        io.KeyMap[(int)ImGuiKey.V] = GlfwConstants.GLFW_KEY_V;
        io.KeyMap[(int)ImGuiKey.X] = GlfwConstants.GLFW_KEY_X;
        io.KeyMap[(int)ImGuiKey.Y] = GlfwConstants.GLFW_KEY_Y;
        io.KeyMap[(int)ImGuiKey.Z] = GlfwConstants.GLFW_KEY_Z;
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

            int vertexSize = cmd_list.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>();
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
        _shader.SetMat4("projection_matrix", mvp);
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

            GL.NamedBufferSubData(_vertexBuffer, IntPtr.Zero, new IntPtr(cmd_list.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>()), cmd_list.VtxBuffer.Data);
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
    }

    private void CheckGLError(string title)
    {
        var error = GL.GetError();
        if (error != GLConstants.GL_NO_ERROR)
        {
            Logger.Fatal($"ImGuiController: ({error}) {title}");
        }
    }
}
