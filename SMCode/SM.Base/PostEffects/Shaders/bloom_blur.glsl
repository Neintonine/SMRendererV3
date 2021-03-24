#version 330
#define PI 3.14159265359

uniform sampler2D renderedTexture;
uniform float RenderScale;

uniform bool First;
uniform float Threshold;

uniform bool Horizontal;

uniform float[32] Weights;
uniform int WeightCount;
uniform float Power;

uniform float Radius;

layout(location = 0) out vec4 color;

vec4 GetRenderColorOffset(vec2 offset);

float brightness(vec3 c)
{
  return max(max(c.r, c.g), c.b);
}

float bright;

float GetWeight(int dif) {
	return Weights[dif];
}

void main() {
	vec3 thres = vec3(First ? Threshold : 0);

	vec2 tex_offset = 1.0 / textureSize(renderedTexture, 0) * vec2(Horizontal ? 1 : 0, Horizontal ? 0 : 1);

	vec3 result = max(GetRenderColorOffset(vec2(0)).rgb - thres, 0) * (First ? Power : 1);
	result *= GetWeight(0);

	for(int i = 1; i < WeightCount; i++) {
		result += max(GetRenderColorOffset(tex_offset * i * Radius).rgb - thres, 0) * (First ? Power : 1) * GetWeight(i);
		result += max(GetRenderColorOffset(-tex_offset * i * Radius).rgb - thres, 0) * (First ? Power : 1) * GetWeight(i);
	}

	color = vec4(result, 1);
}