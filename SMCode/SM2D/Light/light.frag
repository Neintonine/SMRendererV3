#version 330

in vec2 vTexture;

vec4 GetRenderColor();

uniform vec4 Ambient = vec4(1);

layout(location = 0) out vec4 color;

void main() {
	color = GetRenderColor() * Ambient;
}