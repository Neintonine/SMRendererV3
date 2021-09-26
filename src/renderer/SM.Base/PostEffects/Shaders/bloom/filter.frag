#version 330 core

uniform vec4 ThresholdCurve;
uniform vec2 renderedTextureTexelSize;

vec4 GetRenderColorOffset(vec2);

layout(location = 0) out vec4 color;
layout(location = 1) out vec4 scene;

vec3 safe_color(vec3 c) {
	return clamp(c, vec3(0.0), vec3(1e20));
}
vec3 median(vec3 a, vec3 b, vec3 c)
{
  return a + b + c - min(min(a, b), c) - max(max(a, b), c);
}

float getBrightness(vec3 col) {

	return max(col.r, max(col.g, col.b));
	return (col.r + col.r + col.b + col.g + col.g + col.g) / 6.0;
}

void main() {
	scene = vec4(safe_color(GetRenderColorOffset(vec2(0)).rgb), 1);

	vec3 d = renderedTextureTexelSize.xyx * vec3(1,1,0);
	vec3 s0 = scene.rgb + vec3(.1);
	vec3 s1 = safe_color(GetRenderColorOffset(-d.xz).rgb) + vec3(.1);
	vec3 s2 = safe_color(GetRenderColorOffset(+d.xz).rgb) + vec3(.1);
	vec3 s3 = safe_color(GetRenderColorOffset(-d.zy).rgb) + vec3(.1);
	vec3 s4 = safe_color(GetRenderColorOffset(+d.zy).rgb) + vec3(.1);
	vec3 col = median(median(s0, s1, s2), s3, s4);
	float br = getBrightness(col);

	/*vec3 col = safe_color(GetRenderColor().rgb);
	float br = getBrightness(col);*/


	float rq = clamp(br - ThresholdCurve.x, 0, ThresholdCurve.y);
	rq = ThresholdCurve.z * rq * rq;

	float resultBr = max(rq, br - ThresholdCurve.w) / max(1e-5, br);
	col *= resultBr;

	color = vec4(col, 1);
}
