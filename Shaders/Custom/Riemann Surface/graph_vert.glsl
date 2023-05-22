﻿#version 330 core
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
uniform float branch;

vec2 function(vec2 functionInput){
    //float a = 1, b = sin(t / 3), c = 0.2;
    //return a * Pow(functionInput, 2) + b * functionInput + vec2(c, 0);
    //return a * Pow(functionInput, t) + b * functionInput + vec2(c, 0);
    
    /*
    vec2 z = functionInput;
    for(int i = 0; i < 100; i++){
        z = Pow(z, 2) - functionInput;
        if(z.x * z.x + z.y * z.y >= 4)
            return z;
    }

    return z;*/

    //return Log(functionInput, gl_InstanceID + branch);

    return ExpandedZeta(functionInput);
    //return Fibonacci(functionInput);
}

void main()
{
    vTextureCoordinates = aTextureCoordinate;

    float minToMax = 16;
    vec2 inputZ = (aTextureCoordinate - vec2(0.5)) * minToMax;
    vec2 outputZ = function(inputZ);

    float angle = atan(outputZ.y, outputZ.x) / (2 * pi) + 0.5;
    vec3 rgb = HSVToRGB(vec3(angle, 1, 1 - 1 / (20 * length(outputZ) + 1)));
    complexColor = rgb;
    float y = length(outputZ);
    y /= minToMax;

    height = 0;
    y = max(min(y, 1), -1);
    gl_Position = projection * camera * object * vec4(aPosition + vec3(0, y, 0), 1.0);
}