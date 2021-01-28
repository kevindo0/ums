Shader "Custom/RoundMaskMove"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("Radius", float) = 0
        _Move ("Move", Range(0, 100)) = 0
        _TintColor ("Tint Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Radius;
            float _Move;
            float4 _TintColor;
            float4 _MainTex_ST;
            float inner;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                if(1 - _Move + _Radius > 0){
                    float2 uv = i.uv - float2(1 - _Move + _Radius, 0.5);
                    inner = step(0, uv.x);
                    float value = sqrt(pow(0.5, 2) + pow(_Radius, 2));
                    fixed r = length(uv);
                    clip(value - r + inner);
                }
                fixed4 col = tex2D(_MainTex, i.uv) * _TintColor;
                return col;
            }
            ENDCG
        }
    }
}
