#version 330 core

in vec2 vTextureCoordinates;
in vec3 complexColor;
in float height;

out vec4 fragColor;

void main()
{
    if(height >= 1)
        discard;
    fragColor = vec4(complexColor, 1);
}