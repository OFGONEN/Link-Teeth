/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Grid : MonoBehaviour
{
#region Fields
    [ BoxGroup( "Setup") ] public TransformPool pool_separator;

    private List< Transform > active_separators = new List< Transform >(16);
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		Place_Separators( 0 );
	}
#endregion

#region API
#endregion

#region Implementation
    private void Place_Separators( int index )
    {
		var level_data = CurrentLevelData.Instance.levelData;

		var grid_data   = level_data.grid_data_array[ index ];
		var grid_width  = grid_data.GridWidth;
		var grid_height = grid_data.GridHeight;

		Vector3 position_start = new Vector3(
            grid_data.GridWidth  / -2f * GameSettings.Instance.grid_square_lenght,
            0,
            grid_data.GridHeight / -2f * GameSettings.Instance.grid_square_lenght
        );

		for( var i = 0; i < grid_width + 1; i++ )
        {
			var separator = pool_separator.GetEntity();

			separator.position   = position_start + Vector3.right * i * GameSettings.Instance.grid_square_lenght;
			separator.forward    = Vector3.forward;
			separator.localScale = new Vector3( 1, 1, 1 * grid_height );

			separator.gameObject.SetActive( true );

			active_separators.Add( separator );
		}

		for( var i = 0; i < grid_height + 1; i++ )
        {
			var separator = pool_separator.GetEntity();

			separator.position   = position_start + Vector3.forward * i * GameSettings.Instance.grid_square_lenght;
			separator.forward    = Vector3.right;
			separator.localScale = new Vector3( 1, 1, 1 * grid_width );

			separator.gameObject.SetActive( true );

			active_separators.Add( separator );
		}
	}

	private void ReturnAllSeparators()
	{
		for( var i = 0; i < active_separators.Count; i++ )
			pool_separator.ReturnEntity( active_separators[ i ] );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}