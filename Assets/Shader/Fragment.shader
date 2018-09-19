Shader "Unlit/Fragment"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex("Noise texture", 2D) = "white" {}
		_ReadTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags{
		"RenderType" = "Transparent"
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
	}
		Cull Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
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
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _NoiseTex;
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
				fixed4 col = tex2D(_MainTex, i.uv);
				float width = 0.05;
				float2 tearPos = float2((i.uv.x * (1 / width) - frac(i.uv.x * (1 / width))) * width, (i.uv.y * (1 / width) - frac(i.uv.y * (1 / width))) * width);
				float2 velocity = float2(0.02 + tex2D(_NoiseTex, i.uv).z / 10, 0.03 + tex2D(_NoiseTex, i.uv).z / 10);
				float2 tearPosNow = _Time[1] * velocity + tearPos;

				if (i.uv.x >= tearPosNow.x && i.uv.x <= tearPosNow.x + width && i.uv.y >= tearPosNow.y && i.uv.y <= tearPosNow.y + width)
				{
					col = tex2D(_MainTex, float2(tearPos.x - tearPosNow.x + i.uv.x, tearPos.y - tearPosNow.y + i.uv.y));
				}
				else if (i.uv.x >= tearPos.x && i.uv.x <= tearPos.x + width && i.uv.y >= tearPos.y && i.uv.y <= tearPos.y + width)
				{
					col = float4(0.5, 0.5, 0.5, 0.2);
				}

				return col;
			}
			ENDCG
		}
	}
}
