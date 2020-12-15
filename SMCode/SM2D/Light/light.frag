#version 330
#define PI 3.14159265359

struct Light {
	int Type;
	vec2 Position;
	vec4 Color;

	// pointStuff;
	float Power;
};

in vec2 vTexture;
in vec2 FragPos;

uniform vec4 Ambient = vec4(1);
uniform Light[24] Lights;
uniform int LightCount;

layout(location = 0) out vec4 color;

vec4 GetRenderColor();

vec3 calcPointLight(Light light) {
	vec2 diff = light.Position - FragPos;

	float dif = light.Power / length(diff);
	float intensity = 4 * PI * dif * (dif * dif);

	return vec3(intensity);
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
	vec4 render = GetRenderColor();

	color = render * Ambient;

	color += vec4(calcLight() * render.xyz, 1);
}