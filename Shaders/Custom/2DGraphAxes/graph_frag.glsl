#version 330 core

in vec2 vTextureCoordinates;

out vec4 fragColor;

void main()
{
    bool dispose = false;

    if(abs(vTextureCoordinates.x - 0.5) > 0.01
        && abs(vTextureCoordinates.y - 0.5) > 0.01
        && vTextureCoordinates.x > 0.01 && vTextureCoordinates.x < 0.99
        && vTextureCoordinates.y > 0.01 && vTextureCoordinates.y < 0.99)
        dispose = true;

    vec4 color = vec4(0);
    float modulus = 0.5;
    if(mod(vTextureCoordinates.x, modulus / 2) < 0.01
        && (vTextureCoordinates.y < 0.03 || vTextureCoordinates.y > 0.97)){
        color = vec4(0.7);
        dispose = false;
    }
    if(mod(vTextureCoordinates.y, modulus / 2) < 0.01
        && (vTextureCoordinates.x < 0.03 || vTextureCoordinates.x > 0.97)){
        color = vec4(0.7);
        dispose = false;
    }
    if(!(vTextureCoordinates.x > 0.01 && vTextureCoordinates.x < 0.99
        && vTextureCoordinates.y > 0.01 && vTextureCoordinates.y < 0.99)){
        color = vec4(0.7);
        dispose = false;
    }

    if(dispose)
        discard;

    fragColor = color;
}