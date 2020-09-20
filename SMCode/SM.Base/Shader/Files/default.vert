#version 330
#define maxInstances 32
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTex;

uniform mat4 MVP;
uniform mat4 ModelMatrix[maxInstances];
uniform vec2 TextureOffset[maxInstances];
uniform vec2 TextureScale[maxInstances];

out vec2 vTexture;

void main() {
    vTexture = aTex * TextureScale[gl_InstanceID] + TextureOffset[gl_InstanceID];

    gl_Position = MVP * ModelMatrix[gl_InstanceID] * vec4(aPos, 1);
}