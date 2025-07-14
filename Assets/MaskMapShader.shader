Shader "Custom/MaskMapShader"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _BaseMap ("Albedo (RGB)", 2D) = "white" {}
        _MaskMap ("Mask Map (R:Metallic, G:Occlusion, B:Detail, A:Smoothness)", 2D) = "white" {}
        _Metallic ("Metallic Scale", Range(0,1)) = 1.0
        _Smoothness ("Smoothness Scale", Range(0,1)) = 1.0
        _OcclusionStrength ("Occlusion Strength", Range(0,1)) = 1.0
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _BumpScale ("Normal Scale", Float) = 1.0
    }
    SubShader
    {
        // URP SubShader
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        LOD 300

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _ADDITIONAL_LIGHTS
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float4 tangentWS : TEXCOORD2;
                float3 positionWS : TEXCOORD3;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
            TEXTURE2D(_MaskMap);
            SAMPLER(sampler_MaskMap);
            TEXTURE2D(_NormalMap);
            SAMPLER(sampler_NormalMap);

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float _Metallic;
                float _Smoothness;
                float _OcclusionStrength;
                float _BumpScale;
            CBUFFER_END

            Varyings vert (Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS);
                output.uv = input.uv;
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.tangentWS = float4(TransformObjectToWorldDir(input.tangentOS.xyz), input.tangentOS.w);
                output.positionWS = TransformObjectToWorld(input.positionOS);
                return output;
            }

            half4 frag (Varyings input) : SV_Target
            {
                // Sample textures
                half4 albedo = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv) * _BaseColor;
                half4 mask = SAMPLE_TEXTURE2D(_MaskMap, sampler_MaskMap, input.uv);
                half3 normalTS = UnpackNormalScale(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, input.uv), _BumpScale);

                // Normal mapping
                float3 normalWS = normalize(input.normalWS);
                float3 tangentWS = normalize(input.tangentWS.xyz);
                float3 bitangentWS = cross(normalWS, tangentWS) * input.tangentWS.w;
                float3x3 TBN = float3x3(tangentWS, bitangentWS, normalWS);
                float3 normal = normalize(mul(normalTS, TBN));

                // PBR properties from mask map
                half metallic = mask.r * _Metallic;
                half occlusion = lerp(1.0, mask.g, _OcclusionStrength);
                half smoothness = mask.a * _Smoothness;

                // Lighting calculations
                InputData inputData = (InputData)0;
                inputData.positionWS = input.positionWS;
                inputData.normalWS = normal;
                inputData.viewDirectionWS = normalize(GetWorldSpaceViewDir(input.positionWS));
                inputData.shadowCoord = TransformWorldToShadowCoord(input.positionWS);

                SurfaceData surfaceData = (SurfaceData)0;
                surfaceData.albedo = albedo.rgb;
                surfaceData.metallic = metallic;
                surfaceData.smoothness = smoothness;
                surfaceData.occlusion = occlusion;
                surfaceData.normalTS = normalTS;
                surfaceData.alpha = albedo.a;

                half4 color = UniversalFragmentPBR(inputData, surfaceData);
                return color;
            }
            ENDHLSL
        }
    }

    // Built-in Render Pipeline Fallback
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _BaseMap;
        sampler2D _MaskMap;
        sampler2D _NormalMap;

        struct Input
        {
            float2 uv_BaseMap;
        };

        fixed4 _BaseColor;
        half _Metallic;
        half _Smoothness;
        half _OcclusionStrength;
        half _BumpScale;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 albedo = tex2D(_BaseMap, IN.uv_BaseMap) * _BaseColor;
            fixed4 mask = tex2D(_MaskMap, IN.uv_BaseMap);

            o.Albedo = albedo.rgb;
            o.Metallic = mask.r * _Metallic;
            o.Smoothness = mask.a * _Smoothness;
            o.Occlusion = lerp(1.0, mask.g, _OcclusionStrength);
            o.Normal = UnpackScaleNormal(tex2D(_NormalMap, IN.uv_BaseMap), _BumpScale);
            o.Alpha = albedo.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}