#type vertex
#version 330 core
layout(location = 0) in vec3 POSITION;

uniform mat4 _Model;
uniform mat4 _View;
uniform mat4 _Projection;

void main()
{
    gl_Position = _Projection * _View * _Model * vec4(POSITION, 1.0);
}

#type fragment
#version 330 core
out vec4 FragColor;

uniform vec3 _ObjectColor;
uniform vec3 _LightColor;

void main()
{
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * _LightColor;

    vec3 result = ambient * _ObjectColor;
    FragColor = vec4(result, 1.0);
}