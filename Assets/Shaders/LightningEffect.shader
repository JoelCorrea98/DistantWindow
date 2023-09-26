// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LightningEffect"
{
	Properties
	{
		[HDR]_MainColor("Main Color", Color) = (0,0,0,0)
		_Intensidaddelamascara("Intensidad de la mascara", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Off
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _MainColor;
		uniform float _Intensidaddelamascara;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 break19_g3 = float2( 0.69,-0.4 );
			float Mask10 = ( ( 1.0 - v.texcoord.xy.x ) * v.texcoord.xy.x );
			float2 break19_g2 = float2( -5,5 );
			float temp_output_1_0_g2 = _Time.y;
			float sinIn7_g2 = sin( temp_output_1_0_g2 );
			float sinInOffset6_g2 = sin( ( temp_output_1_0_g2 + 1.0 ) );
			float lerpResult20_g2 = lerp( break19_g2.x , break19_g2.y , frac( ( sin( ( ( sinIn7_g2 - sinInOffset6_g2 ) * 91.2228 ) ) * 43758.55 ) ));
			float temp_output_1_0_g3 = ( Mask10 * ( lerpResult20_g2 + sinIn7_g2 ) );
			float sinIn7_g3 = sin( temp_output_1_0_g3 );
			float sinInOffset6_g3 = sin( ( temp_output_1_0_g3 + 1.0 ) );
			float lerpResult20_g3 = lerp( break19_g3.x , break19_g3.y , frac( ( sin( ( ( sinIn7_g3 - sinInOffset6_g3 ) * 91.2228 ) ) * 43758.55 ) ));
			float3 appendResult12 = (float3(0.0 , ( ( lerpResult20_g3 + sinIn7_g3 ) * Mask10 ) , 0.0));
			v.vertex.xyz += appendResult12;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Emission = _MainColor.rgb;
			float Mask10 = ( ( 1.0 - i.uv_texcoord.x ) * i.uv_texcoord.x );
			o.Alpha = saturate( ( Mask10 * _Intensidaddelamascara ) );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows noshadow vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
1920;560;1641;433;3089.931;430.0092;4.357753;False;False
Node;AmplifyShaderEditor.CommentaryNode;17;-2199.178,532.1133;Inherit;False;979.0012;309;Mask;4;6;7;9;10;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-2149.178,586.1136;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;7;-1875.179,582.1136;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-1680.178,588.1136;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;10;-1463.178,584.1136;Inherit;False;Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;27;-609.9227,987.9838;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;26;-429.9229,987.9838;Inherit;False;Noise Sine Wave;-1;;2;a6eff29f739ced848846e3b648af87bd;0;2;1;FLOAT;0;False;2;FLOAT2;-5,5;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;28;-410.9228,908.9838;Inherit;False;10;Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;32;-215.9229,1007.984;Inherit;False;Constant;_Vector0;Vector 0;3;0;Create;True;0;0;False;0;0.69,-0.4;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-204.9229,913.9838;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;30;-41.92294,913.9838;Inherit;False;Noise Sine Wave;-1;;3;a6eff29f739ced848846e3b648af87bd;0;2;1;FLOAT;0;False;2;FLOAT2;-0.5,0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;35;33.59878,301.1675;Inherit;False;10;Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-52.40112,373.1675;Inherit;False;Property;_Intensidaddelamascara;Intensidad de la mascara;4;0;Create;True;0;0;False;0;0;2.11;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;34;-22.92292,1013.984;Inherit;False;10;Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;207.5988,306.1675;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;181.0771,953.9838;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;19;-651.5075,646.6996;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;12;412.538,744.7601;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;38;341.5988,307.1676;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-506.0473,577.1002;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-799.4182,646.6992;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;22;-991.6492,667.8293;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-976.7105,565.1409;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-1175.295,567.7329;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;5;-1512.957,366.9754;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-1351.554,379.0864;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-2203.961,361.9341;Inherit;False;Constant;_Speed;Speed;0;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;13;322.6486,-4.25277;Inherit;False;Property;_MainColor;Main Color;1;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0.1541919,0.5943396,0.5889155,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;15;-1522.554,457.0863;Inherit;False;Property;_Amplitud;Amplitud;2;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;1;-2019.983,367.0886;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;2;-1834.367,367.0886;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;16;-697.9631,573.2109;Inherit;False;10;Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;3;-1674.208,366.9961;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-1179.99,667.9129;Inherit;False;Property;_Frecuencia;Frecuencia;3;0;Create;True;0;0;False;0;0;15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;753.0062,249.0369;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;LightningEffect;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;6;1
WireConnection;9;0;7;0
WireConnection;9;1;6;1
WireConnection;10;0;9;0
WireConnection;26;1;27;0
WireConnection;29;0;28;0
WireConnection;29;1;26;0
WireConnection;30;1;29;0
WireConnection;30;2;32;0
WireConnection;36;0;35;0
WireConnection;36;1;37;0
WireConnection;33;0;30;0
WireConnection;33;1;34;0
WireConnection;19;0;21;0
WireConnection;12;1;33;0
WireConnection;38;0;36;0
WireConnection;23;0;16;0
WireConnection;23;1;19;0
WireConnection;21;0;24;0
WireConnection;21;1;22;0
WireConnection;24;0;20;0
WireConnection;24;1;25;0
WireConnection;20;0;14;0
WireConnection;20;1;10;0
WireConnection;5;0;3;0
WireConnection;14;0;5;0
WireConnection;14;1;15;0
WireConnection;1;0;4;0
WireConnection;2;0;1;0
WireConnection;3;0;2;0
WireConnection;0;2;13;0
WireConnection;0;9;38;0
WireConnection;0;11;12;0
ASEEND*/
//CHKSM=282BD373C4E2354F2A3280EF4AB2037FF112EABE