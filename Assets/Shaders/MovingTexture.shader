Shader "Custom/Moving Texture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScrollSpeedX ("Scroll Speed X", Float) = 0.0 
        _ScrollSpeedY ("Scroll Speed Y", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                fixed4 vertex : POSITION;
                fixed2 uv : TEXCOORD0;
            };

            struct v2f
            {
                fixed2 uv : TEXCOORD0;
                fixed4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _MainTex_ST;
            fixed _ScrollSpeedX;
            fixed _ScrollSpeedY;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed2 uv = i.uv;
                uv.x += _Time.y * _ScrollSpeedX;
                uv.y += _Time.y * _ScrollSpeedY;
                fixed4 col = tex2D(_MainTex, uv);
                if (col.x < 0.1 || col.y < 0.1 || col.z < 0.1)
                    discard;
                return col;
            }
            ENDCG
        }
    }
}
