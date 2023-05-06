#version 330 core

uniform sampler2D textureSampler;
in vec2 vTextureCoordinates;

out vec4 fragColor;

void main()
{
	vec4 textureColor = texture(textureSampler, vTextureCoordinates);
	if(textureColor.x * textureColor.y * textureColor.z < 0.5)
		discard;

    fragColor = vec4(0);
}