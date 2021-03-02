#version 330

in vec2 vTexture;

uniform sampler2D Scene;
uniform float Exposure;
uniform float Gamma;

layout(location = 0) out vec4 color;

void main() {
	vec3 result = vec3(1) - exp(-texture(Scene, vTexture).rgb * Exposure);

	color = vec4(pow(result, vec3(1 / Gamma)), 1);
}