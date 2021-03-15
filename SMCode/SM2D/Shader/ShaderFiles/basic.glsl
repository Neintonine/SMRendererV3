#version 330
in vec2 v_TexCoords;
in vec4 v_Color;

uniform vec4 Tint;
uniform bool UseTexture;
uniform sampler2D Texture;

layout(location = 0) out vec4 color;

void main() {
    color = v_Color * Tint;
    if (UseTexture) color *= texture(Texture, v_TexCoords);
}