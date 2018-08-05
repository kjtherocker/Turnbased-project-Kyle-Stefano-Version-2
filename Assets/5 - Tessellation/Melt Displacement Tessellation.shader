Shader "Custom/5 - Tessellation/Base" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_MeltY("Melt Y", Float) = 0.0
		_MeltDistance("Melt Distance", Float) = 1.0
		_MeltCurve("Melt Curve", Range(1.0,10.0)) = 2.0

		_Tess("Tessellation Amount", Range( 1, 32 )) = 10
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM


        // add the tessellate function here
		#pragma surface surf Standard fullforwardshadows vertex:disp addshadow tessellate:tessDistance nolightmap

		// to use tessellation we must target shader model 4.6 and up
		#pragma target 4.6

		// include Unity's tessellation code
        #include "Tessellation.cginc"

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		half _MeltY;
		half _MeltDistance;
		half _MeltCurve;

		float _Tess;

		struct appdata {
            float4 vertex : POSITION;
            float4 tangent : TANGENT;
            float3 normal : NORMAL;
            float2 texcoord : TEXCOORD0;
        };

		float4 tessDistance(appdata v0, appdata v1, appdata v2)
        {
            float minDist = 10.0;
            float maxDist = 25.0;

            // this unity function scales how much tessellation based on how close the camera is
            // objects further away keep the same vert count as before

            // the last parameter is the factor of tessellation, increasing it in the material inspector increases verts created
            return UnityDistanceBasedTess(v0.vertex, v1.vertex, v2.vertex, minDist, maxDist, _Tess);
        }

		float4 getNewVertPosition( float4 objectSpacePosition, float3 objectSpaceNormal )
		{
			float4 worldSpacePosition = mul( unity_ObjectToWorld, objectSpacePosition );
			float4 worldSpaceNormal   = mul( unity_ObjectToWorld, float4(objectSpaceNormal,0) );

			float melt = ( worldSpacePosition.y - _MeltY ) / _MeltDistance;

			melt = 1 - saturate( melt );
			melt = pow( melt, _MeltCurve );

			worldSpacePosition.xz += worldSpaceNormal.xz * melt;

			return mul( unity_WorldToObject, worldSpacePosition );
		}

		void disp( inout appdata v )
		{
			float4 vertPosition = getNewVertPosition( v.vertex, v.normal );

			float4 bitangent = float4( cross( v.normal, v.tangent ), 0 );
			float vertOffset = 0.01;

			float4 v1 = getNewVertPosition( v.vertex + v.tangent * vertOffset, v.normal );
			float4 v2 = getNewVertPosition( v.vertex + bitangent * vertOffset, v.normal );

			float4 newTangent = v1 - vertPosition;
			float4 newBitangent = v2 - vertPosition;

			v.normal = cross( newTangent, newBitangent );

			v.vertex = vertPosition;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
