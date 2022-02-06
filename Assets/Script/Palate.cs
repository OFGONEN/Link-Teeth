/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Palate : MonoBehaviour
{
#region Fields
	[ BoxGroup( "Setup" ) ] public SharedReferenceNotifier palate_mouth_position_reference;
	[ BoxGroup( "Setup" ) ] public SharedReferenceNotifier palate_table_position_reference;
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
		sequence.Append( transform.DORotate( target_transform.eulerAngles, GameSettings.Instance.palate_table_movement_duration ) );
		sequence.AppendCallback( ExtensionMethods.EmptyMethod ); //todo fire a event
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
