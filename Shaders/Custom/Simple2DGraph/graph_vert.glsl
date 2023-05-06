#version 330 core
#include C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\.Headers\complex.glsl
#include C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\.Headers\hue.glsl

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTextureCoordinate;

out vec2 vTextureCoordinates;
out vec3 complexColor;
out float height;
uniform mat4 projection = mat4(1);
uniform mat4 camera = mat4(1);
uniform mat4 object = mat4(1);

void main()
{
    vTextureCoordinates = aTextureCoordinate;
    float a = 1, b = 0, c = 0.2;

    vec2 inputZ = (aTextureCoordinate - vec2(0.5)) * 2;
    vec2 outputZ = a * Pow(inputZ, 2) + b * inputZ + vec2(c, 0);

    float angle = atan(outputZ.y, outputZ.x) * 180 / 3.14159265 + 180;
    vec3 rgb = HSVToRGB(vec3(angle / 360.0, 1, 1));
    complexColor = rgb;
    float y = outputZ.x;

    height = y;
    gl_Position = projection * camera * object * vec4(aPosition + vec3(0, y, 0), 1.0);
}