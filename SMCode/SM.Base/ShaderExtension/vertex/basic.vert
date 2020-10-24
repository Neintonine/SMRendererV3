#version 330
#define maxInstances //!instanceMax

struct Instance {
    mat4 ModelMatrix;
    vec2 TextureOffset;
    vec2 TextureScale;
};

layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTex;
layout(location = 3) in vec4 aColor;

uniform mat4 MVP;
uniform bool HasVColor;
uniform Instance[maxInstances] Instances;

out vec2 vTexture;
out vec4 vColor;
out vec3 FragPos;

void ApplyTexModifier() {
    vTexture = aTex * Instances[gl_InstanceID].TextureScale + Instances[gl_InstanceID].TextureOffset;
}

void CheckVertexColor() {
    if (HasVColor) vColor = aColor;
    else vColor = vec4(1);
}

void ApplyModelTransformation() {
    gl_Position = MVP * Instances[gl_InstanceID].ModelMatrix * vec4(aPos, 1);

    FragPos = vec3(Instances[gl_InstanceID].ModelMatrix * vec4(aPos, 1));
}