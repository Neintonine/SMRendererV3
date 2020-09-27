#version 330

in vec2 vTexture;
in vec4 vColor;

uniform vec4 Tint;
uniform bool UseTexture;
uniform sampler2D Texture;

layout(location = 0) out vec4 color;

void main() {
    color = vColor * Tint;
    if (UseTexture) color *= texture(Texture, vTexture);
}