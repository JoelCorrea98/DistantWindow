// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NormalReconstruction"
{
	Properties
	{
		_TessValue( "Max Tessellation", Range( 1, 32 ) ) = 15
		_Displace("Displace", Range( 0 , 0.1)) = 0
		_Displacement("Displacement", Range( 0 , 1)) = 0
		_Frequency("Frequency", Float) = 0
		_DisplacementX("Displacement X", Range( 0 , 1)) = 0
		_DisplacementY("Displacement Y", Range( 0 , 1)) = 0
		_MainColor("MainColor", Color) = (0.1848522,0.4815417,0.5849056,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			half filler;
		};

		uniform float _Displacement;
		uniform float _Frequency;
		uniform float _DisplacementY;
		uniform float _Displace;
		uniform float _DisplacementX;
		uniform float4 _MainColor;
		uniform float _TessValue;

		float4 tessFunction( )
		{
			return _TessValue;
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float3 ase_vertexNormal = v.normal.xyz;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 VertexPosition14 = ( ( ( ase_vertexNormal * _Displacement ) * cos( ( ( ase_vertex3Pos.x * _Frequency ) + _Time.y ) ) ) + ase_vertex3Pos );
			v.vertex.xyz = VertexPosition14;
			float4 ase_vertexTangent = v.tangent;
			float3 ase_vertexBitangent = cross( ase_vertexNormal, ase_vertexTangent) * v.tangent.w * unity_WorldTransformParams.w;
			float3x3 ObjectToTngent35 = float3x3(ase_vertexTangent.xyz, ase_vertexBitangent, ase_vertexNormal);
			float Displace45 = _Displace;
			float3 appendResult63 = (float3(0.0 , Displace45 , 0.0));
			float3 temp_output_66_0 = mul( ( mul( ase_vertex3Pos, ObjectToTngent35 ) + appendResult63 ), ObjectToTngent35 );
			float3 YDeviated53 = ( ( ( ase_vertexNormal * _DisplacementY ) * cos( ( ( _Frequency * temp_output_66_0 ).x + _Time.y ) ) ) + temp_output_66_0 );
			float3 appendResult43 = (float3(Displace45 , 0.0 , 0.0));
			float3 temp_output_47_0 = ( ( mul( ase_vertex3Pos, ObjectToTngent35 ) + appendResult43 ) * 0 );
			float3 XDeviated29 = ( ( ( ase_vertexNormal * _DisplacementX ) * cos( ( ( _Frequency * temp_output_47_0 ).x + _Time.y ) ) ) + temp_output_47_0 );
			float3 normalizeResult76 = normalize( cross( ( YDeviated53 - VertexPosition14 ) , ( XDeviated29 - VertexPosition14 ) ) );
			float3 LocalVertexNormal77 = normalizeResult76;
			v.normal = LocalVertexNormal77;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Emission = _MainColor.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
1920;560;1641;433;5447.432;131.987;5.470062;True;False
Node;AmplifyShaderEditor.CommentaryNode;36;-3441.757,498.7991;Inherit;False;779.3481;495.5507;Convertir el objeto a espcio tangente;5;31;32;33;34;35;;1,1,1,1;0;0
Node;AmplifyShaderEditor.NormalVertexDataNode;33;-3388.757,815.3499;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TangentVertexDataNode;31;-3389.03,548.7991;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BitangentVertexDataNode;32;-3391.757,680.3499;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;44;-4175.804,1232.174;Inherit;False;Property;_Displace;Displace;5;0;Create;True;0;0;False;0;0;0.0201;0;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.MatrixFromVectors;34;-3161.45,658.5673;Inherit;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;45;-3866.45,1234.962;Inherit;False;Displace;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;35;-2918.409,652.882;Inherit;False;ObjectToTngent;-1;True;1;0;FLOAT3x3;0,0,0,0,1,0,0,0,1;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.GetLocalVarNode;56;-3370.758,1826.293;Inherit;False;35;ObjectToTngent;1;0;OBJECT;;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.GetLocalVarNode;46;-3167.368,1278.481;Inherit;False;45;Displace;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;40;-3352.722,1194.859;Inherit;False;35;ObjectToTngent;1;0;OBJECT;;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.PosVertexDataNode;55;-3336.718,1688.768;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;38;-3318.682,1057.334;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;58;-3185.405,1909.914;Inherit;False;45;Displace;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;43;-2969.8,1283.823;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-3114.435,1115.884;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3x3;0,0,0,0,1,0,0,0,1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;63;-2987.836,1915.256;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-3132.472,1747.318;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3x3;0,0,0,1,0,0,1,0,1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;41;-2779.473,1115.883;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;65;-2812.314,1916.724;Inherit;False;35;ObjectToTngent;1;0;OBJECT;;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.GetLocalVarNode;48;-2794.278,1285.291;Inherit;False;35;ObjectToTngent;1;0;OBJECT;;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;64;-2797.509,1747.317;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-2518.612,1117.446;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-2536.649,1748.88;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3x3;0,0,0,0,1,0,0,0,1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;79;-2671.172,1536.706;Inherit;False;Property;_Frequency;Frequency;7;0;Create;True;0;0;False;0;0;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;18;-2008.829,256.8362;Inherit;False;1321.805;625.0001;Vertex Offset;12;8;4;3;1;11;2;12;9;16;15;14;80;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;30;-2011.692,885.215;Inherit;False;1321.805;625.0003;X Desviated;9;19;21;22;23;24;25;26;28;29;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;68;-2008.257,1516.524;Inherit;False;1317.707;613.5016;Y Deviated;9;53;62;61;60;59;54;52;51;50;;1,1,1,1;0;0
Node;AmplifyShaderEditor.PosVertexDataNode;4;-1997.829,571.1362;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;82;-2417.823,1256.859;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;-2381.547,1886.432;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleTimeNode;19;-1946.094,1353.416;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;51;-1955.505,1985.724;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;49;-2243.27,1256.801;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.BreakToComponentsNode;67;-2244.027,1885.954;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-1805.917,606.4555;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;8;-1982.23,731.0361;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-1886.029,450.7361;Inherit;False;Property;_Displacement;Displacement;6;0;Create;True;0;0;False;0;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-1670.03,628.5364;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;50;-1891.403,1567.824;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalVertexDataNode;22;-1881.992,935.5151;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalVertexDataNode;1;-1879.129,307.1362;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-1706.894,1255.916;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1888.892,1079.116;Inherit;False;Property;_DisplacementX;Displacement X;8;0;Create;True;0;0;False;0;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;62;-1716.305,1888.224;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-1898.303,1711.424;Inherit;False;Property;_DisplacementY;Displacement Y;9;0;Create;True;0;0;False;0;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;61;-1556.405,1889.524;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-1600.004,1567.524;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1590.593,935.215;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CosOpNode;12;-1544.13,628.8363;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-1587.729,306.8362;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CosOpNode;25;-1546.994,1257.216;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;16;-1314.975,665.9034;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-1346.53,440.9365;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-1349.394,1069.316;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-1358.805,1701.625;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;28;-1135.728,1094.854;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;59;-1145.139,1727.162;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-1079.713,440.516;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;14;-936.024,434.677;Inherit;False;VertexPosition;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-938.8868,1063.057;Inherit;False;XDeviated;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;53;-948.2972,1695.365;Inherit;False;YDeviated;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;73;-591.3947,1212.472;Inherit;False;14;VertexPosition;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;74;-581.4486,1136.365;Inherit;False;53;YDeviated;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;72;-588.4217,1044.798;Inherit;False;14;VertexPosition;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;71;-582.4756,968.6913;Inherit;False;29;XDeviated;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;69;-324.2,1028.502;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;70;-324.0847,1139.741;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CrossProductOpNode;75;-123.1146,1075.526;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode;76;39.80241,1075.526;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;77;205.0977,1070.769;Inherit;False;LocalVertexNormal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;78;566.546,916.978;Inherit;False;77;LocalVertexNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;17;598.0454,832.9531;Inherit;False;14;VertexPosition;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;13;523.1581,367.7788;Inherit;False;Property;_MainColor;MainColor;10;0;Create;True;0;0;False;0;0.1848522,0.4815417,0.5849056,0;0.1843137,0.5843138,0.4082402,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;850.5419,563.7223;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;NormalReconstruction;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;1;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;;-1;-1;-1;0;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;34;0;31;0
WireConnection;34;1;32;0
WireConnection;34;2;33;0
WireConnection;45;0;44;0
WireConnection;35;0;34;0
WireConnection;43;0;46;0
WireConnection;39;0;38;0
WireConnection;39;1;40;0
WireConnection;63;1;58;0
WireConnection;57;0;55;0
WireConnection;57;1;56;0
WireConnection;41;0;39;0
WireConnection;41;1;43;0
WireConnection;64;0;57;0
WireConnection;64;1;63;0
WireConnection;47;0;41;0
WireConnection;47;1;48;0
WireConnection;66;0;64;0
WireConnection;66;1;65;0
WireConnection;82;0;79;0
WireConnection;82;1;47;0
WireConnection;81;0;79;0
WireConnection;81;1;66;0
WireConnection;49;0;82;0
WireConnection;67;0;81;0
WireConnection;80;0;4;1
WireConnection;80;1;79;0
WireConnection;11;0;80;0
WireConnection;11;1;8;0
WireConnection;23;0;49;0
WireConnection;23;1;19;0
WireConnection;62;0;67;0
WireConnection;62;1;51;0
WireConnection;61;0;62;0
WireConnection;54;0;50;0
WireConnection;54;1;60;0
WireConnection;24;0;22;0
WireConnection;24;1;21;0
WireConnection;12;0;11;0
WireConnection;2;0;1;0
WireConnection;2;1;3;0
WireConnection;25;0;23;0
WireConnection;9;0;2;0
WireConnection;9;1;12;0
WireConnection;26;0;24;0
WireConnection;26;1;25;0
WireConnection;52;0;54;0
WireConnection;52;1;61;0
WireConnection;28;0;26;0
WireConnection;28;1;47;0
WireConnection;59;0;52;0
WireConnection;59;1;66;0
WireConnection;15;0;9;0
WireConnection;15;1;16;0
WireConnection;14;0;15;0
WireConnection;29;0;28;0
WireConnection;53;0;59;0
WireConnection;69;0;71;0
WireConnection;69;1;72;0
WireConnection;70;0;74;0
WireConnection;70;1;73;0
WireConnection;75;0;70;0
WireConnection;75;1;69;0
WireConnection;76;0;75;0
WireConnection;77;0;76;0
WireConnection;0;2;13;0
WireConnection;0;11;17;0
WireConnection;0;12;78;0
ASEEND*/
//CHKSM=A4998A0C28AD001599F9F30F337C797CE543AC87