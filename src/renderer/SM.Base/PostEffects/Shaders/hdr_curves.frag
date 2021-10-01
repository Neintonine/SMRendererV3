#version 330 core

uniform float Exposure;

vec3 ACES(vec3 col) {
	const float a = 2.51;
	const float b = 0.03;
	const float c = 2.43;
	const float d = 0.59;
	const float e = 0.14;

	return clamp((col * (a * col + b)) / (col * (c * col + d) + e), 0.0,1.0);
}

vec3 reinhardTone(vec3 col) {
	return col / (col + vec3(1.0));
}

vec3 exposure(vec3 col) {
	return vec3(1) - exp(-col * Exposure);
}