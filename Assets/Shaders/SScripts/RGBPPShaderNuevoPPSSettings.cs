// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( RGBPPShaderNuevoPPSRenderer ), PostProcessEvent.AfterStack, "RGBPPShaderNuevo", true )]
public sealed class RGBPPShaderNuevoPPSSettings : PostProcessEffectSettings
{
	[Tooltip( "GrayScale Controller" )]
	public FloatParameter _GrayScaleController = new FloatParameter { value = 0f };
}

public sealed class RGBPPShaderNuevoPPSRenderer : PostProcessEffectRenderer<RGBPPShaderNuevoPPSSettings>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "Hidden/RGB PP Shader Nuevo" ) );
		sheet.properties.SetFloat( "_GrayScaleController", settings._GrayScaleController );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
