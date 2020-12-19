#version 330
//# region vertex

//# import SM_base_vertex_basic
void ApplyTexModifier();
void CheckVertexColor();
void ApplyModelTransformation();

void vmain() {
	ApplyTexModifier();
	CheckVertexColor();
	ApplyModelTransformation();
}

//# region fragment
in vec2 vTexture;
in vec4 vColor;

uniform vec4 Tint;
uniform bool UseTexture;
uniform sampler2D Texture;

layout(location = 0) out vec4 color;

void fmain() {
    color = vColor * Tint;
    if (UseTexture) color *= texture(Texture, vTexture);
}