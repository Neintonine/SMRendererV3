#version 330

in vec2 vTexture;

uniform sampler2D Scene;
uniform float Exposure;
uniform float Gamma;

layout(location = 0) out vec4 color;

vec3 ACES(vec3 x) {
	const float a = 2.51;
	const float b = 0.03;
	const float c = 2.43;
	const float d = 0.59;
	const float e = 0.14;

	return clamp((x * (a * x + b)) / (x * (c * x + d) + e), 0,1.0);
}

vec3 reinhardTone(vec3 col) {
	return col / (col + vec3(1.0));
}

vec3 exposure(vec3 scene) {
	return vec3(1) - exp(-texture(Scene, vTexture).rgb * Exposure);
}

void main() {

	vec3 scene = texture2D(Scene, vTexture).rgb;
	vec3 result = reinhardTone(scene);

	color = vec4(pow(result, vec3(1 / Gamma)), 1);
}