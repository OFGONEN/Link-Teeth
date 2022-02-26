/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using UnityEditor;
using Sirenix.OdinInspector;

namespace FFEditor
{
[CreateAssetMenu( fileName = "GridDataConverter", menuName = "FFEditor/GridDataConverter" )]
	public class GridDataConverter : ScriptableObject
	{
        [ BoxGroup( "Manual " ) ] public GridData base_gridData;
        [ BoxGroup( "Manual " ) ] public RealGridData target_gridData;

        [ Button() ]
        public void ConvertManual()
        {
			EditorUtility.SetDirty( target_gridData );

			var base_width = base_gridData.gridToothData.GetLength( 0 );
			var base_height = base_gridData.gridToothData.GetLength( 1 );

			target_gridData.SetUp( base_width, base_height );

			for( var x = 0; x < base_width; x++ )
			{
				for( var y = 0; y < base_height; y++ )
				{
					target_gridData.gridToothData[ x + ( y * base_width ) ] = base_gridData.gridToothData[ x, y ];
				}
			}
			AssetDatabase.SaveAssets();
		}
	}
}