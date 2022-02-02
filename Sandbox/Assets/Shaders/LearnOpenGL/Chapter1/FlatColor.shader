#type vertex
#version 330 core
layout(location = 0) in vec3 POSITION;
layout(location = 1) in vec3 COLOR;

out vec3 _Color;

void main()
{
    gl_Position = vec4(POSITION, 1.0);
    _Color = COLOR;
}

#type fragment
#version 330 core
out vec4 FragColor;

in vec3 _Color;

void main()
{
    FragColor = vec4(_Color, 1.0f);
}
