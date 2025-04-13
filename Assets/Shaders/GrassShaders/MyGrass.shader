Shader "Custom/MyGrass"
{
    Properties
    {
        _GroundColor ("Ground Color", Color) = (1, 1, 1, 1)
        _TipColor ("Tip Color", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
        
        _BladeWidth ("Blade Width", Range(0, 1)) = 0.5
        _BladeHeight ("Blade Height", Range(0, 2)) = 0.5       
        
        _WindMap("Wind Offset Map", 2D) = "bump" {}
        _WindVelocity("Wind Velocity", Vector) = (1, 0, 0, 0)
        _WindStrength("Wind Strength", Range(0, 1)) = 0.5
        _WindFrequency("Wind Frequency", Range(0, 1)) = 0.01
        
        
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "Queue"="Geometry"
            "RenderPipeline"="UniversalPipeline"            
        }
        LOD 100
        Cull Off
        
        HLSLINCLUDE
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            #pragma multi_compile_local WIND_ON_

            #define UNITY_PI 3.14159265359f
			#define UNITY_TWO_PI 6.28318530718f
            #define BLADE_SEGMENTS 4

            CBUFFER_START(UnityPerMaterial)
                float4 _GroundColor;
                float4 _TipColor;
                sampler2D _MainTex;
                float4 _MainTex_ST;
            
                float _BladeWidth;
                float _BladeHeight;
                float _WindStrength;
                float _WindFrequency;

                sampler2D _WindMap;
                float4 _WindMap_ST;
            CBUFFER_END

            struct appdata
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float2 uv : TEXCOORD0;
            };

            struct tessControlPoint
            {
                float4 positionWS : INTERNALTESSPOS;
                float3 normalWS : NORMAL;
                float4 tangentWS : TANGENT;
                float2 uv : TEXCOORD0;                
            };

            struct v2g
            {
                float4 positionWS : SV_POSITION;
                float3 normalWS  : NORMAL;
                float4 tangentWS : TANGENT;
                float2 uv : TEXCOORD0;                
            };

            struct g2f
            {
                float4 positionCS : SV_POSITION;
                float3 normalWS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            float rand01(float3 co)
            {
                //(https://github.com/IronWarrior/UnityGrassGeometryShader)
                //https://forum.unity.com/threads/am-i-over-complicating-this-random-function.454887/#post-2949326
                return frac(sin(dot(co, float3(12.9898, 78.233, 37.719))) * 43758.5453);
            }

            float3x3 angleAxis3x3(float angle, float axis)
            {
                //https://gist.github.com/keijiro/ee439d5e7388f3aafc5296005c8c3f33
                float c, s;
				sincos(angle, s, c);

				float t = 1 - c;
				float x = axis.x;
				float y = axis.y;
				float z = axis.z;

				return float3x3
				(
					t * x * x + c, t * x * y - s * z, t * x * z + s * y,
					t * x * y + s * z, t * y * y + c, t * y * z - s * x,
					t * x * z - s * y, t * y * z + s * x, t * z * z + c
				);
            }

            float3x3 identity3x3()
			{
				return float3x3
				(
					1, 0, 0,
					0, 1, 0,
					0, 0, 1
				);
			}

            tessControlPoint vert(appdata v)
            {
                tessControlPoint o;
                o.positionWS = float4(TransformObjectToWorld(v.positionOS), 1.0f);
                o.normalWS = TransformObjectToWorldNormal(v.normalOS);
                o.tangentWS = v.tangentOS;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
        ENDHLSL

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
