/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Line : MonoBehaviour
{
#region Fields
    [ BoxGroup( "Setup" ) ] public LinePool linePool;
    [ BoxGroup( "Setup" ) ] public LineRenderer lineRenderer;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( Vector3 position, Color color )
    {
		gameObject.SetActive( true );

		lineRenderer.startWidth = GameSettings.Instance.line_width;
		lineRenderer.endWidth   = GameSettings.Instance.line_width;

		lineRenderer.positionCount = 2;
		lineRenderer.SetPosition( 0, position );
		lineRenderer.SetPosition( 1, position );

		lineRenderer.startColor = color;
		lineRenderer.endColor   = color;
	}

    public void Spawn( Vector3 firstPosition, Vector3 secondPosition, Color color )
    {
		Spawn( firstPosition, color );
		AppendPoint( secondPosition );
	}

    public void DeSpawn()
    {
		lineRenderer.positionCount = 0;
		linePool.ReturnEntity( this );
	}

    public void AppendPoint( Vector3 position )
    {
		lineRenderer.positionCount += 1;
		UpdatePoint( position );
	}

    public void RemovePoint()
    {
		lineRenderer.positionCount -= 1;
	}
    
    public void UpdatePoint( Vector3 position )
    {
		lineRenderer.SetPosition( lineRenderer.positionCount - 1, position );
    }
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
