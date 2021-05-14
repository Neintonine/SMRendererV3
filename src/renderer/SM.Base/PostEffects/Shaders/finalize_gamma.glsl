#version 330

in vec2 vTexture;

uniform sampler2D Scene;
uniform float Gamma;

layout(location = 0) out vec4 color;

void main() {
	color = vec4(pow(texture(Scene, vTexture).rgb, vec3(1 / Gamma)), 1);
}