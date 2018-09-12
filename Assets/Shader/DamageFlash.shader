Shader "Unlit/DamageFlash"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_WhiteAmount("How little white, 1 is white 100 is normal..", Float) = 1
		_Color("Color", Vector) = (1, 1, 1)
	}
	SubShader
	{
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off

		Tags
		{
			"RenderType" = "Opaque"
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
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
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			float _WhiteAmount;
			float3 _Color;
			

			fixed4 frag (v2f i) : SV_Target
			{
				
				fixed4 col = tex2D(_MainTex, i.uv);
				col = float4(col.r + (1 - col.r) / _WhiteAmount, col.g + (1 - col.g) / _WhiteAmount, col.b + (1 - col.b) / _WhiteAmount, col.a);
				col.r *= _Color.x;
				col.g *= _Color.y;
				col.b *= _Color.z;
				return col;
			}
			ENDCG
		}
	}
}
