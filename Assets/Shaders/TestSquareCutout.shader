Shader "Custom/TestSquareCutout" {
	Properties {
		_Color("Primary Color", Color) = (1,1,1,1)
		_MainTex("Primary (RGB)", 2D) = "white" {}
		_Color2("Secondary Color", Color) = (1,1,1,1)
		_SecondTex("Secondary (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

	    _FOVColor("CutOutColor", Color) = (1, 1, 1)

		_PositionA("PositionA",  Vector) = (0, 0, 0, 0)
		_PositionB("PositionB",  Vector) = (0, 0, 0, 0)
		_PositionC("PositionC",  Vector) = (0, 0, 0, 0)
		_PositionD("PositionD",  Vector) = (0, 0, 0, 0)


		_Ramp("Toon Ramp (RGB)", 2D) = "gray" {}
		_NoiseTex("Dissolve Noise", 2D) = "white"{}
		_NScale("Noise Scale", Range(0, 10)) = 1
		_DisAmount("Noise Texture Opacity", Range(0.01, 1)) = 0.01
		_Radius("Radius", Range(0, 10)) = 0
		_DisLineWidth("Line Width", Range(0, 2)) = 0
		_DisLineColor("Line Tint", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0


        sampler2D _MainTex, _SecondTex;
        float4 _Color, _Color2;
		sampler2D _NoiseTex;


		float _DisAmount, _NScale;
		float _DisLineWidth;
		float4 _DisLineColor;
		float _Radius;

		float4 _PositionA;
		float4 _PositionB;
		float4 _PositionC;
		float4 _PositionD;


		struct Input 
		{
			float2 uv_MainTex;
			float3 worldPos;
			float3 worldNormal; // built in value for world normal
		};

		half _Glossiness;
		half _Metallic;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)


	    bool isPointInTriangle(float2 p1, float2 p2, float2 p3, float2 pointInQuestion)
		{
			float denominator = ((p2.y - p3.y) * (p1.x - p3.x) + (p3.x - p2.x) * (p1.y - p3.y));
			float a = ((p2.y - p3.y) * (pointInQuestion.x - p3.x) + (p3.x - p2.x) * (pointInQuestion.y - p3.y)) / denominator;
			float b = ((p3.y - p1.y) * (pointInQuestion.x - p3.x) + (p1.x - p3.x) * (pointInQuestion.y - p3.y)) / denominator;
			float c = 1 - a - b;

			return 0 <= a && a <= 1 && 0 <= b && b <= 1 && 0 <= c && c <= 1;
		}


		void surf (Input IN, inout SurfaceOutputStandard o) 
		{


			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 c2 = tex2D(_SecondTex, IN.uv_MainTex) * _Color;

			// triplanar noise
			float3 blendNormal = saturate(pow(IN.worldNormal * 3, 10));
			half4 nSide1 = tex2D(_NoiseTex, (IN.worldPos.xy) );
			half4 nSide2 = tex2D(_NoiseTex, (IN.worldPos.xz + _Time.x) * _NScale);
			half4 nTop = tex2D(_NoiseTex, (IN.worldPos.yz + _Time.x) * _NScale);

			float3 noisetexture = nSide1;
			noisetexture = lerp(noisetexture, nTop, blendNormal.x);
			//noisetexture = lerp(noisetexture, nSide2, blendNormal.y);


			float3 RightPoint = _PositionC.xyz;
			float3 LeftPoint = _PositionB.xyz;
			float3 FarBasePoint = _PositionD.xyz;
			float3 BasePoint = _PositionA.xyz;


			float3 dis = distance(RightPoint, IN.worldPos);
			float3 dis2 = distance(FarBasePoint, IN.worldPos);

			float3 sphere = dis + dis2 / _Radius;


			float3 sphereNoise = noisetexture.r * sphere;


			float3 DissolveLine = step(sphereNoise - _DisLineWidth, _DisAmount) ; // line between two textures
			DissolveLine *= _DisLineColor; // color the line


			float3 primaryTex = (step(_DisLineWidth, _DisAmount) * c.rgb);
			float3 secondaryTex = (step(_DisAmount, sphereNoise) * c2.rgb);
			float3 resultTex = primaryTex + secondaryTex + DissolveLine;//+ secondaryTex;//+ DissolveLine + DissolveLine2;


			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			

			
			float3 PointInQuestion = IN.worldPos;

			if (isPointInTriangle(BasePoint.xz, RightPoint.xz, LeftPoint.xz, PointInQuestion.xz) || isPointInTriangle(LeftPoint.xz, RightPoint.xz, FarBasePoint.xz, PointInQuestion.xz))
			{
				o.Albedo = secondaryTex + DissolveLine;
			}
			else
			{

				o.Albedo = resultTex;
			}


		}
		ENDCG
	}
	FallBack "Diffuse"
}
