Shader "Unlit/HmmHMM"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex("Noise texture", 2D) = "white" {}
		_Color1("First Edge color", Vector) = (1, 1 , 1, 1)
		_Color2("Second Edge color", Vector) = (1, 1 , 1, 1)
		_Speed("Speed of the effect", Float) = 1
		_EdgeRadius1("First Edge size, from 1 - 0", Float) = 1
		_EdgeRadius2("Second Edge size, from 1 - 0", Float) = 1
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
			float3 _Color1;
			float3 _Color2;
			float _Speed;
			float _EdgeRadius1;
			float _EdgeRadius2;
			float timeSinceLevelLoaded;
			
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
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 noiseCol = tex2D(_NoiseTex, i.uv);
				
				float colaKerroin = (noiseCol.a + 1) * (0.8 - (_Time[1] - timeSinceLevelLoaded) * _Speed); //0.8 että se alkaa heti... Siis jossain olisi voinut laskea varmaan jotain mutta tuo on nyt "kokeellisesti" määritetty.
				if (colaKerroin < 1 - (_EdgeRadius2 + _EdgeRadius1))
				{
					colaKerroin = 0;
				}
				else if (colaKerroin > 1) 
				{
					colaKerroin = 1;
				}
				else if (colaKerroin > 1 - _EdgeRadius1) 
				{
					col.r = _Color1.x;
					col.g = _Color1.y;
					col.b = _Color1.z;
				}
				else 
				{
					col.r = _Color2.x;
					col.g = _Color2.y;
					col.b = _Color2.z;
				}
				col.a *= colaKerroin;

				UNITY_APPLY_FOG(i.fogCoord, col)
				return col;
			}
			ENDCG
		}
	}
}
