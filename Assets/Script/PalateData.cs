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
	[ Button() ]
	public void SetAllAlpha()
	{
		for( var i = 0; i < palateToothData.Length; i++ )
			palateToothData[ i ].tooth_color = palateToothData[ i ].tooth_color.SetAlpha( 1 );
	}
#endif
}
