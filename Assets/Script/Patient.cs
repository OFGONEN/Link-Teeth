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

    // Private Fields \\

    // Components
    private Animator patient_animator;

    private Transform table_transform;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
        patient_animator = GetComponentInChildren< Animator >();
    }

    private void Start()
    {
		table_transform = table_position_reference.SharedValue as Transform;
	}
#endregion

#region API
    public void MovementToTable()
    {
		patient_animator.SetBool( "walking", true );

		transform.DOMove( table_transform.position, GameSettings.Instance.patient_movement_duration )
			.OnUpdate( OnMovement_Update )
			.OnComplete( OnMovement_Complete );
	}
#endregion

#region Implementation
    private void OnMovement_Update()
    {
		transform.LookAtOverTimeAxis( table_transform.position + table_transform.forward, Vector3.up, GameSettings.Instance.patient_look_speed );
	}

    private void OnMovement_Complete()
    {
		patient_animator.SetBool( "walking", false );
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}