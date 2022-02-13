#type vertex
#version 330 core
layout(location = 0) in vec3 POSITION;
layout(location = 1) in vec3 NORMAL;
layout(location = 2) in vec2 TEXCOORDS;

uniform mat4 _Model;
uniform mat4 _View;
uniform mat4 _Projection;

out vec3 FragPos;
out vec3 Normal;
out vec2 TexCoords;

void main()
{
    FragPos = vec3(_Model * vec4(POSITION, 1.0));
    Normal = mat3(transpose(inverse(_Model))) * NORMAL;
    TexCoords = TEXCOORDS;

    gl_Position = _Projection * _View * vec4(FragPos, 1.0);
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

struct DirectionalLight
{
    vec3 direction;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform vec3 _ViewPos;
uniform Material _Material;
uniform DirectionalLight _DirectionalLight;

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;

void main()
{
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(-_DirectionalLight.direction);

    vec3 ambient = _DirectionalLight.ambient * _Material.ambient;

    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = _DirectionalLight.diffuse * diff * _Material.diffuse;

    vec3 viewDir = normalize(_ViewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), _Material.shininess);
    vec3 specular = _DirectionalLight.specular * spec * _Material.specular;

    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
}