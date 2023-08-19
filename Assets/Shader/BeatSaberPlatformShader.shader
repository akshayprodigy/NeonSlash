Shader "Custom/BeatSaberPlatformShader"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _EmissionColor("Emission Color", Color) = (1, 1, 1, 1)
        _OutlineColor("Outline Color", Color) = (0, 0.5, 1, 1)
        _OutlineWidth("Outline Width", Range(0.001, 0.1)) = 0.005
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            Lighting Off
            ZWrite On
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    float4 vertex : SV_POSITION;
                    float3 normal : NORMAL;
                    float2 uv : TEXCOORD0;
                };

                float _OutlineWidth;
                float4 _OutlineColor;
                sampler2D _MainTex;
                float4 _EmissionColor;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.normal = UnityObjectToWorldNormal(v.normal);
                    o.uv = v.vertex.xy; // Or use a proper UV mapping if needed
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = _OutlineColor;
                    float depth = i.vertex.z / i.vertex.w;
                    float4 mainTexColor = tex2D(_MainTex, i.uv);
                    col.rgb *= mainTexColor.rgb;
                    col.rgb += col.rgb * _EmissionColor.rgb * mainTexColor.a;
                    col.a = 1 - saturate(smoothstep(0, _OutlineWidth, depth));
                    return col;
                }
                ENDCG
            }
        }
}