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
    [ BoxGroup( "Setup" ) ] public Shapes.Line line;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( Vector3 position, Color color )
    {
		gameObject.SetActive( true );

		transform.position = position.AddY( GameSettings.Instance.line_height );
		line.Start         = Vector3.zero;
		line.End           = Vector3.zero;

		line.Color = color;
	}

    public void Spawn( Vector3 firstPosition, Vector3 secondPosition, Color color )
    {
		Spawn( firstPosition, color );
		UpdatePoint( secondPosition );
	}

    public void DeSpawn()
    {
		linePool.ReturnEntity( this );
	}

    public void UpdatePoint( Vector3 position )
    {
		line.End = transform.InverseTransformPoint( position );
    }
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
