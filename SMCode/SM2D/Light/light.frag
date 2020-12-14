#version 330
#define PI 3.14159265359

struct Light {
	int Type;
	vec2 Position;
	vec4 Color;
};

in vec2 vTexture;
in vec2 FragPos;

uniform vec4 Ambient = vec4(1);
uniform Light[24] Lights;
uniform int LightCount;

layout(location = 0) out vec4 color;

vec4 GetRenderColor();

vec3 calcPointLight(Light light) {
	float dis = distance(FragPos, light.Position);
	float intensity = 4 / 4 * PI * pow(dis, 2);
	
	return vec3(light.Color * intensity);
}

vec3 calcLight() {
	vec3 addedLight = vec3(0);

	for(int i = 0; i < LightCount; i++) {
		Light light = Lights[i];

		switch(light.Type) {
			case 0:
				addedLight += calcPointLight(light);
				break;
		}
	}

	return addedLight;
}

void main() {
	color = GetRenderColor() * Ambient;
	color += vec4(calcLight(), 1);
}