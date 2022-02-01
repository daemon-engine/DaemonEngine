#type vertex
#version 330 core
layout(location = 0) in vec3 POSITION;
layout(location = 1) in vec3 COLOR;
layout(location = 2) in vec2 TEXCOORD;

out vec3 _Color;
out vec2 _TexCoord;

void main()
{
    gl_Position = vec4(POSITION, 1.0);
    _Color = COLOR;
    _TexCoord = TEXCOORD;
}

#type fragment
#version 330 core
out vec4 FragColor;

in vec3 _Color;
in vec2 _TexCoord;

uniform sampler2D Texture;

void main()
{
    FragColor = texture(Texture, _TexCoord) * vec4(_Color, 1.0);
}
