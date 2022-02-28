/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using UnityEditor;
using Sirenix.OdinInspector;

public class CameraTween : MonoBehaviour
{
#region Fields
    [ SerializeField ] private SharedReferenceNotifier camera_reference;
    [ SerializeField ] private CameraTweenData[] cameraTweenData_array;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void PlayCameraTween( int index )
    {
		var camera = camera_reference.SharedValue as Transform;

		var data = cameraTweenData_array[ index ];

		var sequence = DOTween.Sequence();

		if( data.tween_complete_event )
			sequence.OnComplete( data.tween_complete_event.Raise );
        
        if( data.does_tween_position )
			sequence.Join( camera.DOMove( data.target.position, data.duration ).SetEase( data.ease_position ) );

        if( data.does_tween_rotation )
			sequence.Join( camera.DORotate( data.target.eulerAngles, data.duration ).SetEase( data.ease_rotation ) );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
	private Vector3 default_position;
	private Vector3 default_rotation;

	[ Button() ]	
	private void ReturnDefaultPosition()
	{
		var camera = Camera.main;
		camera.transform.position    = default_position;
		camera.transform.eulerAngles = default_rotation;
	}

	[ Button() ]
	private void SetCameraPosition( int index )
	{
		var camera = Camera.main;

		default_position = camera.transform.position;
		default_rotation = camera.transform.eulerAngles;

		var data = cameraTweenData_array[ index ];

		camera.transform.position = data.target.position;
		camera.transform.rotation = data.target.rotation;
	}
	private void OnDrawGizmos()
	{
		for( var i = 0; i < cameraTweenData_array.Length; i++ )
		{
			var data = cameraTweenData_array[ i ];

			if( data.target == null ) continue;

			Handles.ArrowHandleCap( -1, data.target.position, data.target.rotation, 0.5f, EventType.Repaint );
			Handles.Label( data.target.position, data.target.name );
		}
	}
#endif
#endregion
}