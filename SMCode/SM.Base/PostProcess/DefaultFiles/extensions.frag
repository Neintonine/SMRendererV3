#version 330

in vec2 vTexture;

uniform sampler2D renderedTexture;

vec4 GetRenderColor() {
	return texture(renderedTexture, vTexture);
}