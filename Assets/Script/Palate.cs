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
	[ BoxGroup( "Setup" ) ] public SharedReferenceNotifier palate_mouth_position_reference;
	[ BoxGroup( "Setup" ) ] public SharedReferenceNotifier palate_table_position_reference;

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
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
