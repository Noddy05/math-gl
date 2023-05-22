#version 330 core
#include C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\.Headers\hue.glsl

in vec2 vTextureCoordinates;

out vec4 fragColor;

void main()
{
    vec2 coordinates = vTextureCoordinates - vec2(0.5);
	float angle = atan(-coordinates.x, coordinates.y) / (2 * 3.14159265) + 0.5;
    vec3 rgb = HSVToRGB(vec3(angle, 1, 1));

    fragColor = vec4(rgb, 0);
}