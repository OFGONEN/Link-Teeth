/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Grid : MonoBehaviour
{
#region Fields
    [ BoxGroup( "Setup" ) ] public EventListenerDelegateResponse puzzle_fill_complete_listener;
    [ BoxGroup( "Setup" ) ] public EventListenerDelegateResponse palate_movement_table_listener;
    [ BoxGroup( "Setup" ) ] public SelectionManager manager_selection;
    [ BoxGroup( "Setup" ) ] public TransformPool pool_separator;
    [ BoxGroup( "Setup" ) ] public SlotPool pool_slot;
    [ BoxGroup( "Setup" ) ] public ToothSet tooth_set;

    private List< Transform > active_separators = new List< Transform >( 16 );
    private List< Slot > active_slots = new List< Slot >( 20 );
#endregion

#region Properties
#endregion

#region Unity API
	private void OnEnable()
	{
		puzzle_fill_complete_listener.OnEnable();
		palate_movement_table_listener.OnEnable();
	}

	private void Awake()
	{
		puzzle_fill_complete_listener.response  = PuzzleFilledCompleteListener;
		palate_movement_table_listener.response = manager_selection.ResetSelectionMethods;
	}

	private void OnDisable()
	{
		puzzle_fill_complete_listener.OnDisable();
		palate_movement_table_listener.OnDisable();
	}

    private void Start()
    {
		tooth_set.ClearSet();
		Place_Puzzle( 0 );
	}
#endregion

#region API
#endregion

#region Implementation
	private void Place_Puzzle( int index )
	{
		CurrentLevelData.Instance.levelData.grid_data_index = index;

		Place_Separators( index );
		Place_Slots( index );
	}

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

			separator.SetParent( transform );

			separator.localPosition = position_start + Vector3.right * i * GameSettings.Instance.grid_square_lenght;
			separator.forward       = transform.forward;
			separator.localScale    = new Vector3( 1, 1, 1 * grid_height );

			separator.gameObject.SetActive( true );

			active_separators.Add( separator );
		}

		for( var i = 0; i < grid_height + 1; i++ )
        {
			var separator = pool_separator.GetEntity();

			separator.SetParent( transform );

			separator.localPosition = position_start + Vector3.forward * i * GameSettings.Instance.grid_square_lenght;
			separator.forward       = transform.right;
			separator.localScale    = new Vector3( 1, 1, 1 * grid_width );

			separator.gameObject.SetActive( true );

			active_separators.Add( separator );
		}
	}

	private void Place_Slots( int index )
	{
		var level_data = CurrentLevelData.Instance.levelData;
		var grid_data  = level_data.grid_data_array[ index ];

		Vector3 position_start = new Vector3(
            grid_data.GridWidth  / -2f * GameSettings.Instance.grid_square_lenght,
            0,
            grid_data.GridHeight / 2f * GameSettings.Instance.grid_square_lenght
        );

		for( var x = 0; x < grid_data.GridWidth; x++ )
		{
			for( var y = 0; y < grid_data.GridHeight; y++ )
			{
				var slot = pool_slot.GetEntity();

				slot.transform.SetParent( transform );
				slot.transform.localEulerAngles = Vector3.zero;

				var grid_length = GameSettings.Instance.grid_square_lenght;
				Vector3 offset = new Vector3(
					x * grid_length + grid_length / 2f,
					0,
					( y * grid_length + grid_length / 2f ) * -1f
				);

				slot.transform.localPosition = position_start + offset;
				slot.Spawn( grid_data.gridToothData[ x + ( y * grid_data.GridWidth) ], x , y );

				active_slots.Add( slot );
			}
		}
	}

	private void ReturnAllSeparators()
	{
		for( var i = 0; i < active_separators.Count; i++ )
			pool_separator.ReturnEntity( active_separators[ i ] );

		active_separators.Clear();
	}

	private void ReturnAllSlots()
	{
		for( var i = 0; i < active_slots.Count; i++ )
			active_slots[ i ].ReturnToPool();

		active_slots.Clear();
	}

	private void PuzzleFilledCompleteListener()
	{
		ReturnAllSeparators();
		ReturnAllSlots();

		var level_data = CurrentLevelData.Instance.levelData;

		if( level_data.grid_data_index + 1 < level_data.grid_data_array.Length )
		{
			FFLogger.Log( "New Puzzle" );
			Place_Puzzle( level_data.grid_data_index + 1 );
			manager_selection.ResetSelectionMethods();
		}
		else
			FFLogger.Log( "No puzzle left!Q" );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}