Shader "Transparent/Cutout/Transparent" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_CutTex("Cutout (A)", 2D) = "white" {}
		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
	    _RotationSpeedx("Rotation Speedx", Float) = 2.0
		_RotationSpeedy("Rotation Speedy", Float) = 2.0
		_RotationSpeedz("Rotation Speedz", Float) = 2.0
	}

		SubShader{
			Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
			LOD 200

		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		sampler2D _CutTex;
		fixed4 _Color;
		float _Cutoff;
		float _RotationSpeedx;
		float _RotationSpeedy;
		float _RotationSpeedz;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			
			

			float sinX = sin(_RotationSpeedx * _Time);
			float cosX = cos(_RotationSpeedy * _Time);
			float sinY = sin(_RotationSpeedz * _Time);
			float2x2 rotationMatrix = sinX + sinY;
			IN.uv_MainTex = mul(IN.uv_MainTex, rotationMatrix);

			float ca = tex2D(_CutTex, IN.uv_MainTex).a;


			o.Albedo = c.rgb;
			if (ca > _Cutoff)
			  o.Alpha = c.a;
			else
			  o.Alpha = 0;
		}
		ENDCG
		}

			Fallback "Transparent/VertexLit"
}