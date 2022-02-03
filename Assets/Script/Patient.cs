/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Patient : MonoBehaviour
{
#region Fields
    [ BoxGroup( "Setup" ), SerializeField ] private SharedReferenceNotifier table_position_reference;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void MovementToTable()
    {
		var table_transform = table_position_reference.SharedValue as Transform;

		var sequence = DOTween.Sequence();

		sequence.Append( transform.DOMove( table_transform.position, GameSettings.Instance.patient_movement_duration ) );
		sequence.Append( transform.DORotate( table_transform.eulerAngles, GameSettings.Instance.patient_movement_duration ) );

		sequence.OnComplete( OnMovementComplete );
	}
#endregion

#region Implementation
    private void OnMovementComplete()
    {
        FFLogger.Log( "On Movement To Table Complete" );
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}