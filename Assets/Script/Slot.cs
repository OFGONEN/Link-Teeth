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
    [ BoxGroup( "Shared" ) ] public SlotPool pool_slot;

    [ BoxGroup( "Setup" ) ] public ColorSetter tooth_selection_plane;
    [ BoxGroup( "Setup" ) ] public BoxCollider tooth_selection_collider;

    // Private \\
    [ ReadOnly ] private GridToothData tooth_data;
    private Tooth tooth_spawned;
    private Vector2Int grid_index;

    private bool slot_occupied;
	private Color slot_color;
	private Line slot_line;
	private Color slot_connected_tooth_color;
	[ ShowInInspector, ReadOnly ] private ToothType slot_connected_tooth;
	[ ShowInInspector, ReadOnly ] private Slot slot_paired;
	[ ShowInInspector, ReadOnly ] private Slot slot_connected;
#endregion

#region Properties
	public ToothType ConnectedToothType => slot_connected_tooth;
	public Color ConnectedToothColor 	=> slot_connected_tooth_color;
    public ToothType ToothType => tooth_data.tooth_type;
	public Vector2Int GridIndex   => grid_index;
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
		grid_index = new Vector2Int( grid_index_x, grid_index_y );

		if( tooth_data.tooth_type == ToothType.Canine )
			SpawnTooth( GameSettings.Instance.tooth_pool_canine );
        else if( tooth_data.tooth_type == ToothType.Molar )
			SpawnTooth( GameSettings.Instance.tooth_pool_molar );
        else if( tooth_data.tooth_type == ToothType.PreMolar )
			SpawnTooth( GameSettings.Instance.tooth_pool_premolar );
        else // Tooth type is null
			SpawnNull();

		slot_connected_tooth       = tooth_data.tooth_type;
		slot_connected_tooth_color = tooth_data.tooth_color;

		slot_line      = null;
		slot_connected = null;
		slot_paired    = null;
	
		pool_slot.pool_dictionary.Add( grid_index, this ); // Removed when returned to pool
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
		//! this paired tooth == that connected tooth, do not pair!
		//! Cannot connect to a tooth that is connected to this!
		if( FindPairedTooth() == slot.FindConnectedTooth() ) return;

		tooth_selection_plane.SetColor( slot_color.SetAlpha( GameSettings.Instance.grid_plane_alpha ) );
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
		{
			slot_connected_tooth       = slot.ConnectedToothType;
			slot_connected_tooth_color = slot.ConnectedToothColor;
		}

		if( slot_paired && ToothType == ToothType.None )
			slot_paired.ClearPaired();

		slot_paired    = slot;
		slot_color     = color;
		slot_occupied  = true;
	}

	public void ClearFrontConnections()
	{
		ClearLine();

		var connected  = slot_connected;
		slot_connected = null;

		connected?.Clear();
	}

	public void Clear()
	{
		if( ToothType != ToothType.None && SetNewPair() ) return;

		tooth_selection_plane.SetColor( GameSettings.Instance.grid_default_color );

		if( ToothType == ToothType.None )
			ClearNoneType();

		ClearLine();

		var connected = slot_connected;

		slot_connected = null;
		slot_paired    = null;

		connected?.Clear();
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
				ClearNoneType();
		}
	}

	public void ClearStrayConnections()
	{
		if( !IsToothConnected() )
			ClearFrontConnections();
	}

	public bool IsToothConnected()
	{
		if( slot_connected )
		{
			if( slot_connected.ToothType != ToothType.None )
				return true;
			else
				return slot_connected.IsToothConnected();
		}
		else
			return false;
	}

	public Slot FindPairedTooth()
	{
		if( ToothType != ToothType.None )
			return this;
		else if( slot_paired )
			return slot_paired.FindPairedTooth();
		else
			return null;
	}

	public Slot FindConnectedTooth()
	{
		if( slot_connected )
		{
			if( slot_connected.ToothType != ToothType.None )
				return slot_connected;
			else
				return slot_connected.FindConnectedTooth();
		}
		else
			return null;
	}

	public void ReturnToPool()
	{
		if( ToothType != ToothType.None )
			manager_selection.slot_tooth_list.Remove( this );

		pool_slot.ReturnEntity( this );
	}

	public void ClearLine()
	{
		if( slot_line )
		{
			pool_line.ReturnEntity( slot_line );
			slot_line = null;
		}
	}
#endregion

#region Implementation
    private void SpawnTooth( ToothPool pool )
    {
		var tooth = pool.GetEntity();

		tooth.transform.position = transform.position;
		tooth.transform.rotation = transform.rotation;

		tooth.Spawn( tooth_data );

		tooth_spawned = tooth;
		slot_occupied = true;
		slot_color    = tooth_data.tooth_color;

		tooth_selection_plane.SetColor( slot_color.SetAlpha( GameSettings.Instance.grid_plane_alpha ) );
		manager_selection.slot_tooth_list.Add( this );
	}

    private void SpawnNull()
    {
		slot_occupied  = false;
		slot_color     = GameSettings.Instance.grid_default_color;

		tooth_selection_plane.SetColor( GameSettings.Instance.grid_default_color );
	}

	private void ClearNoneType()
	{
		slot_connected_tooth       = ToothType.None;
		slot_connected_tooth_color = Color.black;
		slot_occupied              = false;
	}

	private bool SetNewPair()
	{
		var slot = manager_selection.GivePairedSlot( this );

		if( slot )
		{
			ConnectSlot( slot );
			return true;
		}
		else
		{
			slot_paired = null;
			return false || slot_connected;
		}
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