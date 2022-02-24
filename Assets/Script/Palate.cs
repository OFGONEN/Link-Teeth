/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Palate : MonoBehaviour
{
#region Fields
	[ BoxGroup( "SharedData" ) ] public PalateData palate_data_current;
	[ BoxGroup( "SharedData" ) ] public PalateToothSet palate_tooth_set;
	[ BoxGroup( "SharedData" ) ] public SharedReferenceNotifier palate_mouth_position_reference;
	[ BoxGroup( "SharedData" ) ] public SharedReferenceNotifier palate_table_position_reference;

	[ BoxGroup( "Setup" ) ] public Transform palate_parent_gfx;

	[ BoxGroup( "Fired Events" ) ] public UnityEvent palate_movement_table_end;
	[ BoxGroup( "Fired Events" ) ] public UnityEvent palate_movement_mouth_end;
#endregion

#region Properties
#endregion

#region Unity API
    private void Start()
    {
		transform.SetParent( palate_mouth_position_reference.SharedValue as Transform );

		transform.localPosition = Vector3.zero;
		transform.localEulerAngles = Vector3.zero;

		ModifyTeeth();
	}
#endregion

#region API
	public void MovementToTable()
    {
		var target_transform = palate_table_position_reference.SharedValue as Transform;

		transform.SetParent( null );

		var sequence = DOTween.Sequence();

		sequence.Append( transform.DOMove( target_transform.position, GameSettings.Instance.palate_table_movement_duration ) );
		sequence.Join( transform.DORotate( target_transform.eulerAngles, GameSettings.Instance.palate_table_movement_duration ) );
		sequence.AppendCallback( palate_movement_table_end.Invoke );
	}

	public void MovementToMouth()
	{
		transform.SetParent( palate_mouth_position_reference.SharedValue as Transform );

		var sequence = DOTween.Sequence();

		sequence.Append( transform.DOLocalMove( Vector3.zero, GameSettings.Instance.palate_mouth_movement_duration ) );
		sequence.Join( transform.DOLocalRotate( Vector3.zero, GameSettings.Instance.palate_mouth_movement_duration ) );
		sequence.AppendCallback( palate_movement_mouth_end.Invoke );
	}
#endregion

#region Implementation
	private void ModifyTeeth()
	{
		var tooth_data_array = palate_data_current.palateToothData;

		for( var i = 0; i < tooth_data_array.Length; i++ )
		{
			var data  = tooth_data_array[ i ];
			var tooth = palate_parent_gfx.GetChild( data.tooth_index );

			var palate_tooth = tooth.gameObject.AddComponent< PalateTooth >();

			palate_tooth.Spawn( palate_tooth_set, data );
		}
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
