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
    [ BoxGroup( "Shared" ) ] public LinePool pool_line;

    [ BoxGroup( "Setup" ) ] public ColorSetter tooth_selection_plane;
    [ BoxGroup( "Setup" ) ] public Collider tooth_selection_collider;

    // Private \\
    [ ReadOnly ] private GridToothData tooth_data;
    private Tooth tooth_spawned;
    private Vector2 grid_index;

    private bool slot_occupied;
	private Color slot_color;
	private Line slot_line;
	private Slot slot_connected;
#endregion

#region Properties
    public ToothType ToothType => tooth_data.tooth_type;
    public Color ToothColor    => tooth_data.tooth_color;
	public Color SlotColor     => slot_color;
	public Vector2 GridIndex   => grid_index;
	public bool SlotOccupied   => slot_occupied;
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

		slot_line = null;
		slot_connected = null;
		tooth_selection_plane.SetColor( GameSettings.Instance.grid_default_color );
	}

    public void OnSelect()
    {
		manager_selection.OnSlot_Select( this );
	}

    public void OnDeSelect()
    {
		manager_selection.OnSlot_DeSelect( this );
	}

	public void PairSlot( Slot slot )
	{
		tooth_selection_plane.SetColor( slot_color );
		slot_occupied = true;

		slot_connected = slot;
		slot.ConnectColor( slot_color );

		slot_line = pool_line.GetEntity();
		slot_line.Spawn( 
			transform.position.AddY( GameSettings.Instance.grid_line_height ), 
			slot.transform.position.AddY( GameSettings.Instance.grid_line_height ),
			slot_color
		 );
	}

	public void ConnectColor( Color color )
	{
		tooth_selection_plane.SetColor( color.SetAlpha( GameSettings.Instance.grid_plane_alpha ) );

		slot_color    = color;
		slot_occupied = true;
	}

	public void ClearFrontConnections()
	{
		if( slot_line )
		{
			pool_line.ReturnEntity( slot_line );
			slot_line = null;
		}

		slot_connected?.Clear();
		slot_connected = null;
	}

	public void Clear()
	{
		tooth_selection_plane.SetColor( GameSettings.Instance.grid_default_color );

		slot_occupied = false || ToothType != ToothType.None;

		if( slot_line )
		{
			pool_line.ReturnEntity( slot_line );
			slot_line = null;
		}

		slot_connected?.Clear();
		slot_connected = null;
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
		slot_occupied = true;
		slot_color    = tooth_data.tooth_color;
	}

    private void SpawnNull()
    {
		slot_occupied  = false;
		slot_color     = GameSettings.Instance.grid_default_color;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}