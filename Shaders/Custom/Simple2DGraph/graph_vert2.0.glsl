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

vec2 function(vec2 functionInput){
    float a = 1, b = 0, c = 0.2;
    //return a * Pow(functionInput, 2) + b * functionInput + vec2(c, 0);
    return a * Pow(functionInput, 2) + b * functionInput + vec2(c, 0);
}

void main()
{
    vTextureCoordinates = aTextureCoordinate;

    vec2 inputZ = (aTextureCoordinate - vec2(0.5)) * 2;
    vec2 outputZ = function(inputZ);

    float angle = atan(outputZ.y, outputZ.x) / (2 * 3.14159265) + 0.5;
    vec3 rgb = HSVToRGB(vec3(angle, 1, 1));
    complexColor = rgb;
    float y = outputZ.x;

    height = y;
    gl_Position = projection * camera * object * vec4(aPosition + vec3(0, y, 0), 1.0);
}