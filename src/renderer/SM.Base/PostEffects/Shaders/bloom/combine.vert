#version 330

layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTex;

uniform mat4 MVP;
uniform mat3 amountTransform;

out vec2 vTexture;
out vec2 amountUV;

void main() {
	vTexture = aTex;
	amountUV = vec2(amountTransform * vec3(aTex, 1));

	gl_Position = MVP * vec4(aPos, 1);
}