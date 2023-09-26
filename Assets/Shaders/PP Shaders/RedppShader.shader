// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hidden/RedppShader"
{
	Properties
	{
		_Color0("Color 0", Color) = (0.4716981,0.08677466,0.2938909,0)
		_Color1("Color 1", Color) = (0.1981132,0.04205234,0.07525681,0)
		_Step("Step", Float) = 0
		_ColorBorde("ColorBorde", Color) = (0,0,0,0)
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
			
			uniform float4 _ColorBorde;
			uniform float _Step;
			uniform float4 _Color0;
			uniform float4 _Color1;


			
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
				float4 TS33 = tex2DNode1;
				float2 uv0_MainTex = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float2 Center5_g1 = uv0_MainTex;
				float4 break32 = ( _MainTex_TexelSize * _Step );
				float temp_output_4_0_g1 = break32.y;
				float PosY8_g1 = temp_output_4_0_g1;
				float temp_output_3_0_g1 = break32.x;
				float NegX10_g1 = -temp_output_3_0_g1;
				float2 appendResult16_g16 = (float2(PosY8_g1 , NegX10_g1));
				float4 tex2DNode3_g16 = tex2D( _MainTex, ( Center5_g1 + float2( 0,0 ) + appendResult16_g16 ) );
				float TopLeft23_g1 = sqrt( ( ( tex2DNode3_g16.r * tex2DNode3_g16.r ) + ( tex2DNode3_g16.g * tex2DNode3_g16.g ) + ( tex2DNode3_g16.b * tex2DNode3_g16.b ) ) );
				float2 appendResult16_g17 = (float2(0.0 , NegX10_g1));
				float4 tex2DNode3_g17 = tex2D( _MainTex, ( Center5_g1 + float2( 0,0 ) + appendResult16_g17 ) );
				float Left43_g1 = sqrt( ( ( tex2DNode3_g17.r * tex2DNode3_g17.r ) + ( tex2DNode3_g17.g * tex2DNode3_g17.g ) + ( tex2DNode3_g17.b * tex2DNode3_g17.b ) ) );
				float NegY11_g1 = -temp_output_4_0_g1;
				float PosX7_g1 = temp_output_3_0_g1;
				float2 appendResult16_g12 = (float2(NegY11_g1 , PosX7_g1));
				float4 tex2DNode3_g12 = tex2D( _MainTex, ( Center5_g1 + float2( 0,0 ) + appendResult16_g12 ) );
				float BottomLeft73_g1 = sqrt( ( ( tex2DNode3_g12.r * tex2DNode3_g12.r ) + ( tex2DNode3_g12.g * tex2DNode3_g12.g ) + ( tex2DNode3_g12.b * tex2DNode3_g12.b ) ) );
				float2 appendResult16_g14 = (float2(PosY8_g1 , PosX7_g1));
				float4 tex2DNode3_g14 = tex2D( _MainTex, ( Center5_g1 + float2( 0,0 ) + appendResult16_g14 ) );
				float TopRight38_g1 = sqrt( ( ( tex2DNode3_g14.r * tex2DNode3_g14.r ) + ( tex2DNode3_g14.g * tex2DNode3_g14.g ) + ( tex2DNode3_g14.b * tex2DNode3_g14.b ) ) );
				float2 appendResult16_g18 = (float2(0.0 , PosX7_g1));
				float4 tex2DNode3_g18 = tex2D( _MainTex, ( Center5_g1 + float2( 0,0 ) + appendResult16_g18 ) );
				float Right53_g1 = sqrt( ( ( tex2DNode3_g18.r * tex2DNode3_g18.r ) + ( tex2DNode3_g18.g * tex2DNode3_g18.g ) + ( tex2DNode3_g18.b * tex2DNode3_g18.b ) ) );
				float2 appendResult16_g10 = (float2(NegY11_g1 , NegX10_g1));
				float4 tex2DNode3_g10 = tex2D( _MainTex, ( Center5_g1 + float2( 0,0 ) + appendResult16_g10 ) );
				float BottomRight58_g1 = sqrt( ( ( tex2DNode3_g10.r * tex2DNode3_g10.r ) + ( tex2DNode3_g10.g * tex2DNode3_g10.g ) + ( tex2DNode3_g10.b * tex2DNode3_g10.b ) ) );
				float temp_output_89_0_g1 = ( ( -TopLeft23_g1 + ( Left43_g1 * -2.0 ) + -BottomLeft73_g1 + TopRight38_g1 + ( Right53_g1 * 2.0 ) + BottomRight58_g1 ) / 6.0 );
				float2 appendResult16_g13 = (float2(PosY8_g1 , 0.0));
				float4 tex2DNode3_g13 = tex2D( _MainTex, ( Center5_g1 + float2( 0,0 ) + appendResult16_g13 ) );
				float Top29_g1 = sqrt( ( ( tex2DNode3_g13.r * tex2DNode3_g13.r ) + ( tex2DNode3_g13.g * tex2DNode3_g13.g ) + ( tex2DNode3_g13.b * tex2DNode3_g13.b ) ) );
				float2 appendResult16_g15 = (float2(NegY11_g1 , 0.0));
				float4 tex2DNode3_g15 = tex2D( _MainTex, ( Center5_g1 + float2( 0,0 ) + appendResult16_g15 ) );
				float Bottom62_g1 = sqrt( ( ( tex2DNode3_g15.r * tex2DNode3_g15.r ) + ( tex2DNode3_g15.g * tex2DNode3_g15.g ) + ( tex2DNode3_g15.b * tex2DNode3_g15.b ) ) );
				float temp_output_103_0_g1 = ( ( -TopLeft23_g1 + ( Top29_g1 * -2.0 ) + -TopRight38_g1 + BottomLeft73_g1 + ( Bottom62_g1 * 2.0 ) + BottomRight58_g1 ) / 6.0 );
				float4 lerpResult36 = lerp( TS33 , _ColorBorde , saturate( sqrt( ( ( temp_output_89_0_g1 * temp_output_89_0_g1 ) + ( temp_output_103_0_g1 * temp_output_103_0_g1 ) ) ) ));
				float grayscale3 = Luminance(TS33.rgb);
				float4 lerpResult23 = lerp( _Color0 , _Color1 , ( saturate( ( 1.0 - step( grayscale3 , 0.3778507 ) ) ) - saturate( ( 1.0 - step( grayscale3 , 0.7107909 ) ) ) ));
				

				float4 color = ( lerpResult36 * ( lerpResult23 * 1.5 ) );
				
				return color;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17800
0;568;1641;433;-633.8184;422.1074;1;True;False
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;2;-1532.774,114.2588;Inherit;False;0;0;_MainTex;Pass;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1327.373,109.0588;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;33;-994.9689,90.69262;Inherit;False;TS;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;34;-446.5362,101.9735;Inherit;False;33;TS;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-171.0219,-13.93391;Inherit;False;Constant;_SecondStep;Second Step;0;0;Create;True;0;0;False;0;0.7107909;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCGrayscale;3;-256.0025,101.4779;Inherit;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-169.722,-96.33435;Inherit;False;Constant;_Float2;Float 2;0;0;Create;True;0;0;False;0;0.3778507;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;14;129.0717,5.57296;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;16;126.4723,-112.7267;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-704.6083,-729.6432;Inherit;False;Property;_Step;Step;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;29;-734.6083,-905.6432;Inherit;False;0;0;_MainTex_TexelSize;Pass;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;20;244.5771,2.496266;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-454.6083,-805.6432;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.OneMinusNode;22;249.5771,-111.5038;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;28;-529.2101,-626.4285;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;19;401.5717,3.073353;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;32;-299.6083,-756.6432;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SaturateNode;18;404.1716,-107.9267;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;24;572.0809,-255.1351;Inherit;False;Property;_Color1;Color 1;1;0;Create;True;0;0;False;0;0.1981132,0.04205234,0.07525681,0;0.1981132,0.04205234,0.07525681,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;11;567.8681,-427.7801;Inherit;False;Property;_Color0;Color 0;0;0;Create;True;0;0;False;0;0.4716981,0.08677466,0.2938909,0;0.4716981,0.08677466,0.2938909,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;21;557.5771,-74.50378;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;40;-7.293015,-634.3289;Inherit;False;Convolution Kernels;-1;;1;8fe0abcb9c5f30546895507fb923050e;0;4;1;FLOAT2;0,0;False;2;SAMPLER2D;0;False;3;FLOAT;0;False;4;FLOAT;0;False;2;FLOAT;110;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;35;56.03113,-904.5209;Inherit;False;33;TS;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;37;9.031128,-811.5209;Inherit;False;Property;_ColorBorde;ColorBorde;3;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;38;218.337,-614.385;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;915.0818,9.436585;Inherit;False;Constant;_Float1;Float 1;0;0;Create;True;0;0;False;0;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;23;894.8525,-168.1192;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;1141.209,-102.6087;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;36;397.3309,-763.1209;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;7;31.73419,762.2864;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;8;294.3019,543.7596;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;4;-397.7564,325.7822;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;1391.803,-257.4821;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;6;-157.6097,776.1916;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;245.574,747.4299;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;68.59718,902.0188;Inherit;False;Constant;_Float0;Float 0;0;0;Create;True;0;0;False;0;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;5;-281.2748,545.7455;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;9;-79.31828,536.5739;Inherit;False;Constant;_Vector0;Vector 0;0;0;Create;True;0;0;False;0;1,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1608.345,-303.336;Float;False;True;-1;2;ASEMaterialInspector;0;9;Hidden/RedppShader;9376b2cb6b56c214d80b9e5867446d6b;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;True;2;False;-1;False;False;True;2;False;-1;True;7;False;-1;False;False;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;1;0;2;0
WireConnection;33;0;1;0
WireConnection;3;0;34;0
WireConnection;14;0;3;0
WireConnection;14;1;15;0
WireConnection;16;0;3;0
WireConnection;16;1;17;0
WireConnection;20;0;14;0
WireConnection;30;0;29;0
WireConnection;30;1;31;0
WireConnection;22;0;16;0
WireConnection;28;2;2;0
WireConnection;19;0;20;0
WireConnection;32;0;30;0
WireConnection;18;0;22;0
WireConnection;21;0;18;0
WireConnection;21;1;19;0
WireConnection;40;1;28;0
WireConnection;40;2;2;0
WireConnection;40;3;32;0
WireConnection;40;4;32;1
WireConnection;38;0;40;0
WireConnection;23;0;11;0
WireConnection;23;1;24;0
WireConnection;23;2;21;0
WireConnection;25;0;23;0
WireConnection;25;1;26;0
WireConnection;36;0;35;0
WireConnection;36;1;37;0
WireConnection;36;2;38;0
WireConnection;7;0;6;0
WireConnection;8;0;3;0
WireConnection;8;1;9;0
WireConnection;8;2;13;0
WireConnection;4;0;1;1
WireConnection;4;1;1;2
WireConnection;39;0;36;0
WireConnection;39;1;25;0
WireConnection;6;0;5;0
WireConnection;6;1;1;3
WireConnection;13;0;7;0
WireConnection;13;1;12;0
WireConnection;5;0;4;0
WireConnection;0;0;39;0
ASEEND*/
//CHKSM=F93D1D1A29CB8C71CFD2DC73BC581190ED617785