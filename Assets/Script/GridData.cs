/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
// using Sirenix.Utilities.Editor;

[ CreateAssetMenu( fileName = "grid_data_", menuName = "FF/Game/GridData" ) ]
public class GridData : SerializedScriptableObject
{
	[ TitleGroup( "Data"), TableMatrix( DrawElementMethod = "DrawElement" ) ] public GridToothData[,] gridToothData;

	public float GridWidth => gridToothData.GetLength( 0 );
	public float GridHeight => gridToothData.GetLength( 1 );

#if UNITY_EDITOR
	// static GridToothData DrawElement( Rect rect, GridToothData data )
	// {
	// 	data.tooth_color = SirenixEditorFields.ColorField( rect.SubXMax( 100 ), data.tooth_color );
	// 	data.tooth_type = ( ToothType )SirenixEditorFields.EnumDropdown( rect.AddXMin( 100 ), data.tooth_type );

	// 	return data;
	// }


	private void OnValidate()
	{
		var width  = gridToothData.GetLength( 0 );
		var height = gridToothData.GetLength( 1 );

		for( var x = 0; x < width; x++ )
		{
			for( var y = 0; y < height; y++ )
			{
				gridToothData[ x, y ].tooth_color = gridToothData[ x, y ].tooth_color.SetAlpha( 1 );
			}
		}
	}
#endif
}

