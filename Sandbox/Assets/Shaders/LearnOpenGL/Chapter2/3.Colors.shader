#type vertex
#version 330 core
layout(location = 0) in vec3 POSITION;
layout(location = 1) in vec3 NORMAL;

uniform mat4 _Model;
uniform mat4 _View;
uniform mat4 _Projection;

out vec3 FragPos;
out vec3 Normal;

void main()
{
    gl_Position = _Projection * _View * _Model * vec4(POSITION, 1.0);
    FragPos = vec3(_Model * vec4(POSITION, 1.0));
    Normal = mat3(transpose(inverse(_Model))) * NORMAL;
}

#type fragment
#version 330 core
out vec4 FragColor;

uniform vec3 _ObjectColor;
uniform vec3 _LightColor;
uniform vec3 _LightPos;

in vec3 FragPos;
in vec3 Normal;

void main()
{
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(_LightPos - FragPos);

    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * _LightColor;

    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * _LightColor;

    vec3 result = (ambient + diffuse) * _ObjectColor;
    FragColor = vec4(result, 1.0);
}