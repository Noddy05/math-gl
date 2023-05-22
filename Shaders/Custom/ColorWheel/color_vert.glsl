#version 330 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTextureCoordinate;

uniform mat4 projection = mat4(1);
uniform mat4 camera = mat4(1);
uniform mat4 object = mat4(1);

out vec2 vTextureCoordinates;

void main()
{
    vTextureCoordinates = aTextureCoordinate;
    gl_Position = projection * camera * object * vec4(aPosition, 1.0);
}