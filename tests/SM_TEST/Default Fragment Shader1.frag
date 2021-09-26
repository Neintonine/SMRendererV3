#version 330 core

uniform vec4 Color;
uniform float Scale;

layout(location = 0) out vec4 color;

void main() {
	color = Color * Scale;
}
