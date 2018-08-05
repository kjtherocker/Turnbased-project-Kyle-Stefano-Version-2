Shader "Custom/6 - Tessellation + Pixels/Base" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_MeltY("Melt Y", Float) = 0.0
		_MeltDistance("Melt Distance", Float) = 1.0
		_MeltCurve("Melt Curve", Range(1.0,10.0)) = 2.0

		_Tess("Tessellation Amount", Range( 1, 32 )) = 10

		_MeltColor ("Color", Color) = (1,1,1,1)
		_MeltGlossiness ("Smoothness", Range(0,1)) = 0.0
		_MeltMetallic ("Metallic", Range(0,1)) = 0.0
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
			float3 worldPos; // this is a built in variable unity will populate in the surface shader for us to use
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		half _MeltY;
		half _MeltDistance;
		half _MeltCurve;

		float _Tess;

		// add all the specific 'melt' surface settings
		half _MeltGlossiness;
		half _MeltMetallic;
		fixed4 _MeltColor;

		struct appdata {
            float4 vertex : POSITION;
            float4 tangent : TANGENT;
            float3 normal : NORMAL;
            float2 texcoord : TEXCOORD0;
        };

        // A modified version of Unity's UnityCalcDistanceTessFactor that only adds tess verts if the vertex is in the melt range
        float MeltCalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess)
		{
			float3 wpos = mul(unity_ObjectToWorld,vertex).xyz;
			float dist = distance (wpos, _WorldSpaceCameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0);

			float melt = (( wpos.y - _MeltY ) / _MeltDistance);
			// calculate the melt for the world position
			// in our normal vert function we saturate this subtract it from 1 so verts near the ground are 1 and join the mesh at 0
			// we only care if the object is in the 'melt' range, so any value between 0 & 1, regardless of the sign.
			// will add a threshold too so verts near the edges are tessellated too

			if( melt < -0.1 || melt > 1.1 )
			{
				f = 0.01; // set the value to the lower end of the clamp
			}

			// move the tess multiply here for clarity
			return f  * tess;
		}

        // A modified version of Unity's UnityDistanceBasedTess to run our version of TessFactor
        // Distance based tessellation:
		// Tessellation level is "tess" before "minDist" from camera, and linearly decreases to 1
		// up to "maxDist" from camera.
		float4 MeltDistanceBasedTess (float4 v0, float4 v1, float4 v2, float minDist, float maxDist, float tess)
		{
			float3 f;
			f.x = MeltCalcDistanceTessFactor (v0,minDist,maxDist,tess);
			f.y = MeltCalcDistanceTessFactor (v1,minDist,maxDist,tess);
			f.z = MeltCalcDistanceTessFactor (v2,minDist,maxDist,tess);

			return UnityCalcTriEdgeTessFactors (f);
		}

		float4 tessDistance(appdata v0, appdata v1, appdata v2)
        {
            float minDist = 10.0;
            float maxDist = 25.0;

            // this unity function scales how much tessellation based on how close the camera is
            // objects further away keep the same vert count as before

            // the last parameter is the factor of tessellation, increasing it in the material inspector increases verts created
            return MeltDistanceBasedTess(v0.vertex, v1.vertex, v2.vertex, minDist, maxDist, _Tess);
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

		float getMelt( float3 worldSpacePosition )
		{
			float4 objectSpacePosition = mul( unity_WorldToObject, float4( worldSpacePosition, 0 ));
			float melt = ( worldSpacePosition.y - _MeltY ) / _MeltDistance;

			melt = 1 - saturate( melt );
//			melt = pow( melt, _MeltCurve ); // we don't care about the curve for this, just the linear melt value

			// this is the same code as our pixel shaders
			float wave = sin( objectSpacePosition.x * 4 + objectSpacePosition.z * 5 ) * 0.15;
			float hardMelt = step( 0.5, melt + wave );

			return hardMelt;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			float hardMelt = getMelt( IN.worldPos );

			// 'c' controls the color of the surface
			// by default this is the texture tinted by the color
			// we want our melt color to blend into this
			o.Albedo = lerp( c.rgb, _MeltColor.rgb, hardMelt );

			// do the same calcuation to the metallic & smoothness
			o.Metallic = lerp( _Metallic, _MeltMetallic, hardMelt );
			o.Smoothness = lerp( _Glossiness, _MeltGlossiness, hardMelt );
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
