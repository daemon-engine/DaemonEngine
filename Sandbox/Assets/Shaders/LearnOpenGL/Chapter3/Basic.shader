#type vertex
#version 330 core
layout(location = 0) in vec3 POSITION;
layout(location = 1) in vec3 NORMAL;
layout(location = 2) in vec2 TEXCOORDS;

uniform mat4 _Model;
uniform mat4 _View;
uniform mat4 _Projection;

out vec3 Normal;
out vec2 TexCoords;

void main()
{
    gl_Position = _Projection * _View * _Model * vec4(POSITION, 1.0);
    Normal = mat3(transpose(inverse(_Model))) * NORMAL;
    TexCoords = TEXCOORDS;
}

#type fragment
#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec2 TexCoords;

void main()
{
    FragColor = vec4(1.0, 0.0, 1.0, 1.0);
}