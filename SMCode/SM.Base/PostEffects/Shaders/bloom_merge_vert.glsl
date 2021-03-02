#version 330

layout(location = 1) in vec2 aTex;

uniform mat3 TextureTransform;

out vec2 TransformedTexture;

void vertex() {
	TransformedTexture = vec2(TextureTransform * vec3(aTex, 1));
}