/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Patient : MonoBehaviour
{
#region Fields
    [ BoxGroup( "Setup" ), SerializeField ] private SharedReferenceNotifier table_position_reference;

    [ BoxGroup( "Fired Events" ) ] public UnityEvent palate_table_movement_start;
    [ BoxGroup( "Fired Events" ) ] public UnityEvent palate_mouth_movement_start;
    [ BoxGroup( "Fired Events" ) ] public UnityEvent palate_mouth_movement_end;


    // Private Fields \\

    // Components
    private Animator patient_animator;
    private Transform table_transform;
	
	// Delegate
	private UnityMessage onMouthOpen;
	private UnityMessage onMouthClose;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
        patient_animator = GetComponentInChildren< Animator >();

		onMouthOpen  = ExtensionMethods.EmptyMethod;
		onMouthClose = ExtensionMethods.EmptyMethod;
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

		onMouthOpen = OnMouthOpen_PalateMovement_Table;
	}

	public void OnMouthOpen()
	{
		onMouthOpen();
	}

	public void OnMouthClose()
	{
		onMouthClose();
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
		patient_animator.SetBool( "mouth_open", true );
	}

	private void OnMouthOpen_PalateMovement_Table()
	{
		palate_table_movement_start.Invoke();
		onMouthOpen = OnMouthOpen_PalateMovement_Mouth;
	}

	private void OnMouthOpen_PalateMovement_Mouth()
	{
		palate_mouth_movement_start.Invoke();

		onMouthOpen  = ExtensionMethods.EmptyMethod;
		onMouthClose = palate_mouth_movement_end.Invoke;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}