#version 330
layout(location = 0) in vec3 aPos;

uniform mat4 MVP;
uniform mat4 ModelMatrix;

void main() {
    gl_Position = MVP * ModelMatrix * vec4(aPos, 1);
}