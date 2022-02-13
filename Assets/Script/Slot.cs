/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Slot : MonoBehaviour
{
#region Fields
    [ BoxGroup( "Setup" ) ] public ColorSetter tooth_selection_plane;
    [ BoxGroup( "Setup" ) ] public Collider tooth_selection_collider;

    // Private \\
    [ ReadOnly ] private GridToothData tooth_data;
    private Tooth tooth_spawned;
#endregion

#region Properties
    public ToothType ToothType => tooth_data.tooth_type;
    public Color ToothColor => tooth_data.tooth_color;
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( GridToothData data )
    {
		gameObject.SetActive( true );
		tooth_data = data;

		if( tooth_data.tooth_type == ToothType.Canine )
			SpawnTooth( GameSettings.Instance.tooth_pool_canine );
        else if( tooth_data.tooth_type == ToothType.Molar )
			SpawnTooth( GameSettings.Instance.tooth_pool_molar );
        else if( tooth_data.tooth_type == ToothType.Canine )
			SpawnTooth( GameSettings.Instance.tooth_pool_canine );
        else // Tooth type is null
			SpawnNull();

		tooth_selection_plane.SetColor( GameSettings.Instance.grid_default_color );
	}

    public void OnSelect()
    {
		FFLogger.Log( "On Select", gameObject );
		tooth_selection_plane.SetColor( tooth_data.tooth_color );

    }

    public void OnDeSelect()
    {
		FFLogger.Log( "On DeSelect", gameObject );
		tooth_selection_plane.SetColor( GameSettings.Instance.grid_default_color );
    }
#endregion

#region Implementation
    private void SpawnTooth( ToothPool pool )
    {
		var tooth = pool.GetEntity();

		tooth.transform.position = transform.position;
		tooth.transform.rotation = transform.rotation;

		tooth.Spawn( tooth_data.tooth_color );

		tooth_spawned = tooth;

		tooth_selection_plane.SetColor( GameSettings.Instance.grid_default_color );
	}

    private void SpawnNull()
    {
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}