Shader "Custom/VR/VR With 2 Color"
{
	Properties
	{
		// the black color on the texture will become the second color.
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color 1", Color) = (1, 1, 1, 1)
		_ColorAlt("Color 2", Color) = (0, 0, 0, 1)
	}
		SubShader
		{
			Tags
			{
				"RenderType" = "Opaque"
			}
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
				fixed4 _Color;
				fixed4 _ColorAlt;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);
				
					if (col.x + col.y + col.z < .1f)
						return col * _ColorAlt;
					return col * _Color;
				}
				ENDCG
			}
		}
}