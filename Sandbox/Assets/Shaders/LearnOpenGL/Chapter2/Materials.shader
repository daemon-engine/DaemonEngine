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

struct Material
{
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    float shininess;
};

struct Light
{
    vec3 position;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    float shininess;
};

uniform vec3 _ObjectColor;
uniform vec3 _LightColor;
uniform vec3 _LightPos;
uniform vec3 _ViewPos;
uniform Material _Material;
uniform Light _Light;

in vec3 FragPos;
in vec3 Normal;

void main()
{
    vec3 ambient = _Light.ambient * _Material.ambient;

    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(_LightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = _Light.diffuse * (diff * _Material.diffuse);

    vec3 viewDir = normalize(_ViewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), _Material.shininess);
    vec3 specular = _Light.specular * (spec * _Material.specular);

    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
}