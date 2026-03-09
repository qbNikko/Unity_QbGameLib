Shader "QbGameLib/PostProcessing/PixelShader"
{
    SubShader
    {
        Tags {"RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline" }
        ZWrite Off Cull Off
        HLSLINCLUDE
        
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
        
        SamplerState sampler_point_clamp;
        
        uniform float2 _BlockCount;
        uniform float2 _BlockSize;
        uniform float2 _HalfBlockSize;
        
        ENDHLSL

        Pass
        {
            Name "Pixelization"

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment frag
            
            half4 frag(Varyings input) : SV_TARGET0
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                
                float2 uv = input.texcoord.xy;
                float2 blockPos = floor(uv * _BlockCount);
                float2 blockCenter = blockPos * _BlockSize + _HalfBlockSize;

                float4 tex = SAMPLE_TEXTURE2D(_BlitTexture, sampler_point_clamp, blockCenter);

                return tex;
            }
            ENDHLSL
        }
    }
}