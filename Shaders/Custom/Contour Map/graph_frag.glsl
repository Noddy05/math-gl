#version 330 core
#include C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\.Headers\complex.glsl
#include C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\.Headers\hue.glsl

in vec2 vCoordinates;
uniform vec3 center;
in float height;

float epsilon = 0.0001;
float maxDistance = 1;
out vec4 fragColor;

vec2 function(vec2 functionInput){
     return Pow(functionInput, -1);
}

float size = 4;
int steps = 20;
float stepHeight(float height){
    return max(min(round(height * steps / size) / steps, 1), 0);
}
float getHeight(vec2 inputZ){
    return length(function((inputZ - vec2(0.5)) * size * root2));
}

void main()
{
    float heightDivisor = 8;
    float height = getHeight(vCoordinates);
    float heightRounded = stepHeight(height);
    height /= heightDivisor;

    bool onEdge = false;
    float edgeMargin = 0.0002;
    if(stepHeight(getHeight(vCoordinates + vec2(edgeMargin, 0))) != heightRounded){
        fragColor = vec4(0);
        return;
    }
    if(stepHeight(getHeight(vCoordinates - vec2(edgeMargin, 0))) != heightRounded){
        fragColor = vec4(0);
        return;
    }
    if(stepHeight(getHeight(vCoordinates + vec2(0, edgeMargin))) != heightRounded){
        fragColor = vec4(0);
        return;
    }
    if(stepHeight(getHeight(vCoordinates - vec2(0, edgeMargin))) != heightRounded){
        fragColor = vec4(0);
        return;
    }
    float edgeMarginCorner = root2 * edgeMargin;
    if(stepHeight(getHeight(vCoordinates + vec2(edgeMarginCorner, edgeMarginCorner))) != heightRounded){
        fragColor = vec4(0);
        return;
    }
    if(stepHeight(getHeight(vCoordinates + vec2(-edgeMarginCorner, edgeMarginCorner))) != heightRounded){
        fragColor = vec4(0);
        return;
    }
    if(stepHeight(getHeight(vCoordinates + vec2(-edgeMarginCorner, -edgeMarginCorner))) != heightRounded){
        fragColor = vec4(0);
        return;
    }
    if(stepHeight(getHeight(vCoordinates + vec2(edgeMarginCorner, -edgeMarginCorner))) != heightRounded){
        fragColor = vec4(0);
        return;
    }

    fragColor = vec4(HSVToRGB(vec3(heightRounded / 3, heightRounded, 1 - heightRounded / 5)), 1);
}