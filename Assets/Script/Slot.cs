/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FFStudio;
using Sirenix.OdinInspector;

public class Slot : MonoBehaviour
{
#region Fields
    [ BoxGroup( "Shared" ) ] public SelectionManager manager_selection;
    [ BoxGroup( "Shared" ) ] public LinePool pool_line;

    [ BoxGroup( "Setup" ) ] public ColorSetter tooth_selection_plane;
    [ BoxGroup( "Setup" ) ] public BoxCollider tooth_selection_collider;

    // Private \\
    [ ReadOnly ] private GridToothData tooth_data;
    private Tooth tooth_spawned;
    private Vector2 grid_index;

    private bool slot_occupied;
	private Color slot_color;
	private ToothType slot_connected_tooth;
	private Line slot_line;
	[ ShowInInspector, ReadOnly ] private Slot slot_paired;
	[ ShowInInspector, ReadOnly ] private Slot slot_connected;
#endregion

#region Properties
	public ToothType ConnectedToothType => slot_connected_tooth;
    public ToothType ToothType => tooth_data.tooth_type;
	public Vector2 GridIndex   => grid_index;
    public Color ToothColor    => tooth_data.tooth_color;
	public Color SlotColor     => slot_color;
	public bool SlotOccupied   => slot_occupied;
	public Slot SlotPaired     => slot_paired;
	public Slot SlotConnected  => slot_connected;
#endregion

#region Unity API
	private void Awake()
	{
		var size   = tooth_selection_collider.size;
		    size.x = size.z = GameSettings.Instance.grid_square_lenght;
	}
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
        else if( tooth_data.tooth_type == ToothType.PreMolar )
			SpawnTooth( GameSettings.Instance.tooth_pool_premolar );
        else // Tooth type is null
			SpawnNull();

		slot_connected_tooth = tooth_data.tooth_type;
		slot_line      = null;
		slot_connected = null;
		slot_paired    = null;
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
		slot.ConnectSlot( this );

		slot_line = pool_line.GetEntity();
		slot_line.Spawn( 
			transform.position.AddY( GameSettings.Instance.grid_line_height ), 
			slot.transform.position.AddY( GameSettings.Instance.grid_line_height ),
			slot_color
		 );
	}

	public void ConnectSlot( Slot slot )
	{
		var color = slot.SlotColor;

		tooth_selection_plane.SetColor( color.SetAlpha( GameSettings.Instance.grid_plane_alpha ) );

		if( tooth_data.tooth_type == ToothType.None )
			slot_connected_tooth = slot.ConnectedToothType;

		if( slot_paired )
			slot_paired.ClearPaired();

		slot_paired    = slot;
		slot_color     = color;
		slot_occupied  = true;
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

		if( ToothType == ToothType.None )
		{
			slot_connected_tooth = ToothType.None;
			slot_occupied        = false;
		}

		if( slot_line )
		{
			pool_line.ReturnEntity( slot_line );
			slot_line = null;
		}

		slot_connected?.Clear();
		slot_connected = null;
		slot_paired = null;
	}

	public void ClearPaired()
	{
		if( slot_line )
		{
			pool_line.ReturnEntity( slot_line );
			slot_line = null;
		}

		slot_connected = null;

		if( !slot_paired )
		{
			tooth_selection_plane.SetColor( GameSettings.Instance.grid_default_color );

			if( ToothType == ToothType.None )
			{
				slot_connected_tooth = ToothType.None;
				slot_occupied = false;
			}
		}
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
	private void OnDrawGizmos()
	{
		Handles.Label( transform.position + Vector3.up * 0.1f, "Tooth Type:" + slot_connected_tooth.ToString() );
	}
#endif
#endregion
}