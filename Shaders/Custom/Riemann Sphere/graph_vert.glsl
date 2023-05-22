#version 330 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTextureCoordinate;

out vec3 vPosition;
out vec3 vLocalPosition;
out vec3 cameraPosition;
out vec3 complexColor;
out float height;
uniform mat4 projection = mat4(1);
uniform mat4 camera = mat4(1);
uniform mat4 object = mat4(1);
uniform float branch;


void main()
{
    vPosition = (object * vec4(aPosition, 1.0)).xyz;
    vLocalPosition = aPosition;
    cameraPosition = (inverse(camera) * vec4(0, 0, 0, 1)).xyz;

    float minToMax = 4;

    height = 0;
    gl_Position = projection * camera * object * vec4(aPosition, 1.0);
}