#version 330 core

uniform vec4 color = vec4(1, 0, 1, 1);
out vec4 fragColor;

void main()
{
    fragColor = color;
}