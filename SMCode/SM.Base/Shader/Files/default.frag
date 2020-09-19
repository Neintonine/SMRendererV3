#version 330

in vec2 vTexture;

uniform vec4 Tint;
uniform bool UseTexture;
uniform sampler2D Texture;

layout(location = 0) out vec4 color;

void main() {
    color = Tint;
    if (UseTexture) color *= texture(Texture, vTexture);
}