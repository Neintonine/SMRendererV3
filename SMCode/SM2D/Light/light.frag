#version 330
#define PI 3.14159265359

struct Light {
	int Type;
	vec2 Position;
	vec4 Color;

	// pointStuff;
	float Power;
	float Inner;
	float Outer;
};

in vec2 vTexture;
in vec2 FragPos;

uniform vec2 FragSize;

uniform vec4 Ambient = vec4(1);
uniform Light[24] Lights;
uniform int LightCount;

uniform float ShadowSensitivty;
uniform sampler2D OccluderMap;

layout(location = 0) out vec4 color;

vec4 GetRenderColor();

vec3 calcPointLight(Light light) {
	vec2 diff = light.Position - FragPos;

	float dif = 20 / length(diff);
	float intensity = light.Power * (dif) * (dif * dif);

	return vec3(light.Color * intensity);
}

float occluded(Light light) {
	float occluder = 1 - length(texture(OccluderMap, vTexture).rgb);

	if (occluder != 0) {
		vec2 diff = light.Position - FragPos;
		vec2 dir = normalize(diff);

		float steps = length(diff) / ShadowSensitivty;

		vec2 curPos = FragPos;
		for(int i = 0; i < steps; i++) {
			curPos += dir * i * ShadowSensitivty;
		}
	}
	return occluder;
}

vec3 calcLight() {
	vec3 addedLight = vec3(0);

	for(int i = 0; i < LightCount; i++) {
		Light light = Lights[i];

		vec3 lightColor;
		switch(light.Type) {
			case 0:
				lightColor += calcPointLight(light);
				break;
		}
		addedLight += lightColor * occluded(light);
	}

	return addedLight;
}

void main() {
	vec4 render = GetRenderColor();

	color = render * Ambient;

	color += vec4(calcLight(), 1);
}