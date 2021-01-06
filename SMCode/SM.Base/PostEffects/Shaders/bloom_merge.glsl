#version 330

in vec2 vTexture;

uniform sampler2D renderedTexture;
uniform sampler2D Bloom;

uniform float Exposure;
uniform bool HDR;

layout(location = 0) out vec4 color;

void main() {
	vec3 result = texture(Bloom, vTexture).rgb;
	if (!HDR) {
		result = vec3(1.0) - exp(-result * Exposure);
	}
	result += texture(renderedTexture, vTexture).rgb;
	
	color = vec4(result, 1);
}