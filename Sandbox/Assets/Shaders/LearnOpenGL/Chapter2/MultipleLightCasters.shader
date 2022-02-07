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

struct DirectionalLight
{
	vec3 direction;

	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};

struct PointLight
{
	vec3 position;

	float constant;
	float linear;
	float quadratic;

	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};

struct SpotLight
{
	vec3 position;
	vec3 direction;
	float cutOff;
	float outerCutOff;

	float constant;
	float linear;
	float quadratic;

	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};

uniform vec3 _ObjectColor;
uniform vec3 _LightColor;
uniform vec3 _LightPos;
uniform vec3 _ViewPos;
uniform Material _Material;

uniform DirectionalLight _DirLight;

#define POINT_LIGHTS 4
uniform PointLight _PointLights[POINT_LIGHTS];
uniform SpotLight _SpotLight;

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;

vec3 CalculateDirectionalLight(DirectionalLight dirLight, vec3 normal, vec3 viewDir)
{
	vec3 lightDir = normalize(-dirLight.direction);

	// diffuse shading
	float diff = max(dot(normal, lightDir), 0.0);

	// specular shading
	vec3 reflectDir = reflect(-lightDir, normal);
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), _Material.shininess);

	// combine results
	vec3 ambient = dirLight.ambient * vec3(texture(_Material.diffuse, TexCoords));
	vec3 diffuse = dirLight.diffuse * diff * vec3(texture(_Material.diffuse, TexCoords));
	vec3 specular = dirLight.specular * spec * vec3(texture(_Material.specular, TexCoords));
	return (ambient + diffuse + specular);
}

vec3 CalculatePointLight(PointLight pointLight, vec3 normal, vec3 fragPos, vec3 viewDir)
{
	vec3 lightDir = normalize(pointLight.position - fragPos);

	// diffuse shading
	float diff = max(dot(normal, lightDir), 0.0);

	// specular shading
	vec3 reflectDir = reflect(-lightDir, normal);
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), _Material.shininess);

	// attenuation
	float distance = length(pointLight.position - fragPos);
	float attenuation = 1.0 / (pointLight.constant + pointLight.linear * distance + pointLight.quadratic * (distance * distance));

	// combine results
	vec3 ambient = pointLight.ambient * vec3(texture(_Material.diffuse, TexCoords));
	vec3 diffuse = pointLight.diffuse * diff * vec3(texture(_Material.diffuse, TexCoords));
	vec3 specular = pointLight.specular * spec * vec3(texture(_Material.specular, TexCoords));
	ambient *= attenuation;
	diffuse *= attenuation;
	specular *= attenuation;
	return (ambient + diffuse + specular);
}

vec3 CalculateSpotLight(SpotLight spotLight, vec3 normal, vec3 fragPos, vec3 viewDir)
{
	vec3 lightDir = normalize(spotLight.position - fragPos);

	// diffuse shading
	float diff = max(dot(normal, lightDir), 0.0);

	// specular shading
	vec3 reflectDir = reflect(-lightDir, normal);
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), _Material.shininess);

	// attenuation
	float distance = length(spotLight.position - fragPos);
	float attenuation = 1.0 / (spotLight.constant + spotLight.linear * distance + spotLight.quadratic * (distance * distance));

	// spotlight intensity
	float theta = dot(lightDir, normalize(-spotLight.direction));
	float epsilon = spotLight.cutOff - spotLight.outerCutOff;
	float intensity = clamp((theta - spotLight.outerCutOff) / epsilon, 0.0, 1.0);

	// combine results
	vec3 ambient = spotLight.ambient * vec3(texture(_Material.diffuse, TexCoords));
	vec3 diffuse = spotLight.diffuse * diff * vec3(texture(_Material.diffuse, TexCoords));
	vec3 specular = spotLight.specular * spec * vec3(texture(_Material.specular, TexCoords));
	ambient *= attenuation * intensity;
	diffuse *= attenuation * intensity;
	specular *= attenuation * intensity;
	return (ambient + diffuse + specular);
}

void main()
{
	vec3 norm = normalize(Normal);
	vec3 viewDir = normalize(_ViewPos - FragPos);

	vec3 output = CalculateDirectionalLight(_DirLight, norm, viewDir);

	for(int i = 0; i < POINT_LIGHTS; i++)
	{
		output += CalculatePointLight(_PointLights[i], norm, FragPos, viewDir);
	}

	output += CalculateSpotLight(_SpotLight, norm, FragPos, viewDir);

	FragColor = vec4(output, 1.0);
}