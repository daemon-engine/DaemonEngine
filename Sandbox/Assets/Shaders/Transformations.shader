#type vertex
#version 330 core
layout(location = 0) in vec3 POSITION;
layout(location = 1) in vec2 TEXCOORD;

out vec2 TexCoord;

uniform mat4 _Transform;

void main()
{
    gl_Position = _Transform * vec4(POSITION, 1.0);
    TexCoord = TEXCOORD;
}

#type fragment
#version 330 core
out vec4 FragColor;

in vec2 TexCoord;

uniform sampler2D _Texture1;
uniform sampler2D _Texture2;

void main()
{
    FragColor = mix(texture(_Texture1, TexCoord), texture(_Texture2, TexCoord), 0.2);
}

