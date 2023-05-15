#version 330 core

in vec2 vTextureCoordinates;
in vec3 complexColor;
in float height;

out vec4 fragColor;

void main()
{
    if(height >= 2)
        discard;

    vec3 color = complexColor;

    float width = 1000.01;
    if((vTextureCoordinates.x - (0.5 - width) > 0 && vTextureCoordinates.x - (0.5 - width) < width * 2) ||
        (vTextureCoordinates.y - (0.5 - width) > 0 && vTextureCoordinates.y - (0.5 - width) < width * 2))
    {

    } 
    else
    {
        discard;
    }
    fragColor = vec4(color, 1);
}