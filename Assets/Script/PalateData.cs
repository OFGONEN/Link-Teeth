/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "palate_data_", menuName = "FF/Game/PalateData" ) ]
public class PalateData : ScriptableObject
{
	public PalateToothData[] palateToothData;

#if UNITY_EDITOR
	private void SetAllAlpha()
	{
		for( var i = 0; i < palateToothData.Length; i++ )
			palateToothData[ i ].tooth_color = palateToothData[ i ].tooth_color.SetAlpha( 1 );
	}

	private void OnValidate()
	{
		for( var i = 0; i < palateToothData.Length; i++ )
		{
			// Set alpha of all colors to 1
			palateToothData[ i ].tooth_color = palateToothData[ i ].tooth_color.SetAlpha( 1 );

			if( palateToothData[ i ].tooth_index < 0 )
			{
				FFLogger.LogError( $"Tooth Index({i}) cannot be lower than 0" );
				palateToothData[ i ].tooth_index = 0;
			}

			if( palateToothData[ i ].tooth_index > 17 )
			{
				FFLogger.LogError( $"Tooth Index({i}) cannot be higher than 17" );
				palateToothData[ i ].tooth_index = 17;
			}
		}
	}
#endif
}
