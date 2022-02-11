#type vertex
#version 330 core
layout(location = 0) in vec2 POSITION;
layout(location = 1) in vec2 TEXCOORD;

out vec2 TexCoords;

void main()
{
    gl_Position = vec4(POSITION.x, POSITION.y, 0.0, 1.0);
    TexCoords = TEXCOORD;
}

#type fragment
#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D _ScreenTexture;

void main()
{
    FragColor = vec4(vec3(1.0 - texture(_ScreenTexture, TexCoords)), 1.0);
}
