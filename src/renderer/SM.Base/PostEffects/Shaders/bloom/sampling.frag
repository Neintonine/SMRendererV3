#version 330 core


uniform vec2 renderedTextureTexelSize;
uniform float sampleSize;

vec4 GetRenderColorOffset(vec2);


vec3 upsample_filter_high() {
	vec4 d = renderedTextureTexelSize.xyxy * vec4(1, 1,-1,0);

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

	return s * 0.0625; // 1 / 16 = 0.0625
}