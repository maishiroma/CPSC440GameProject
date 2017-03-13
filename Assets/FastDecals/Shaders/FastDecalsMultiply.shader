Shader "Fast Decals/Multiply" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

	Category {
		Tags { "Queue"="Transparent+100" "IgnoreProjector"="True" "RenderType"="Transparent" }

		Blend Zero SrcColor

		Cull Off Lighting Off ZWrite Off Fog { Mode Off }

		SubShader {
			Pass {
				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest

				#include "UnityCG.cginc"

				struct vertexInput {
					float4 vertex : POSITION;
					float2 texcoord : TEXCOORD0;
					fixed4 color : COLOR;
				};

				struct vertexOutput {
					float4 position : SV_POSITION;
					float2 texcoord : TEXCOORD0;
					fixed4 color : COLOR;
				};

				sampler2D _MainTex;
				uniform float4 _MainTex_ST;

				vertexOutput vert (vertexInput input) {
					vertexOutput output;
					output.texcoord = TRANSFORM_TEX(input.texcoord, _MainTex);
					output.position = mul(UNITY_MATRIX_MVP, input.vertex);
					output.color = input.color;
					return output;
				}

				fixed4 frag (vertexOutput output) : COLOR0 {
					half4 tex = tex2D(_MainTex, output.texcoord) * output.color;
					return lerp(half4(1,1,1,1), tex, tex.a);
				}

				ENDCG
			}
		}
	}
	Fallback "Mobile/Particles/Multiply"
}