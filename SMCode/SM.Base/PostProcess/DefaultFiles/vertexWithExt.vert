#version 330

layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTex;

uniform mat4 MVP;
uniform mat4 ModelMatrix;

out vec2 vTexture;
out vec2 FragPos;

void vertex();

void main() {
	vTexture = aTex;
	
	FragPos = vec2(ModelMatrix * vec4(aPos, 1));

	gl_Position = MVP * vec4(aPos, 1);

	vertex();
}