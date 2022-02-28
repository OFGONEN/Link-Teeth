/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Tooth : MonoBehaviour
{
#region Fields
    [ BoxGroup( "Fired Events" ) ] public GameEvent tooth_jump_complete;
    [ BoxGroup( "Fired Events" ) ] public GameEvent tooth_spawn_complete;

    [ BoxGroup( "Shared Variables" ) ] public ToothPool tooth_pool;
    [ BoxGroup( "Shared Variables" ) ] public ToothSet tooth_set;
    [ BoxGroup( "Shared Variables" ) ] public PalateToothSet palateTooth_set;
    [ BoxGroup( "Shared Variables" ) ] public SelectionManager manager_selection;

    [ BoxGroup( "Setup" ) ] public ToothType tooth_type;
    [ BoxGroup( "Setup" ) ] public ColorSetter tooth_color_setter;

	[ ReadOnly, ShowInInspector ] private int tooth_index;
	[ ReadOnly, ShowInInspector ] private PalateTooth target_palateTooth;
	[ ReadOnly, ShowInInspector ] private GridToothData tooth_data;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( GridToothData data, Transform target )
    {
		gameObject.SetActive( true );
		transform.rotation = target.rotation;
		transform.position = target.position + Vector3.up * GameSettings.Instance.tooth_spawn_length;

		tooth_color_setter.SetColor( data.tooth_color );

		tooth_data  = data;
		tooth_index = tooth_set.itemList.Count;
		tooth_set.AddList( this );

		transform.DOMove( target.position, GameSettings.Instance.tooth_spawn_duration )
			.SetEase( GameSettings.Instance.tooth_spawn_ease )
			.SetDelay( GameSettings.Instance.tooth_spawn_delay * tooth_index )
			.OnComplete( OnSpawnComplete );
	}

    public void OnPuzzleSolved() // Called from Unity Event
    {
		//Info PuzzleSolvedTween
		transform.DOMove( transform.position + Vector3.up * GameSettings.Instance.tooth_levitate_amount, 
			GameSettings.Instance.tooth_levitate_duration )
			.SetDelay( GameSettings.Instance.tooth_levitate_delay )
			.SetEase( GameSettings.Instance.tooth_levitate_ease )
			.OnComplete( OnPuzzleSolvedComplete );
	}
#endregion

#region Implementation
	private void OnPuzzleSolvedComplete()
	{
		var color = tooth_data.tooth_color;

		foreach( var palateTooth in palateTooth_set.itemList )
		{
			if( tooth_data.tooth_type == palateTooth.ToothType && palateTooth.IsEmpty && palateTooth.Color.CompareColor( color ) )
			{
				target_palateTooth = palateTooth;
				palateTooth.Fill();
				//Info JumpTween
				transform.DOJump( palateTooth.transform.position, GameSettings.Instance.tooth_jump_power, 1, GameSettings.Instance.tooth_jump_duration )
				.SetEase( GameSettings.Instance.tooth_jump_ease )
				.SetDelay( GameSettings.Instance.tooth_jump_delay * tooth_index )
				.OnComplete( OnJumpTweenComplete );
				break;
			}
		}
	}

	private void OnJumpTweenComplete()
	{
		tooth_set.RemoveList( this );
		tooth_pool.ReturnEntity( this );
		target_palateTooth.Fill_Visual();

		if( tooth_set.itemList.Count == 0 )
			tooth_jump_complete.Raise();
	}

	private void OnSpawnComplete()
	{
		if( tooth_index == tooth_set.itemList.Count - 1 )
			tooth_spawn_complete.Raise();
			// manager_selection.ResetSelectionMethods();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
