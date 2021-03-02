#version 330

uniform float Gamma;

vec4 texture2DGamma(sampler2D s, vec2 P) {
	vec4 tex = texture2D(s, P);
	return vec4(pow(tex.rgb, vec3(Gamma)), tex.a);
}