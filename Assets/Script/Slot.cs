/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Slot : MonoBehaviour
{
#region Fields
    [ BoxGroup( "Shared" ) ] public SelectionManager manager_selection;

    [ BoxGroup( "Setup" ) ] public ColorSetter tooth_selection_plane;
    [ BoxGroup( "Setup" ) ] public Collider tooth_selection_collider;

    // Private \\
    [ ReadOnly ] private GridToothData tooth_data;
    private Tooth tooth_spawned;
    private Vector2 grid_index;
#endregion

#region Properties
    public ToothType ToothType => tooth_data.tooth_type;
    public Color ToothColor => tooth_data.tooth_color;
    public Vector2 GridIndex => grid_index;
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( GridToothData data, int grid_index_x, int grid_index_y )
    {
		gameObject.SetActive( true );
		tooth_data = data;
		grid_index = new Vector2( grid_index_x, grid_index_y );

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
		// tooth_selection_plane.SetColor( tooth_data.tooth_color );
		manager_selection.OnSlot_Select( this );
	}

    public void OnDeSelect()
    {
		// FFLogger.Log( "On DeSelect", gameObject );
		// tooth_selection_plane.SetColor( GameSettings.Instance.grid_default_color );

		manager_selection.OnSlot_DeSelect( this );
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