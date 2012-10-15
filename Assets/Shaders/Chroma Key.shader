Shader "Custom/Chroma Key" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Main Color", COLOR) = (1,1,1,1)
		_KeyColor ("Key Color", COLOR) = (0,0,1,1)
		_Cutoff ("Alpha Cutoff", Range (0, 1)) = 0.5
		_MaxThresholdR ("Key Color Max Threshold Red", Range (0, 1)) = 0
		_MinThresholdR ("Key Color Min Threshold Red", Range (0, 1)) = 0
		_MaxThresholdG ("Key Color Max Threshold Green", Range (0, 1)) = 0
		_MinThresholdG ("Key Color Min Threshold Green", Range (0, 1)) = 0
		_MaxThresholdB ("Key Color Max Threshold Blue", Range (0, 1)) = 0
		_MinThresholdB ("Key Color Min Threshold Blue", Range (0, 1)) = 0
	}
	SubShader 
	{
		Tags { "Queue" = "AlphaTest" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 200
		AlphaTest Greater [_Cutoff]
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		half4 _KeyColor;
		half _MaxThresholdR;
		half _MaxThresholdG;
		half _MaxThresholdB;
		half _MinThresholdR;
		half _MinThresholdG;
		half _MinThresholdB;

		struct Input 
		{
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			if((c.r >= _KeyColor.r - _MinThresholdR  && c.r <= _KeyColor.r + _MaxThresholdR) && (c.g >= _KeyColor.g - _MinThresholdG && c.g <= _KeyColor.g + _MaxThresholdG) && (c.b >= _KeyColor.b - _MinThresholdB && c.b <= _KeyColor.b + _MaxThresholdB))
			{
				o.Albedo = (1,0,0);
				o.Alpha = 0;
			}
			else
			{
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
