Shader "Custom/Lit_Study"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (1,0,0,1)
        [MainTexture] _BaseMap("Base Map", 2D) = "white" {}
    } 

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"            
        }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                float4 _BaseMap_ST;
            CBUFFER_END

            TEXTURE2D(_BaseMap);//텍스쳐 오브젝트
            SAMPLER(sampler_BaseMap);//샘플러

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float3 normal       : NORMAL;
                float2 uv           : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float3 normal       : NORMAL;
                float2 uv           : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normal = TransformObjectToWorldNormal(IN.normal.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv,_BaseMap);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float4 final = SAMPLE_TEXTURE2D(_BaseMap,sampler_BaseMap,IN.uv);
                final = final * _BaseColor;
                final.xyz = IN.normal;
                return final;                
            }
            ENDHLSL
        }
    }
    //ref: https://chulin28ho.tistory.com/866
}
