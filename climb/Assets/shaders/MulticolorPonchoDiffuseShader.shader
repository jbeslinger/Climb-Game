// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/MulticolorPonchoDiffuseShader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_SwapTex("Color Data", 2D) = "transparent" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Lambert alpha	
		#pragma multi_compile _ PIXELSNAP_ON
		#include "UnityCG.cginc"

		struct Input
		{
			float2 uv_MainTex;
			float4 color: COLOR; 
		};	

		sampler2D _MainTex;
		sampler2D _AlphaTex;
		float _AlphaSplitEnabled;
		fixed4 _Color;

		sampler2D _SwapTex;

		fixed4 SampleSpriteTexture (float2 uv)
		{
			fixed4 color = tex2D (_MainTex, uv);
			if (_AlphaSplitEnabled)
			color.a = tex2D (_AlphaTex, uv).r;

			return color;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 c = SampleSpriteTexture (IN.uv_MainTex);
			fixed4 swapCol = tex2D(_SwapTex, float2(c.x, 0));
			fixed4 final = lerp(c, swapCol, swapCol.a) * IN.color;
			o.Albedo = final.rgb * final.a * _Color; // vertex RGB
			o.Alpha = final.a;
		}
		ENDCG
	}
}