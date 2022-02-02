#type vertex
#version 330 core
layout(location = 0) in vec3 POSITION;
layout(location = 1) in vec3 NORMAL;

uniform vec3 _LightPos;
uniform vec3 _ViewPos;
uniform vec3 _LightColor;

uniform mat4 _Model;
uniform mat4 _View;
uniform mat4 _Projection;

out vec3 LightingColor; // resulting color from lighting calculations

void main()
{
    gl_Position = _Projection * _View * _Model * vec4(POSITION, 1.0);

    // gouraud shading
    // ------------------------
    vec3 Position = vec3(_Model * vec4(POSITION, 1.0));
    vec3 Normal = mat3(transpose(inverse(_Model))) * NORMAL;

    // ambient
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * _LightColor;

    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(_LightPos - Position);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * _LightColor;

    // specular
    float specularStrength = 1.0; // this is set higher to better show the effect of Gouraud shading 
    vec3 viewDir = normalize(_ViewPos - Position);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec3 specular = specularStrength * spec * _LightColor;

    LightingColor = ambient + diffuse + specular;
}

#type fragment
#version 330 core
out vec4 FragColor;

in vec3 LightingColor;

uniform vec3 _ObjectColor;

void main()
{
    FragColor = vec4(LightingColor * _ObjectColor, 1.0);
}