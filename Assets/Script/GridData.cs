/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "grid_data_", menuName = "FF/Game/GridData" ) ]
public class GridData : ScriptableObject
{
	public int size_row;
	public int size_column;
	public GridToothData[] gridToothData;

#if UNITY_EDITOR
	[ Button() ]
	public void CreateGridArray()
	{
		if( size_row >= 1 && size_column >= 1)
			gridToothData = new GridToothData[ size_row * size_column ];
		else
			FFLogger.LogError( "Both Row and Column sizes needs to be bigger than 1." );
	}

	[ Button() ]
	public void SetData( int row, int column, GridToothData data )
	{
		if( data.tooth_type == ToothType.None )
			FFLogger.LogError( "Cannot Select NONE as Tooth Type!!" );
		else if( row >= 0 && row <= size_row && column >= 0 && column <= size_column )
		{
			var index    		   = row * size_row + column;
			data.tooth_color 	   = data.tooth_color.SetAlpha( 1 );
			gridToothData[ index ] = data;
		}
		else
			FFLogger.LogError( "Select correct index for the Grid" );
	}
#endif
}

