using DaemonEngine.Extensions.OpenGL;
using DaemonEngine.Extensions.OpenGL.Enums;
using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLShader : IShader
{
    private readonly string _vertexShaderSource;
    private readonly string _fragmentShaderSource;

    private readonly uint _id;

    public OpenGLShader()
    {
        _vertexShaderSource = @"#version 330 core
layout (location = 0) in vec3 aPos;
void main()
{
    gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);
}";

        _fragmentShaderSource = @"#version 330 core
out vec4 FragColor;
void main()
{
    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
}";

        var vertexShader = CreateShader(GLShaderType.VertexShader, _vertexShaderSource);
        var fragmentShader = CreateShader(GLShaderType.FragmentShader, _fragmentShaderSource);
        _id = CreateShaderProgram(vertexShader, fragmentShader);

        //Logger.Information("Successfully loaded shader!");
    }

    public void Bind()
    {
        GL.UseProgram(_id);
    }

    public void Unbind()
    {
        GL.UseProgram(0);
    }

    private uint CreateShaderProgram(uint vertexShader, uint fragmentShader)
    {
        var program = GL.CreateProgram();
        GL.AttachShader(program, vertexShader);
        GL.AttachShader(program, fragmentShader);
        GL.ValidateProgram(program);
        GL.LinkProgram(program);

        var success = GL.GetProgramiv(program, GLConstants.GL_LINK_STATUS);
        if (success != GLConstants.GL_TRUE)
        {
            int length = 0;
            var infoLog = GL.GetProgramInfoLog(program, 512, ref length);
            System.Diagnostics.Debugger.Break();
            //Logger.Warning($"Failed to link Shader Program {program}\n{infoLog}");
        }

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
        return program;
    }

    private uint CreateShader(GLShaderType type, string source)
    {
        var shader = GL.CreateShader(type);
        GL.ShaderSource(shader, 1, new string[] { source }, null);
        GL.CompileShader(shader);

        var success = GL.GetShaderiv(shader, GLConstants.GL_COMPILE_STATUS);
        if (success != GLConstants.GL_TRUE)
        {
            int length = 0;
            var infoLog = GL.GetShaderInfoLog(shader, 512, ref length);
            System.Diagnostics.Debugger.Break();
            //Logger.Warning($"Failed to compile Shader {shader}\n{infoLog}");
        }

        return shader;
    }
}
