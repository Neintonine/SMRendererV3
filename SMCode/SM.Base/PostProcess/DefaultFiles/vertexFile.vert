#version 330

layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTex;

uniform mat4 MVP;

out vec2 vTexture;

void main() {
	vTexture = aTex;

	gl_Position = MVP * vec4(aPos, 1);
}