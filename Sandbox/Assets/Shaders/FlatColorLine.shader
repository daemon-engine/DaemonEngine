#type vertex
#version 450 core
layout(location = 0) in vec3 POSITION;
layout(location = 1) in vec3 COLOR;

layout(std140, binding = 0) uniform Camera
{
    mat4 Projection;
    mat4 View;
    vec3 ViewPosition;
};
uniform mat4 _Model = mat4(1.0);

out vec3 Color;

void main()
{
    Color = COLOR;
    gl_Position = Projection * View * _Model * vec4(POSITION, 1.0);
}

#type fragment
#version 450 core
out vec4 FragColor;

in vec3 Color;

void main()
{
    FragColor = vec4(Color, 1.0);
}