/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using UnityEditor;
using Sirenix.OdinInspector;
using System.IO;

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

        private void ConvertManual( GridData base_data, RealGridData target_data )
        {
			var base_width = base_data.gridToothData.GetLength( 0 );
			var base_height = base_data.gridToothData.GetLength( 1 );

			target_data.SetUp( base_width, base_height );

			for( var x = 0; x < base_width; x++ )
			{
				for( var y = 0; y < base_height; y++ )
				{
					target_data.gridToothData[ x + ( y * base_width ) ] = base_data.gridToothData[ x, y ];
				}
			}
		}

		[ Button() ]
		public void ConvertAll()
		{
			string[] grid_guids = AssetDatabase.FindAssets( "t:GridData", new[] { "Assets/Scriptable_Object/Game/Grid" } );

			for( var i = 0; i < grid_guids.Length; i++ )
			{
				var path = AssetDatabase.GUIDToAssetPath( grid_guids[ i ] );
				var grid =  AssetDatabase.LoadAssetAtPath< GridData >( path );

				var realGrid = ScriptableObject.CreateInstance< RealGridData >();

				ConvertManual( grid, realGrid );

				var file_path = Path.GetDirectoryName( path );
				var file_name = Path.GetFileName( path );

				AssetDatabase.CreateAsset( realGrid, "Assets/Scriptable_Object/Game/RealGrid/real_" + file_name );
			}
		}
	}
}