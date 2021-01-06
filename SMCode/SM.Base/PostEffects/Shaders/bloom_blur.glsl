#version 330

uniform sampler2D renderedTexture;

uniform bool First;
uniform float Threshold;

uniform bool Horizontal;

uniform float[32] Weights;
uniform int WeightCount;

layout(location = 0) out vec4 color;

vec4 GetRenderColorOffset(vec2 offset);

void main() {
	vec3 thres = vec3(First ? Threshold : 0);

	vec2 tex_offset = 1.0 / textureSize(renderedTexture, 0);
	vec3 result = max(GetRenderColorOffset(vec2(0)).rgb - thres, 0) * Weights[0];

	if (Horizontal) {
		for(int i = 1; i < WeightCount; i++) {
			result += max(GetRenderColorOffset(vec2(tex_offset.x * i, 0)).rgb - thres, 0) * Weights[i];
			result += max(GetRenderColorOffset(vec2(-tex_offset.x * i, 0)).rgb - thres, 0) * Weights[i];
		}
	} else {
		for(int i = 1; i < WeightCount; i++) {
			result += GetRenderColorOffset(vec2(0, tex_offset.x * i)).rgb * Weights[i];
			result += GetRenderColorOffset(vec2(0, -tex_offset.x * i)).rgb * Weights[i];
		}
	}
	color = vec4(result, 1);
}