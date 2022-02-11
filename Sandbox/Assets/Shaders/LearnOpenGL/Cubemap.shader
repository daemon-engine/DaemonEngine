#type vertex
#version 330 core
layout(location = 0) in vec3 POSITION;

out vec3 TexCoord;

uniform mat4 _Projection;
uniform mat4 _View;

void main()
{
    TexCoord = POSITION;
    vec4 pos = _Projection * _View * vec4(POSITION, 1.0);
    gl_Position = pos.xyww;
}

#type fragment
#version 330 core
out vec4 FragColor;

in vec3 TexCoord;

uniform samplerCube _Cubemap;

void main()
{
    FragColor = texture(_Cubemap, TexCoord);
}
