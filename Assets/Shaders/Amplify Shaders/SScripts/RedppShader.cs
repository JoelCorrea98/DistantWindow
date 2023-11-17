// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( RedppShaderPPSRenderer ), PostProcessEvent.AfterStack, "RedppShader", true )]
public sealed class RedppShader : PostProcessEffectSettings
{
	[Tooltip( "Color 0" )]
	public ColorParameter _Color0 = new ColorParameter { value = new Color(0.4716981f,0.08677466f,0.2938909f,0f) };
	[Tooltip( "Color 1" )]
	public ColorParameter _Color1 = new ColorParameter { value = new Color(0.1981132f,0.04205234f,0.07525681f,0f) };
	[Tooltip( "Step" )]
	public FloatParameter _Step = new FloatParameter { value = 0f };
	[Tooltip( "ColorBorde" )]
	public ColorParameter _ColorBorde = new ColorParameter { value = new Color(0f,0f,0f,0f) };
}

public sealed class RedppShaderPPSRenderer : PostProcessEffectRenderer<RedppShader>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "Hidden/RedppShader" ) );
		sheet.properties.SetColor( "_Color0", settings._Color0 );
		sheet.properties.SetColor( "_Color1", settings._Color1 );
		sheet.properties.SetFloat( "_Step", settings._Step );
		sheet.properties.SetColor( "_ColorBorde", settings._ColorBorde );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
