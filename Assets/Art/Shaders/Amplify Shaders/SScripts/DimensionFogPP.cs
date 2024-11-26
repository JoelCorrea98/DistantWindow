// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( DimensionFogPPSRenderer ), PostProcessEvent.AfterStack, "DimensionFog", true )]
public sealed class DimensionFogPP : PostProcessEffectSettings
{
	[Tooltip( "Gradient" )]
	public FloatParameter _Gradient = new FloatParameter { value = 2f };
	[Tooltip( "Radius" )]
	public FloatParameter _Radius = new FloatParameter { value = 1f };
	[Tooltip( "Fog Color" )]
	public ColorParameter _FogColor = new ColorParameter { value = new Color(0f,0f,0f,0f) };
}

public sealed class DimensionFogPPSRenderer : PostProcessEffectRenderer<DimensionFogPP>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "Hidden/DimensionFog" ) );
		sheet.properties.SetFloat( "_Gradient", settings._Gradient );
		sheet.properties.SetFloat( "_Radius", settings._Radius );
		sheet.properties.SetColor( "_FogColor", settings._FogColor );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
