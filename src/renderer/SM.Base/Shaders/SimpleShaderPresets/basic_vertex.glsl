#version 330
#define SM_SIMPLE_EXTENSION //!extension

layout(location = 0) in vec3 a_Position;
layout(location = 1) in vec2 a_Texture;
layout(location = 3) in vec4 a_Color;

uniform bool HasVColor;
uniform mat4 MVP;
uniform mat3 MasterTextureMatrix;

out vec3 v_VertexPosition;
out vec2 v_TexCoords;
out vec4 v_Color;

#if (SM_SIMPLE_EXTENSION == 1)
void v_Extension();
#endif

void main() {
    v_Color = vec4(1);
    if (HasVColor) v_Color = a_Color;

    v_TexCoords = vec2(MasterTextureMatrix * vec3(a_Texture, 1));

    v_VertexPosition = a_Position;
    gl_Position = MVP * vec4(a_Position, 1);

    #if (SM_SIMPLE_EXTENSION == 1)
    v_Extension();
    #endif
}