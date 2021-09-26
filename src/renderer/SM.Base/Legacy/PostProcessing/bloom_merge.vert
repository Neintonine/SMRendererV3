#version 330

layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTex;

uniform mat4 MVP;
uniform mat3 TextureTransform;

out vec2 vTexture;
out vec2 TransformedTexture;


void main() {
	vTexture = aTex;
	//TransformedTexture = vec2(TextureTransform * vec3(aTex, 1));

	gl_Position = MVP * vec4(aPos, 1);
}