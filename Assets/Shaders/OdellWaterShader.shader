Shader "OdellWaterShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_BlendColor("Blend Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_InvFade("Soft Factor", Range(0.01,3.0)) = 1.0
		_FadeLimit("Fade Limit", Range(0.00,1.0)) = 0.3
		_Scale("Scale", float) = 1
		_Speed("Speed", float) = 1
		_RipplePosition("Ripple Position",  Vector) = (0, 0, 0, 0)
		_WaterAlpha("WaterAlpha", float) = 0.9
		_Amplitude("RippleStrength", float) = 5
		_Wave("Wave", float) = 5
		_Frequency("Frequency", float) = 1
		_IsRippling("CanRipple", float) = 1
	}
		SubShader{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard vertex:vert alpha:fade nolightmap

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

		sampler2D _MainTex;

	struct Input {
		float2 uv_MainTex;
		float4 screenPos;
		float eyeDepth;
		float3 customValue;
	};

	half _Glossiness;
	half _Metallic;
	fixed4 _Color;
	fixed4 _BlendColor;
	float _Scale;
    float _Speed;
	float _Frequency;

	float _Amplitude;
	float _Wave;


	float _WaterAlpha;
	sampler2D_float _CameraDepthTexture;
	float4 _CameraDepthTexture_TexelSize;
	float4 _RipplePosition;
	float _FadeLimit;
	float _InvFade;

	float _IsRippling;

	void vert(inout appdata_full v, out Input o)
	{
		//NormalWave
		UNITY_INITIALIZE_OUTPUT(Input, o);
		COMPUTE_EYEDEPTH(o.eyeDepth);
		half offsetvert = ((v.vertex.x * v.vertex.x) + (v.vertex.z * v.vertex.z));
		half offsetvert2 = v.vertex.x + v.vertex.z;
		half value0 = _Scale * sin(_Time.w * _Speed * _Frequency + offsetvert2);


		//Ripple

		
		//float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
		//float3 RippleStartPoint = _RipplePosition.xyz;
		//float DistanceToPoint = length((worldPos + RippleStartPoint) / 100);
        //
		//v.vertex.y += _Amplitude * sin(-3.14 * DistanceToPoint * _Wave + _Time);
	
			//NormalWave
		v.vertex.y += value0;
		v.normal.y += value0;
		o.customValue += value0;
		
	}

	void surf(Input IN, inout SurfaceOutputStandard o) {
		// Albedo comes from a texture tinted by color
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		// Metallic and smoothness come from slider variables
		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;

		float CameraDepth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos));
		float SceneDepth = LinearEyeDepth(CameraDepth);
		float partZ = IN.eyeDepth;

		float fade = 1.0;
		if (CameraDepth > 0.0) // Make sure the depth texture exists
			fade = saturate(_InvFade * (SceneDepth - partZ));
		o.Alpha = c.a * fade; //(original line)
		//the rest are lines I've input
		o.Alpha = _WaterAlpha;
		if (fade < _FadeLimit)
		o.Albedo = c.rgb * fade + _BlendColor * (1 - fade);
	}
	ENDCG
	}
}