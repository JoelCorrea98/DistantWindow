// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( BlueppShader1PPSRenderer ), PostProcessEvent.AfterStack, "BlueppShader1", true )]
public sealed class BlueppShader1 : PostProcessEffectSettings
{
	[Tooltip( "Color 0" )]
	public ColorParameter _Color2 = new ColorParameter { value = new Color(0.08627451f,0.3166133f,0.4705882f,0f) };
	[Tooltip( "Color 1" )]
	public ColorParameter _Color3 = new ColorParameter { value = new Color(0.04313725f,0.07041758f,0.2f,0f) };
	[Tooltip( "Step" )]
	public FloatParameter _Step2 = new FloatParameter { value = 0f };
	[Tooltip( "ColorBorde" )]
	public ColorParameter _ColorBorde2 = new ColorParameter { value = new Color(0f,0f,0f,0f) };
}

public sealed class BlueppShader1PPSRenderer : PostProcessEffectRenderer<BlueppShader1>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "BlueppShader1" ) );
		sheet.properties.SetColor( "_Color2", settings._Color2 );
		sheet.properties.SetColor( "_Color3", settings._Color3 );
		sheet.properties.SetFloat( "_Step2", settings._Step2 );
		sheet.properties.SetColor( "_ColorBorde2", settings._ColorBorde2 );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
