// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BlueppShader1"
{
	Properties
	{
		_Color2("Color 0", Color) = (0.08627451,0.3166133,0.4705882,0)
		_Color3("Color 1", Color) = (0.04313725,0.07041758,0.2,0)
		_Step2("Step", Float) = 0
		_ColorBorde2("ColorBorde", Color) = (0,0,0,0)
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
			
			uniform float4 _ColorBorde2;
			uniform sampler2D _Sampler218;
			uniform float _Step2;
			uniform float4 _Color2;
			uniform float4 _Color3;


			
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
				float4 tex2DNode1 = tex2D( _MainTex, uv_MainTex );
				float4 TS2 = tex2DNode1;
				float2 uv014 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 Center5_g1 = uv014;
				float4 break15 = ( _MainTex_TexelSize * _Step2 );
				float temp_output_4_0_g1 = break15.y;
				float PosY8_g1 = temp_output_4_0_g1;
				float temp_output_3_0_g1 = break15.x;
				float NegX10_g1 = -temp_output_3_0_g1;
				float2 appendResult16_g16 = (float2(PosY8_g1 , NegX10_g1));
				float4 tex2DNode3_g16 = tex2D( _Sampler218, ( Center5_g1 + float2( 0,0 ) + appendResult16_g16 ) );
				float TopLeft23_g1 = sqrt( ( ( tex2DNode3_g16.r * tex2DNode3_g16.r ) + ( tex2DNode3_g16.g * tex2DNode3_g16.g ) + ( tex2DNode3_g16.b * tex2DNode3_g16.b ) ) );
				float2 appendResult16_g17 = (float2(0.0 , NegX10_g1));
				float4 tex2DNode3_g17 = tex2D( _Sampler218, ( Center5_g1 + float2( 0,0 ) + appendResult16_g17 ) );
				float Left43_g1 = sqrt( ( ( tex2DNode3_g17.r * tex2DNode3_g17.r ) + ( tex2DNode3_g17.g * tex2DNode3_g17.g ) + ( tex2DNode3_g17.b * tex2DNode3_g17.b ) ) );
				float NegY11_g1 = -temp_output_4_0_g1;
				float PosX7_g1 = temp_output_3_0_g1;
				float2 appendResult16_g12 = (float2(NegY11_g1 , PosX7_g1));
				float4 tex2DNode3_g12 = tex2D( _Sampler218, ( Center5_g1 + float2( 0,0 ) + appendResult16_g12 ) );
				float BottomLeft73_g1 = sqrt( ( ( tex2DNode3_g12.r * tex2DNode3_g12.r ) + ( tex2DNode3_g12.g * tex2DNode3_g12.g ) + ( tex2DNode3_g12.b * tex2DNode3_g12.b ) ) );
				float2 appendResult16_g14 = (float2(PosY8_g1 , PosX7_g1));
				float4 tex2DNode3_g14 = tex2D( _Sampler218, ( Center5_g1 + float2( 0,0 ) + appendResult16_g14 ) );
				float TopRight38_g1 = sqrt( ( ( tex2DNode3_g14.r * tex2DNode3_g14.r ) + ( tex2DNode3_g14.g * tex2DNode3_g14.g ) + ( tex2DNode3_g14.b * tex2DNode3_g14.b ) ) );
				float2 appendResult16_g18 = (float2(0.0 , PosX7_g1));
				float4 tex2DNode3_g18 = tex2D( _Sampler218, ( Center5_g1 + float2( 0,0 ) + appendResult16_g18 ) );
				float Right53_g1 = sqrt( ( ( tex2DNode3_g18.r * tex2DNode3_g18.r ) + ( tex2DNode3_g18.g * tex2DNode3_g18.g ) + ( tex2DNode3_g18.b * tex2DNode3_g18.b ) ) );
				float2 appendResult16_g10 = (float2(NegY11_g1 , NegX10_g1));
				float4 tex2DNode3_g10 = tex2D( _Sampler218, ( Center5_g1 + float2( 0,0 ) + appendResult16_g10 ) );
				float BottomRight58_g1 = sqrt( ( ( tex2DNode3_g10.r * tex2DNode3_g10.r ) + ( tex2DNode3_g10.g * tex2DNode3_g10.g ) + ( tex2DNode3_g10.b * tex2DNode3_g10.b ) ) );
				float temp_output_89_0_g1 = ( ( -TopLeft23_g1 + ( Left43_g1 * -2.0 ) + -BottomLeft73_g1 + TopRight38_g1 + ( Right53_g1 * 2.0 ) + BottomRight58_g1 ) / 6.0 );
				float2 appendResult16_g13 = (float2(PosY8_g1 , 0.0));
				float4 tex2DNode3_g13 = tex2D( _Sampler218, ( Center5_g1 + float2( 0,0 ) + appendResult16_g13 ) );
				float Top29_g1 = sqrt( ( ( tex2DNode3_g13.r * tex2DNode3_g13.r ) + ( tex2DNode3_g13.g * tex2DNode3_g13.g ) + ( tex2DNode3_g13.b * tex2DNode3_g13.b ) ) );
				float2 appendResult16_g15 = (float2(NegY11_g1 , 0.0));
				float4 tex2DNode3_g15 = tex2D( _Sampler218, ( Center5_g1 + float2( 0,0 ) + appendResult16_g15 ) );
				float Bottom62_g1 = sqrt( ( ( tex2DNode3_g15.r * tex2DNode3_g15.r ) + ( tex2DNode3_g15.g * tex2DNode3_g15.g ) + ( tex2DNode3_g15.b * tex2DNode3_g15.b ) ) );
				float temp_output_103_0_g1 = ( ( -TopLeft23_g1 + ( Top29_g1 * -2.0 ) + -TopRight38_g1 + BottomLeft73_g1 + ( Bottom62_g1 * 2.0 ) + BottomRight58_g1 ) / 6.0 );
				float4 lerpResult28 = lerp( TS2 , _ColorBorde2 , saturate( sqrt( ( ( temp_output_89_0_g1 * temp_output_89_0_g1 ) + ( temp_output_103_0_g1 * temp_output_103_0_g1 ) ) ) ));
				float grayscale5 = Luminance(TS2.rgb);
				float4 lerpResult26 = lerp( _Color2 , _Color3 , ( saturate( ( 1.0 - step( grayscale5 , 0.3778507 ) ) ) - saturate( ( 1.0 - step( grayscale5 , 0.7107909 ) ) ) ));
				

				float4 color = ( lerpResult28 * ( lerpResult26 * 1.5 ) );
				
				return color;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17800
0;568;1641;433;7860.718;1457.817;7.676038;True;False
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;39;-3304.274,383.722;Inherit;False;0;0;_MainTex;Pass;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-2991.29,379.6077;Inherit;True;Property;_TextureSample2;Texture Sample 0;1;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;2;-2658.886,361.2415;Inherit;False;TS;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;3;-2110.454,372.5224;Inherit;False;2;TS;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-1834.939,256.615;Inherit;False;Constant;_SecondStep2;Second Step;0;0;Create;True;0;0;False;0;0.7107909;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCGrayscale;5;-1919.921,372.0268;Inherit;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1833.64,174.2146;Inherit;False;Constant;_Float4;Float 2;0;0;Create;True;0;0;False;0;0.3778507;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;7;-1534.847,276.1219;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;9;-1537.445,157.8222;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;8;-2398.526,-635.0939;Inherit;False;0;0;_MainTex_TexelSize;Pass;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-2368.526,-459.0942;Inherit;False;Property;_Step2;Step;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;11;-1419.341,273.0451;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-2118.526,-535.0939;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.OneMinusNode;13;-1414.341,159.0451;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;17;-1259.746,162.6222;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;15;-1963.526,-486.0942;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-2193.127,-355.8796;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;16;-1262.346,273.6223;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;20;-1091.836,15.41381;Inherit;False;Property;_Color3;Color 1;1;0;Create;True;0;0;False;0;0.04313725,0.07041758,0.2,0;0.1981132,0.04205234,0.07525681,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;21;-1096.049,-157.2311;Inherit;False;Property;_Color2;Color 0;0;0;Create;True;0;0;False;0;0.08627451,0.3166133,0.4705882,0;0.4716981,0.08677466,0.2938909,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;18;-1671.211,-363.78;Inherit;False;Convolution Kernels;-1;;1;8fe0abcb9c5f30546895507fb923050e;0;4;1;FLOAT2;0,0;False;2;SAMPLER2D;_Sampler218;False;3;FLOAT;0;False;4;FLOAT;0;False;2;FLOAT;110;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;19;-1106.34,196.0452;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;22;-1607.887,-633.9716;Inherit;False;2;TS;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;23;-1654.887,-540.9716;Inherit;False;Property;_ColorBorde2;ColorBorde;3;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;24;-1445.58,-343.8361;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-748.8359,279.9854;Inherit;False;Constant;_Float3;Float 1;0;0;Create;True;0;0;False;0;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;26;-769.0651,102.4297;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-522.7087,167.9402;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;28;-1266.586,-492.5717;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;36;-1632.183,1032.836;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;34;-2061.675,596.3314;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-1418.344,1017.979;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;30;-1945.193,816.2946;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;31;-1821.527,1046.741;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;37;-1369.616,814.3089;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-1595.32,1172.568;Inherit;False;Constant;_Float2;Float 0;0;0;Create;True;0;0;False;0;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-272.1146,13.06682;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;29;-1743.236,807.1231;Inherit;False;Constant;_Vector2;Vector 0;0;0;Create;True;0;0;False;0;1,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;9;BlueppShader1;9376b2cb6b56c214d80b9e5867446d6b;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;True;2;False;-1;False;False;True;2;False;-1;True;7;False;-1;False;False;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;1;0;39;0
WireConnection;2;0;1;0
WireConnection;5;0;3;0
WireConnection;7;0;5;0
WireConnection;7;1;4;0
WireConnection;9;0;5;0
WireConnection;9;1;6;0
WireConnection;11;0;7;0
WireConnection;12;0;8;0
WireConnection;12;1;10;0
WireConnection;13;0;9;0
WireConnection;17;0;13;0
WireConnection;15;0;12;0
WireConnection;16;0;11;0
WireConnection;18;1;14;0
WireConnection;18;3;15;0
WireConnection;18;4;15;1
WireConnection;19;0;17;0
WireConnection;19;1;16;0
WireConnection;24;0;18;0
WireConnection;26;0;21;0
WireConnection;26;1;20;0
WireConnection;26;2;19;0
WireConnection;27;0;26;0
WireConnection;27;1;25;0
WireConnection;28;0;22;0
WireConnection;28;1;23;0
WireConnection;28;2;24;0
WireConnection;36;0;31;0
WireConnection;34;0;1;1
WireConnection;34;1;1;2
WireConnection;33;0;36;0
WireConnection;33;1;32;0
WireConnection;30;0;34;0
WireConnection;31;0;30;0
WireConnection;31;1;1;3
WireConnection;37;0;5;0
WireConnection;37;1;29;0
WireConnection;37;2;33;0
WireConnection;35;0;28;0
WireConnection;35;1;27;0
WireConnection;0;0;35;0
ASEEND*/
//CHKSM=DFA492A5E345B79C33A53BB5C84B60A1197C6147