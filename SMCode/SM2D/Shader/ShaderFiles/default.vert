#version 330
#define maxInstances 32
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTex;
layout(location = 3) in vec4 aColor;

uniform mat4 MVP;
uniform bool HasVColor;
uniform mat4 ModelMatrix[maxInstances];
uniform vec2 TextureOffset[maxInstances];
uniform vec2 TextureScale[maxInstances];

out vec2 vTexture;
out vec4 vColor;

void main() {
    vTexture = aTex * TextureScale[gl_InstanceID] + TextureOffset[gl_InstanceID];

    if (HasVColor) vColor = aColor;
    else vColor = vec4(1);

    gl_Position = MVP * ModelMatrix[gl_InstanceID] * vec4(aPos, 1);
}