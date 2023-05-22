#version 330 core
#include C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\.Headers\complex.glsl
#include C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\.Headers\hue.glsl

in vec3 vPosition;
in vec3 cameraPosition;
in vec3 vLocalPosition;
in vec3 complexColor;
uniform vec3 center;
in float height;

float epsilon = 0.0001;
float maxDistance = 1;
out vec4 fragColor;

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

    return Pow(functionInput, -1);

    //return Fibonacci(functionInput);
}

void main()
{
    vec3 cameraOrigin = vLocalPosition;
    vec3 cameraRay = normalize(vPosition - cameraPosition);
    vec3 toMid = normalize(vec3(0.5) - cameraPosition);

    float distanceTravelled = 0;
    bool hit = false;
    vec3 hitPosition = vec3(0);
    for(int i = 0; i < 100; i++){
		if(distanceTravelled > maxDistance)
			break;

        float maxDistance = length(vec3(0.5) - (cameraRay * distanceTravelled + cameraOrigin)) - 0.5;
        distanceTravelled += maxDistance;
        if(maxDistance < epsilon){
            hit = true;
            hitPosition = cameraRay * distanceTravelled + cameraOrigin;
            break;
        }
    }
    if(!hit)
        discard;
    vec3 poleOrigin = vec3(0.5, 1, 0.5);
    vec3 rayFromPole = normalize(hitPosition - poleOrigin);
    //t denotes the length the direction vector has to be for the vector to intersect with the XZ plane.
    float t = -poleOrigin.y/rayFromPole.y;
    vec3 complexPosition = poleOrigin + rayFromPole * t - vec3(0.5, 0, 0.5);
    
    vec2 outputZ = function(complexPosition.xz);

    float angle = atan(outputZ.y, outputZ.x) / (2 * pi) + 0.5;
    vec3 rgb = HSVToRGB(vec3(angle, 1, 1 - 1 / (20 * length(outputZ) + 1)));
    
    float beltWidth = 0.01;
    float minXYZ = min(abs(0.5 - hitPosition.y), min(abs(0.5 - hitPosition.x), abs(0.5 - hitPosition.z)));
    if(minXYZ <= beltWidth)
    {
        rgb += vec3((beltWidth - minXYZ) * 30);
    }

    fragColor = vec4(rgb, 1);
}