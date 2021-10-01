#version 330 core
/* ACTIONS:
0 = Filtering
1 = Downsamping
2 = Upsampling
3 = Combine
*/

in vec2 vTexture;

uniform vec2 renderedTextureTexelSize;
// Uniforms
uniform vec4 ThresholdCurve;


// Downsampling

uniform float sampleSize;
uniform sampler2D baseBuffer;

in vec2 amountUV;

uniform sampler2D scene;
uniform vec4 bloomColor;
uniform bool HDR;

uniform bool hasAmountMap;
uniform sampler2D amountMap;
uniform vec2 amountLimit;


layout(location = 0) out vec4 color;
layout(location = 1) out vec4 sceneOutput;

vec4 GetRenderColorOffset(vec2);
vec3 reinhardTone(vec3);

// ---- Utils ----
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

// ---- Functions ----
vec3 simpleBoxFilter() {
	#if defined (ACTION_DOWNSAMPLING)
	vec4 d = renderedTextureTexelSize.xyxy * vec4(-1,-1,1,1);
	#else
	vec4 d = renderedTextureTexelSize.xyxy * vec4(-1,-1,1,1) * (sampleSize * 0.5);
	#endif

	vec3 s;
	s = GetRenderColorOffset(d.xy).rgb;
	s += GetRenderColorOffset(d.zy).rgb;
	s += GetRenderColorOffset(d.xw).rgb;
	s += GetRenderColorOffset(d.zw).rgb;

	return s * 0.25; // 1 / 4 = 0.25
}

// Downsampling:
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

// Upsampling:
vec3 upsample_high() {
	vec4 d = renderedTextureTexelSize.xyxy * vec4(1, 1,-1,0) * sampleSize;

	vec3 s;
	// Line + 1
	s = GetRenderColorOffset(d.zy).rgb;			// x - -
	s += GetRenderColorOffset(d.wy).rgb * 2;	// - X -
	s += GetRenderColorOffset(d.xy).rgb;		// - - X

	// Line 0
	s += GetRenderColorOffset(d.zw).rgb * 2;	// X - -
	s += GetRenderColorOffset(vec2(0)).rgb * 4;	// - X -
	s += GetRenderColorOffset(d.xw).rgb * 2;	// - - X
	
	// Line - 1
	s += GetRenderColorOffset(d.zz).rgb;		// X - -
	s += GetRenderColorOffset(d.wz).rgb * 2;	// - X -
	s += GetRenderColorOffset(d.xz).rgb;		// - - X

	return texture2D(baseBuffer, vTexture).rgb + s * 0.0625; // 1 / 16 = 0.0625
}

// ---- Actions ----
vec3 filtering() {


	vec3 col = safe_color(GetRenderColorOffset(vec2(0)).rgb);
	sceneOutput = vec4(col, 1);
	return sceneOutput.rgb;

	#ifdef HIGH
	vec3 d = renderedTextureTexelSize.xyx * vec3(1,1,0);
	vec3 s0 = col + vec3(.1);
	vec3 s1 = safe_color(GetRenderColorOffset(-d.xz).rgb) + vec3(.1);
	vec3 s2 = safe_color(GetRenderColorOffset(+d.xz).rgb) + vec3(.1);
	vec3 s3 = safe_color(GetRenderColorOffset(-d.zy).rgb) + vec3(.1);
	vec3 s4 = safe_color(GetRenderColorOffset(+d.zy).rgb) + vec3(.1);
	vec3 col = median(median(s0, s1, s2), s3, s4);
	#endif
	
	float br = getBrightness(col);

	float rq = clamp(br - ThresholdCurve.x, 0, ThresholdCurve.y);
	rq = ThresholdCurve.z * rq * rq;

	float resultBr = max(rq, br - ThresholdCurve.w) / max(1e-5, br);
	return col * resultBr;
}

vec3 downsample() {
	#ifdef HIGH
	return downsample_high();
	#else
	return simpleBoxFilter();
	#endif
}

vec3 upsample() {
	#ifdef HIGH
	return upsample_high();
	#else
	return simpleBoxFilter();
	#endif
}

vec3 combine() {
	vec3 scene = safe_color(texture2D(scene, vTexture).rgb);
	vec3 blur = upsample() * bloomColor.rgb;

	if (hasAmountMap) {
		blur *= clamp(texture2D(amountMap, amountUV).r * (amountLimit.y - amountLimit.x) + amountLimit.x, 0, 1);
	}

	if (HDR) {
		return scene + blur;
	}

	return scene + reinhardTone(blur);
}

// main:
void main() {
	vec3 col;

	#if defined(ACTION_FILTERING)
	col = filtering();
	#elif defined(ACTION_DOWNSAMPLING)
	col = downsample();
	#elif defined(ACTION_UPSAMPLING)
	col = upsample();
	#else
	col = combine();
	#endif

	color = vec4(col, 1);
}
