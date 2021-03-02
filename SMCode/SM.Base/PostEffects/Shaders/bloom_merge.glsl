#version 330

in vec2 vTexture;
in vec2 TransformedTexture;

uniform sampler2D Scene;
uniform sampler2D Bloom;

uniform float MinAmount;
uniform float MaxAmount;
uniform sampler2D AmountMap;
uniform bool HasAmountMap;

uniform float Exposure;
uniform bool HDR;

layout(location = 0) out vec4 color;

void main() {
	vec3 result = texture(Bloom, vTexture).rgb;
	if (HasAmountMap) result *= clamp(length(texture(AmountMap, TransformedTexture).rgb) * (MaxAmount - MinAmount) + MinAmount, 0, 1);
	if (!HDR) {
		result = vec3(1.0) - exp(-result * Exposure);
	}
	

	result = texture(Scene, vTexture).rgb + result;
	
	color = vec4(result, 1);
}