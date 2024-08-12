Shader "Custom/GlowOutlineShader"
{
    Properties
    {
        _Color ("Glow Color", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _Outline ("Outline Width", Float) = 1.0
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Pass
        {
            Name "OUTLINE"
            ZWrite Off
            ZTest Less
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            // Vertex Shader
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
            };

            float _Outline;
            float4 _OutlineColor;

            v2f vert (appdata v)
            {
                v2f o;
                float4x4 model = unity_ObjectToWorld;
                float4 pos = mul(model, v.vertex);
                pos.xy += normalize(float2(0, 1)) * _Outline; // Adjust to outline width
                o.pos = UnityObjectToClipPos(pos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }

        Pass
        {
            Name "GLOW"
            ZWrite Off
            ZTest Less
            ColorMask RGB
            Blend SrcAlpha One

            // Vertex Shader
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Pass
        {
            Name "MAIN"
            ZWrite On
            ZTest LEqual
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            // Vertex Shader
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
