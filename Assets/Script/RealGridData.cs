/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "real_grid_data_", menuName = "FF/Game/RealGridData" ) ]
public class RealGridData : ScriptableObject
{
	public GridToothData[] gridToothData;

	public int GridWidth => gridWidth;
	public int GridHeight => gridHeight;

	[ SerializeField, ReadOnly ] private int gridWidth;
	[ SerializeField, ReadOnly ] private int gridHeight;

#if UNITY_EDITOR
	public void SetUp( int width, int height )
	{
		gridToothData = new GridToothData[ width * height ];
		gridWidth     = width;
		gridHeight    = height;
	}

	[ Button() ]
	private void LogGridToothDataArray()
	{
		for( var x = 0; x < gridToothData.Length; x++ )
		{
			FFLogger.Log( $"Data({x})- Color:{gridToothData[ x ].tooth_color} - Type:{gridToothData[ x ].tooth_type.ToString()}" );
		}
	}
#endif
}
