#version 330 core

in vec2 vTexture;

uniform sampler2D baseBuffer;

layout(location = 0) out vec4 color;

vec3 upsample_filter_high();

void main() {
	color = vec4(texture2D(baseBuffer, vTexture).rgb + upsample_filter_high(),1);
}
