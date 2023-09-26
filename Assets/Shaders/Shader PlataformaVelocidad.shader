// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader PlataformaVelocidad"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color21 = IsGammaSpace() ? float4(0.7372549,0.07843137,0.199665,0) : float4(0.5028866,0.00699541,0.03300047,0);
			float4 color10 = IsGammaSpace() ? float4(0.7372549,0.07843137,0.3566116,0) : float4(0.5028866,0.00699541,0.1044635,0);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float mulTime5 = _Time.y * 15.0;
			float temp_output_15_0 = ( ( sin( ( ( ase_vertex3Pos.z * -1.0 ) + mulTime5 ) ) + 1.0 ) / 2.0 );
			float4 lerpResult20 = lerp( color21 , color10 , temp_output_15_0);
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode1 = tex2D( _TextureSample0, uv_TextureSample0 );
			o.Albedo = ( lerpResult20 * tex2DNode1 ).rgb;
			float4 color19 = IsGammaSpace() ? float4(0.09901208,0.7947788,0.8396226,0) : float4(0.009870192,0.5950156,0.673178,0);
			o.Emission = ( ( temp_output_15_0 * ( 1.0 - tex2DNode1 ) ) * color19 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
0;0;1920;1019;2983.008;915.2889;2.397895;True;False
Node;AmplifyShaderEditor.CommentaryNode;25;-1499.019,389.6567;Inherit;False;2040.663;913.6953;Comment;15;7;3;5;4;6;2;19;17;18;13;14;12;8;24;16;Movimiento Flechas;1,1,1,1;0;0
Node;AmplifyShaderEditor.PosVertexDataNode;2;-1477.822,712.253;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-1460.784,857.7109;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1424.773,955.1636;Inherit;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;False;0;15;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-1230.155,710.9705;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;5;-1226.326,958.9014;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-982.6032,712.1505;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;8;-737.1274,718.9603;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-690.525,983.9166;Inherit;False;Constant;_Float4;Float 3;1;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-679.4189,-26.97322;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;-1;None;b989ef5c76d22bb4db0672591b545aee;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;26;-1496.939,-721.3658;Inherit;False;633.7532;635.1379;Comment;4;21;10;23;20;Color del fondo;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-686.755,1070.601;Inherit;False;Constant;_Float3;Float 2;1;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-540.6465,856.5292;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;24;-426.5408,852.4699;Inherit;False;285;303;Comment;1;15;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;15;-376.5407,902.47;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;16;-340.9138,519.1501;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;21;-1446.939,-671.3658;Inherit;False;Constant;_Color2;Color 0;1;0;Create;True;0;0;False;0;0.7372549,0.07843137,0.199665,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;10;-1444.727,-487.7539;Inherit;False;Constant;_Color0;Color 0;1;0;Create;True;0;0;False;0;0.7372549,0.07843137,0.3566116,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-6.68846,899.4011;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;19;13.86025,1122.397;Inherit;False;Constant;_Color1;Color 1;1;0;Create;True;0;0;False;0;0.09901208,0.7947788,0.8396226,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;20;-1128.186,-483.6441;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SinTimeNode;23;-1372.559,-265.2279;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;294.9053,897.5826;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-184.2865,-45.6926;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;646.0021,-53.58069;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Shader PlataformaVelocidad;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;2;3
WireConnection;3;1;4;0
WireConnection;5;0;6;0
WireConnection;7;0;3;0
WireConnection;7;1;5;0
WireConnection;8;0;7;0
WireConnection;14;0;8;0
WireConnection;14;1;12;0
WireConnection;15;0;14;0
WireConnection;15;1;13;0
WireConnection;16;0;1;0
WireConnection;17;0;15;0
WireConnection;17;1;16;0
WireConnection;20;0;21;0
WireConnection;20;1;10;0
WireConnection;20;2;15;0
WireConnection;18;0;17;0
WireConnection;18;1;19;0
WireConnection;9;0;20;0
WireConnection;9;1;1;0
WireConnection;0;0;9;0
WireConnection;0;2;18;0
ASEEND*/
//CHKSM=79FB87A0A7855C5B1CA6D18DF1B0230D61F1CFCB