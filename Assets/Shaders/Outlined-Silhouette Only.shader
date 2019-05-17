// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Outline" {
	Properties{
	_MainTex("MainTex", 2D) = "" {}
	_Outline("_Outline", Range(0,5)) = 0.01
	_OutlineColor("Color", Color) = (0, 0, 0, 0)
    _Color("Color", Color) = (1, 1, 1, 1)
	_Glossiness("Smoothness", Range(0,1)) = 0.5
	_SliceGuide("Slice Guide (RGB)", 2D) = "white" {}
	_SliceAmount("Slice Amount", Range(0.0, 1.0)) = 0.5
	_Metallic("Metallic", Range(0,1)) = 0.0
	_Opacity("Opacity", Range(0,1)) = 1.0
	_OutlineExistance("OutlineExistance", Range(0,1)) = 0

	}
		SubShader{
		Pass{
		Tags{ "RenderType" = "Transparent" }
		Cull Front

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		struct v2f {
		float4 pos : SV_POSITION;
	};

	float _Outline;
	float4 _OutlineColor;

	float _OutlineExistance;
	

	float4 vert(appdata_base v) : SV_POSITION
	{
		
		v2f o;
	
		o.pos = UnityObjectToClipPos(v.vertex);
		float3 normal = mul((float3x3) UNITY_MATRIX_MV, v.normal);
		normal.x *= UNITY_MATRIX_P[0][0];
		normal.y *= UNITY_MATRIX_P[1][1];
		o.pos.xy += normal.xy * _Outline;
	
	return o.pos;
		
	}

		half4 frag(v2f i) : COLOR{
		return _OutlineColor;
		}

		ENDCG
	}

		CGPROGRAM
#pragma surface surf Standard 
	sampler2D _MainTex;
	sampler2D _SliceGuide;
	float _SliceAmount;
	struct Input {
		float2 uv_MainTex;
		float2 uv_SliceGuide;
		float _SliceAmount;
	};

	
	half _Glossiness;
	half _Metallic;
	half _Opacity;
	float4 _Color;


	UNITY_INSTANCING_BUFFER_START(Props)
		// put more per-instance properties here
	UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o) {
		// Albedo comes from a texture tinted by color
		float4 c = tex2D(_MainTex, IN.uv_MainTex) ;
		//o.Albedo = c.rgb;
		// Metallic and smoothness come from slider variables
		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;
		clip(tex2D(_SliceGuide, IN.uv_SliceGuide).rgb - _SliceAmount);
		o.Albedo =  _Color;
	}
	ENDCG
	}
		FallBack "Diffuse"
}

