#version 330
uniform vec4 Tint;

layout(location = 0) out vec4 color;

void main() {
    color = vec4(1,1,1,1) + Tint;
}