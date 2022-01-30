﻿using System.Linq;
using DaemonEngine.Extensions.OpenGL;
using DaemonEngine.Extensions.OpenGL.Enums;
using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

enum ShaderType
{
    None = 0,
    Vertex = 1,
    Fragment = 2
};

internal class OpenGLShader : IShader
{
    private readonly uint _id;

    public OpenGLShader(string filepath)
    {
        var shaderSources = ReadFile(filepath);
        var vertexSource = shaderSources[ShaderType.Vertex];
        var fragmentSource = shaderSources[ShaderType.Fragment];

        var vertexShader = CreateShader(GLShaderType.VertexShader, vertexSource);
        var fragmentShader = CreateShader(GLShaderType.FragmentShader, fragmentSource);
        _id = CreateShaderProgram(vertexShader, fragmentShader);
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

    private IDictionary<ShaderType, string> ReadFile(string filepath)
    {
        var lines = File.ReadAllLines(filepath);
        var shaderType = ShaderType.None;
        IDictionary<ShaderType, string> shaderSources = new Dictionary<ShaderType, string>();

        var typeKeyword = "#type";
        var shaderSource = "";
        foreach (var line in lines)
        {
            if (line.Contains(typeKeyword))
            {
                if (!string.IsNullOrWhiteSpace(shaderSource))
                {
                    shaderSources[shaderType] = shaderSource;
                    shaderSource = string.Empty;
                }

                var type = line.Substring(typeKeyword.Length + 1, line.Length - (typeKeyword.Length + 1));
                type = string.Concat(type[0].ToString().ToUpper(), type.AsSpan(1));
                shaderType = (ShaderType)Enum.Parse(typeof(ShaderType), type);
                shaderSources.Add(shaderType, "");
                continue;
            }

            shaderSource += line + "\n";
        }

        shaderSources[shaderType] = shaderSource;
        return shaderSources;
    }
}
