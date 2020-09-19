#version 330
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTex;

uniform mat4 MVP;
uniform mat4 ModelMatrix;

out vec2 vTexture;

void main() {
    vTexture = aTex;

    gl_Position = MVP * ModelMatrix * vec4(aPos, 1);
}