// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Dissolve"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Color("Color", 2D) = "white" {}
		_Frequency("Frequency", Float) = 1
		_Noise("Noise", 2D) = "white" {}
		_DisolveAmount("DisolveAmount", Range( 0 , 1)) = 0
		_WhiteAmount("WhiteAmount", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _Color;
		uniform float4 _Color_ST;
		uniform float _WhiteAmount;
		uniform sampler2D _Noise;
		uniform float _Frequency;
		uniform float _DisolveAmount;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Color = i.uv_texcoord * _Color_ST.xy + _Color_ST.zw;
			float4 break11 = tex2D( _Color, uv_Color );
			float4 appendResult12 = (float4(break11.r , break11.g , break11.b , 0.0));
			float4 color22 = IsGammaSpace() ? float4(1,0.2130956,0.0990566,1) : float4(1,0.03733269,0.009877041,1);
			float4 lerpResult23 = lerp( appendResult12 , color22 , _WhiteAmount);
			o.Albedo = lerpResult23.rgb;
			o.Emission = ( color22 * _WhiteAmount ).rgb;
			o.Alpha = 1;
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float3 break9 = ( ase_vertex3Pos * _Frequency );
			float4 appendResult10 = (float4(break9.x , break9.y , 0.0 , 0.0));
			clip( ( ( break11.a * tex2D( _Noise, appendResult10.xy ).r ) + ( ( ( 1.0 - _DisolveAmount ) * 2.0 ) - 1.0 ) ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18800
360;91.5;1071;711;1870.346;422.2346;2.129232;True;False
Node;AmplifyShaderEditor.RangedFloatNode;7;-1922.665,591.9481;Inherit;False;Property;_Frequency;Frequency;2;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;21;-1927.844,441.2146;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-1606.764,511.3484;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-1509.218,872.7668;Inherit;False;Property;_DisolveAmount;DisolveAmount;4;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;9;-1461.755,512.1857;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;17;-1266.535,1016.525;Inherit;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;10;-1302.225,510.7085;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.OneMinusNode;15;-1225.415,877.8618;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1482.384,-265.3475;Inherit;True;Property;_Color;Color;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;19;-1052.877,1065.312;Inherit;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;8;-1130.893,464.3573;Inherit;True;Property;_Noise;Noise;3;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-1051.195,912.2193;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;11;-1112.888,-236.5381;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleSubtractOpNode;18;-859.4072,939.137;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-691.7028,334.8461;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;12;-909.5264,-226.0594;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ColorNode;22;-962.7968,29.48103;Inherit;False;Constant;_Color0;Color 0;5;0;Create;True;0;0;0;False;0;False;1,0.2130956,0.0990566,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;24;-945.021,219.6672;Inherit;False;Property;_WhiteAmount;WhiteAmount;5;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;23;-349.5972,-47.03267;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;20;-570.3945,706.0936;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-511.8963,95.16871;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;13.3,-43.70001;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Dissolve;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;21;0
WireConnection;6;1;7;0
WireConnection;9;0;6;0
WireConnection;10;0;9;0
WireConnection;10;1;9;1
WireConnection;15;0;14;0
WireConnection;8;1;10;0
WireConnection;16;0;15;0
WireConnection;16;1;17;0
WireConnection;11;0;1;0
WireConnection;18;0;16;0
WireConnection;18;1;19;0
WireConnection;13;0;11;3
WireConnection;13;1;8;1
WireConnection;12;0;11;0
WireConnection;12;1;11;1
WireConnection;12;2;11;2
WireConnection;23;0;12;0
WireConnection;23;1;22;0
WireConnection;23;2;24;0
WireConnection;20;0;13;0
WireConnection;20;1;18;0
WireConnection;25;0;22;0
WireConnection;25;1;24;0
WireConnection;0;0;23;0
WireConnection;0;2;25;0
WireConnection;0;10;20;0
ASEEND*/
//CHKSM=BBA6C33076ED1D3D60BE0CC100810BA716E93018