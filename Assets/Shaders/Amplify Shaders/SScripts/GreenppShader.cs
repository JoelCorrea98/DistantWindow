// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( GreenppShaderPPSRenderer ), PostProcessEvent.AfterStack, "GreenppShader", true )]
public sealed class GreenppShader : PostProcessEffectSettings
{
	[Tooltip( "Color 0" )]
	public ColorParameter _Color3 = new ColorParameter { value = new Color(0.08627451f,0.3166133f,0.4705882f,0f) };
	[Tooltip( "Color 1" )]
	public ColorParameter _Color4 = new ColorParameter { value = new Color(0.04313725f,0.07041758f,0.2f,0f) };
	[Tooltip( "Step" )]
	public FloatParameter _Step3 = new FloatParameter { value = 0f };
	[Tooltip( "ColorBorde" )]
	public ColorParameter _ColorBorde3 = new ColorParameter { value = new Color(0f,0f,0f,0f) };
}

public sealed class GreenppShaderPPSRenderer : PostProcessEffectRenderer<GreenppShader>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "GreenppShader" ) );
		sheet.properties.SetColor( "_Color3", settings._Color3 );
		sheet.properties.SetColor( "_Color4", settings._Color4 );
		sheet.properties.SetFloat( "_Step3", settings._Step3 );
		sheet.properties.SetColor( "_ColorBorde3", settings._ColorBorde3 );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
