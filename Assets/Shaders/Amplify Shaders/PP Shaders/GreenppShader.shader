// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "GreenppShader"
{
	Properties
	{
		_Color3("Color 0", Color) = (0.08627451,0.3166133,0.4705882,0)
		_Color4("Color 1", Color) = (0.04313725,0.07041758,0.2,0)
		_Step3("Step", Float) = 0
		_ColorBorde3("ColorBorde", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		Cull Off
		ZWrite Off
		ZTest Always
		
		Pass
		{
			CGPROGRAM

			

			#pragma vertex Vert
			#pragma fragment Frag
			#pragma target 3.0

			#include "UnityCG.cginc"
			
		
			struct ASEAttributesDefault
			{
				float3 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				
			};

			struct ASEVaryingsDefault
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float2 texcoordStereo : TEXCOORD1;
			#if STEREO_INSTANCING_ENABLED
				uint stereoTargetEyeIndex : SV_RenderTargetArrayIndex;
			#endif
				
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform float4 _ColorBorde3;
			uniform sampler2D _Sampler257;
			uniform float _Step3;
			uniform float4 _Color3;
			uniform float4 _Color4;


			
			float2 TransformTriangleVertexToUV (float2 vertex)
			{
				float2 uv = (vertex + 1.0) * 0.5;
				return uv;
			}

			ASEVaryingsDefault Vert( ASEAttributesDefault v  )
			{
				ASEVaryingsDefault o;
				o.vertex = float4(v.vertex.xy, 0.0, 1.0);
				o.texcoord = TransformTriangleVertexToUV (v.vertex.xy);
#if UNITY_UV_STARTS_AT_TOP
				o.texcoord = o.texcoord * float2(1.0, -1.0) + float2(0.0, 1.0);
#endif
				o.texcoordStereo = TransformStereoScreenSpaceTex (o.texcoord, 1.0);

				v.texcoord = o.texcoordStereo;
				float4 ase_ppsScreenPosVertexNorm = float4(o.texcoordStereo,0,1);

				

				return o;
			}

			float4 Frag (ASEVaryingsDefault i  ) : SV_Target
			{
				float4 ase_ppsScreenPosFragNorm = float4(i.texcoordStereo,0,1);

				float2 uv_MainTex = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode40 = tex2D( _MainTex, uv_MainTex );
				float4 TS41 = tex2DNode40;
				float2 uv056 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 Center5_g19 = uv056;
				float4 break54 = ( _MainTex_TexelSize * _Step3 );
				float temp_output_4_0_g19 = break54.y;
				float PosY8_g19 = temp_output_4_0_g19;
				float temp_output_3_0_g19 = break54.x;
				float NegX10_g19 = -temp_output_3_0_g19;
				float2 appendResult16_g25 = (float2(PosY8_g19 , NegX10_g19));
				float4 tex2DNode3_g25 = tex2D( _Sampler257, ( Center5_g19 + float2( 0,0 ) + appendResult16_g25 ) );
				float TopLeft23_g19 = sqrt( ( ( tex2DNode3_g25.r * tex2DNode3_g25.r ) + ( tex2DNode3_g25.g * tex2DNode3_g25.g ) + ( tex2DNode3_g25.b * tex2DNode3_g25.b ) ) );
				float2 appendResult16_g26 = (float2(0.0 , NegX10_g19));
				float4 tex2DNode3_g26 = tex2D( _Sampler257, ( Center5_g19 + float2( 0,0 ) + appendResult16_g26 ) );
				float Left43_g19 = sqrt( ( ( tex2DNode3_g26.r * tex2DNode3_g26.r ) + ( tex2DNode3_g26.g * tex2DNode3_g26.g ) + ( tex2DNode3_g26.b * tex2DNode3_g26.b ) ) );
				float NegY11_g19 = -temp_output_4_0_g19;
				float PosX7_g19 = temp_output_3_0_g19;
				float2 appendResult16_g21 = (float2(NegY11_g19 , PosX7_g19));
				float4 tex2DNode3_g21 = tex2D( _Sampler257, ( Center5_g19 + float2( 0,0 ) + appendResult16_g21 ) );
				float BottomLeft73_g19 = sqrt( ( ( tex2DNode3_g21.r * tex2DNode3_g21.r ) + ( tex2DNode3_g21.g * tex2DNode3_g21.g ) + ( tex2DNode3_g21.b * tex2DNode3_g21.b ) ) );
				float2 appendResult16_g23 = (float2(PosY8_g19 , PosX7_g19));
				float4 tex2DNode3_g23 = tex2D( _Sampler257, ( Center5_g19 + float2( 0,0 ) + appendResult16_g23 ) );
				float TopRight38_g19 = sqrt( ( ( tex2DNode3_g23.r * tex2DNode3_g23.r ) + ( tex2DNode3_g23.g * tex2DNode3_g23.g ) + ( tex2DNode3_g23.b * tex2DNode3_g23.b ) ) );
				float2 appendResult16_g27 = (float2(0.0 , PosX7_g19));
				float4 tex2DNode3_g27 = tex2D( _Sampler257, ( Center5_g19 + float2( 0,0 ) + appendResult16_g27 ) );
				float Right53_g19 = sqrt( ( ( tex2DNode3_g27.r * tex2DNode3_g27.r ) + ( tex2DNode3_g27.g * tex2DNode3_g27.g ) + ( tex2DNode3_g27.b * tex2DNode3_g27.b ) ) );
				float2 appendResult16_g20 = (float2(NegY11_g19 , NegX10_g19));
				float4 tex2DNode3_g20 = tex2D( _Sampler257, ( Center5_g19 + float2( 0,0 ) + appendResult16_g20 ) );
				float BottomRight58_g19 = sqrt( ( ( tex2DNode3_g20.r * tex2DNode3_g20.r ) + ( tex2DNode3_g20.g * tex2DNode3_g20.g ) + ( tex2DNode3_g20.b * tex2DNode3_g20.b ) ) );
				float temp_output_89_0_g19 = ( ( -TopLeft23_g19 + ( Left43_g19 * -2.0 ) + -BottomLeft73_g19 + TopRight38_g19 + ( Right53_g19 * 2.0 ) + BottomRight58_g19 ) / 6.0 );
				float2 appendResult16_g22 = (float2(PosY8_g19 , 0.0));
				float4 tex2DNode3_g22 = tex2D( _Sampler257, ( Center5_g19 + float2( 0,0 ) + appendResult16_g22 ) );
				float Top29_g19 = sqrt( ( ( tex2DNode3_g22.r * tex2DNode3_g22.r ) + ( tex2DNode3_g22.g * tex2DNode3_g22.g ) + ( tex2DNode3_g22.b * tex2DNode3_g22.b ) ) );
				float2 appendResult16_g24 = (float2(NegY11_g19 , 0.0));
				float4 tex2DNode3_g24 = tex2D( _Sampler257, ( Center5_g19 + float2( 0,0 ) + appendResult16_g24 ) );
				float Bottom62_g19 = sqrt( ( ( tex2DNode3_g24.r * tex2DNode3_g24.r ) + ( tex2DNode3_g24.g * tex2DNode3_g24.g ) + ( tex2DNode3_g24.b * tex2DNode3_g24.b ) ) );
				float temp_output_103_0_g19 = ( ( -TopLeft23_g19 + ( Top29_g19 * -2.0 ) + -TopRight38_g19 + BottomLeft73_g19 + ( Bottom62_g19 * 2.0 ) + BottomRight58_g19 ) / 6.0 );
				float4 lerpResult67 = lerp( TS41 , _ColorBorde3 , saturate( sqrt( ( ( temp_output_89_0_g19 * temp_output_89_0_g19 ) + ( temp_output_103_0_g19 * temp_output_103_0_g19 ) ) ) ));
				float grayscale44 = Luminance(TS41.rgb);
				float4 lerpResult65 = lerp( _Color3 , _Color4 , ( saturate( ( 1.0 - step( grayscale44 , 0.3778507 ) ) ) - saturate( ( 1.0 - step( grayscale44 , 0.7107909 ) ) ) ));
				

				float4 color = ( lerpResult67 * ( lerpResult65 * 1.5 ) );
				
				return color;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17800
0;568;1641;433;3693.866;-251.4254;1;True;False
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;39;-3256.771,363.8828;Inherit;False;0;0;_MainTex;Pass;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;40;-2943.787,359.7685;Inherit;True;Property;_TextureSample3;Texture Sample 0;1;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;41;-2611.383,341.4023;Inherit;False;TS;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;42;-2062.951,352.6832;Inherit;False;41;TS;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-1787.437,236.7757;Inherit;False;Constant;_SecondStep3;Second Step;0;0;Create;True;0;0;False;0;0.7107909;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCGrayscale;44;-1872.419,352.1876;Inherit;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-1786.138,154.3754;Inherit;False;Constant;_Float5;Float 2;0;0;Create;True;0;0;False;0;0.3778507;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;47;-1487.345,256.2827;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;46;-1489.943,137.983;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;48;-2351.023,-654.9332;Inherit;False;0;0;_MainTex_TexelSize;Pass;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;49;-2321.023,-478.9334;Inherit;False;Property;_Step3;Step;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;50;-1371.839,253.2059;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-2071.023,-554.9332;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.OneMinusNode;52;-1366.839,139.2059;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;55;-1212.244,142.783;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;54;-1916.024,-505.9334;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.TextureCoordinatesNode;56;-2145.624,-375.7188;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;53;-1214.844,253.7831;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;60;-1044.334,-4.425434;Inherit;False;Property;_Color4;Color 1;1;0;Create;True;0;0;False;0;0.04313725,0.07041758,0.2,0;0.1981132,0.04205234,0.07525681,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;59;-1048.547,-177.0703;Inherit;False;Property;_Color3;Color 0;0;0;Create;True;0;0;False;0;0.08627451,0.3166133,0.4705882,0;0.4716981,0.08677466,0.2938909,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;57;-1623.709,-383.6192;Inherit;False;Convolution Kernels;-1;;19;8fe0abcb9c5f30546895507fb923050e;0;4;1;FLOAT2;0,0;False;2;SAMPLER2D;_Sampler257;False;3;FLOAT;0;False;4;FLOAT;0;False;2;FLOAT;110;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;58;-1058.838,176.206;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;61;-1560.385,-653.8109;Inherit;False;41;TS;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;62;-1607.385,-560.8109;Inherit;False;Property;_ColorBorde3;ColorBorde;3;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;63;-1398.078,-363.6753;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;64;-701.3332,260.1462;Inherit;False;Constant;_Float4;Float 1;0;0;Create;True;0;0;False;0;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;65;-721.5624,82.59044;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-475.2058,148.101;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;67;-1219.084,-512.4109;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;74;-1584.681,1012.997;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;72;-2014.173,576.4921;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-1370.842,998.1398;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;71;-1897.691,796.4553;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;76;-1774.025,1026.902;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;69;-1322.114,794.4696;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;68;-1547.818,1152.729;Inherit;False;Constant;_Float3;Float 0;0;0;Create;True;0;0;False;0;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-224.6119,-6.772419;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;70;-1695.734,787.2838;Inherit;False;Constant;_Vector3;Vector 0;0;0;Create;True;0;0;False;0;1,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;9;GreenppShader;9376b2cb6b56c214d80b9e5867446d6b;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;True;2;False;-1;False;False;True;2;False;-1;True;7;False;-1;False;False;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;40;0;39;0
WireConnection;41;0;40;0
WireConnection;44;0;42;0
WireConnection;47;0;44;0
WireConnection;47;1;43;0
WireConnection;46;0;44;0
WireConnection;46;1;45;0
WireConnection;50;0;47;0
WireConnection;51;0;48;0
WireConnection;51;1;49;0
WireConnection;52;0;46;0
WireConnection;55;0;52;0
WireConnection;54;0;51;0
WireConnection;53;0;50;0
WireConnection;57;1;56;0
WireConnection;57;3;54;0
WireConnection;57;4;54;1
WireConnection;58;0;55;0
WireConnection;58;1;53;0
WireConnection;63;0;57;0
WireConnection;65;0;59;0
WireConnection;65;1;60;0
WireConnection;65;2;58;0
WireConnection;66;0;65;0
WireConnection;66;1;64;0
WireConnection;67;0;61;0
WireConnection;67;1;62;0
WireConnection;67;2;63;0
WireConnection;74;0;76;0
WireConnection;72;0;40;1
WireConnection;72;1;40;2
WireConnection;73;0;74;0
WireConnection;73;1;68;0
WireConnection;71;0;72;0
WireConnection;76;0;71;0
WireConnection;76;1;40;3
WireConnection;69;0;44;0
WireConnection;69;1;70;0
WireConnection;69;2;73;0
WireConnection;75;0;67;0
WireConnection;75;1;66;0
WireConnection;0;0;75;0
ASEEND*/
//CHKSM=DDFE926B19F48A2559EC4CA37FAE2A4DD1947498