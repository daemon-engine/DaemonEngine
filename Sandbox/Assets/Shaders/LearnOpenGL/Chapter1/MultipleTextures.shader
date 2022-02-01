#type vertex
#version 330 core
layout(location = 0) in vec3 POSITION;
layout(location = 1) in vec2 TEXCOORD;

out vec2 _TexCoord;

void main()
{
    gl_Position = vec4(POSITION, 1.0);
    _TexCoord = TEXCOORD;
}

#type fragment
#version 330 core
out vec4 FragColor;

in vec2 _TexCoord;

uniform sampler2D Texture1;
uniform sampler2D Texture2;

void main()
{
    FragColor = mix(texture(Texture1, _TexCoord), texture(Texture2, _TexCoord), 0.2);
}

