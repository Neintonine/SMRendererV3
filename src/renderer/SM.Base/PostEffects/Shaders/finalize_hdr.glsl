#version 330

in vec2 vTexture;

uniform sampler2D Scene;
uniform float Exposure;
uniform float Gamma;

layout(location = 0) out vec4 color;

vec3 ACES(vec3);

vec3 reinhardTone(vec3);

vec3 exposure(vec3);

void main() {

	vec3 scene = texture2D(Scene, vTexture).rgb;
	vec3 result = exposure(scene);
	#if defined(TYPE_REINHARD)
	result = reinhardTone(result);
	#elif defined(TYPE_ACES)
	result = ACES(result);
	#endif

	color = vec4(pow(result, vec3(1 / Gamma)), 1);
}