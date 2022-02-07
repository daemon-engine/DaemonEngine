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
	sampler2D diffuse;
	sampler2D specular;
	float shininess;
};

struct SpotLight
{
	vec3 position;
	vec3 direction;

	float cutOff;
	float outerCutOff;

	vec3 ambient;
	vec3 diffuse;
	vec3 specular;

	float constant;
	float linear;
	float quadratic;
};

uniform vec3 _ObjectColor;
uniform vec3 _LightColor;
uniform vec3 _LightPos;
uniform vec3 _ViewPos;
uniform Material _Material;
uniform SpotLight _SpotLight;

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;

void main()
{
	// Ambient
	vec3 ambient = _SpotLight.ambient * texture(_Material.diffuse, TexCoords).rgb;

	// Diffuse
	vec3 norm = normalize(Normal);
	vec3 lightDir = normalize(_SpotLight.position - FragPos);
	float diff = max(dot(norm, lightDir), 0.0);
	vec3 diffuse = _SpotLight.diffuse * diff * texture(_Material.diffuse, TexCoords).rgb;

	// Specular
	vec3 viewDir = normalize(_ViewPos - FragPos);
	vec3 reflectDir = reflect(-lightDir, norm);
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), _Material.shininess);
	vec3 specular = _SpotLight.specular * spec * texture(_Material.specular, TexCoords).rgb;

	// Spot light soft edges
	float theta = dot(lightDir, normalize(-_SpotLight.direction));
	float epsilon = (_SpotLight.cutOff - _SpotLight.outerCutOff);
	float intensity = clamp((theta - _SpotLight.outerCutOff) / epsilon, 0.0, 1.0);
	diffuse *= intensity;
	specular *= intensity;

	// Attenuation
	float distance = length(_SpotLight.position - FragPos);
	float attenuation = 1.0 / (_SpotLight.constant + _SpotLight.linear * distance + _SpotLight.quadratic * (distance * distance));
	ambient *= attenuation;
	diffuse *= attenuation;
	specular *= attenuation;

	vec3 result = ambient + diffuse + specular;
	FragColor = vec4(result, 1.0);
}