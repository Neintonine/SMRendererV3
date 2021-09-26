#version 330 core

uniform vec2 renderedTextureTexelSize;

vec4 GetRenderColor();
vec4 GetRenderColorOffset(vec2);

float getBrightness(vec3 col) {
	return max(col.r, max(col.g, col.b));
	return (col.r + col.r + col.b + col.g + col.g + col.g) / 6.0;
}

layout(location = 0) out vec4 color;

vec3 downsample_high() {
	vec4 d = renderedTextureTexelSize.xyxy * vec4(-1,-1, +1, +1);
	vec3 s1 = GetRenderColorOffset(d.xy).rgb;	// - -
												// X -

	vec3 s2 = GetRenderColorOffset(d.zy).rgb;	// - -
												// - X

	vec3 s3 = GetRenderColorOffset(d.xw).rgb;	// X -
												// - -

	vec3 s4 = GetRenderColorOffset(d.zw).rgb;	// X -
												// - -

	float s1w = 1.0 / (getBrightness(s1) + 1.0);
	float s2w = 1.0 / (getBrightness(s2) + 1.0);
	float s3w = 1.0 / (getBrightness(s3) + 1.0);
	float s4w = 1.0 / (getBrightness(s4) + 1.0);
	float one_div = 1.0 / (s1w + s2w + s3w + s4w);

	return (s1 * s1w + s2 * s2w + s3 * s3w + s4 * s4w) * one_div;
}

void main() {

	color = vec4(downsample_high(),1);
}
