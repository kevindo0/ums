Shader "Custom/RoundRect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TintColor ("Tint Color", Color) = (1, 1, 1, 1)
        //圆角半径
        _Round ("Round Radius", Range(0, 1)) = 0.4
        //图片高宽比
        _AspectRatio ("Aspect Ratio", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
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
            float4 _MainTex_ST;
            float4 _TintColor;
            fixed _Round;
            fixed _AspectRatio;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                fixed r;
                fixed inner;
 
                //左下角
                //平移uv坐标原点
                float2 uv = i.uv - float2(_Round*_AspectRatio, _Round);
                /* if (uv.x <= 0 && uv.y <= 0){
                    uv.x /= _AspectRatio;
                    r = length(uv);
                    clip(_Round - r);
                } */
                //当inner的值为0时，相当于上面的if语句为真
                inner = step(0, uv.x) + step(0, uv.y);
                uv.x /= _AspectRatio;
                r = length(uv);
                clip(_Round - r + inner*2);
 
                //左上角
                uv = i.uv - float2(_Round*_AspectRatio, 1-_Round);
                /* if (uv.x <= 0 && uv.y >= 0){
                    uv.x /= _AspectRatio;
                    r = length(uv);
                    clip(_Round - r);
                } */
                inner = step(0, uv.x) + (1 - step(0, uv.y));
                uv.x /= _AspectRatio;
                r = length(uv);
                clip(_Round - r + inner*2);
 
                //右下角
                uv = i.uv - float2((1-_Round*_AspectRatio), _Round);
                /* if (uv.x >= 0 && uv.y <= 0){
                    uv.x /= _AspectRatio;
                    r = length(uv);
                    clip(_Round - r);
                } */
                inner = 1 - step(0, uv.x) + step(0, uv.y);
                uv.x /= _AspectRatio;
                r = length(uv);
                clip(_Round - r + inner*2);
 
                // //右上角
                uv = i.uv - float2((1-_Round*_AspectRatio), 1-_Round);
                /* if (uv.x >= 0 && uv.y >= 0){
                    uv.x /= _AspectRatio;
                    r = length(uv);
                    clip(_Round - r);
                } */
                //inner = (1 - step(0, uv.x)) + (1 - step(0, uv.y));
                inner = 2 - step(0, uv.x) - step(0, uv.y);
                uv.x /= _AspectRatio;
                r = length(uv);
                clip(_Round - r + inner*2);
 
                fixed4 col = tex2D(_MainTex, i.uv) * _TintColor;
                return col;
            }
            ENDCG
        }
    }
}