﻿#version 330 core

in vec2 vTexture;
in vec2 amountUV;

uniform sampler2D scene;
uniform vec4 bloomColor;
uniform bool HDR;

uniform bool hasAmountMap;
uniform sampler2D amountMap;
uniform vec2 amountLimit;

vec3 safe_color(vec3 c) {
	return clamp(c, vec3(0.0), vec3(1e20));
}

vec3 upsample_filter_high();

layout(location = 0) out vec4 color;

vec3 reinhardTone(vec3 col) {
	return col / (col + vec3(1.0));
}

void main() {

	vec3 scene = safe_color(texture2D(scene, vTexture).rgb);
	vec3 blur = upsample_filter_high() * bloomColor.rgb;

	if (hasAmountMap) {
		blur *= clamp(texture2D(amountMap, amountUV).r * (amountLimit.y - amountLimit.x) + amountLimit.x, 0, 1);
	}

	if (HDR) {
		color = vec4(scene + blur, 1);
		return;
	}

	color = vec4(scene + reinhardTone(blur), 1);
}
