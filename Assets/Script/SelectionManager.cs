/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "manager_selection", menuName = "FF/Manager/Selection" ) ]
public class SelectionManager : ScriptableObject
{
#region Fields
    [ BoxGroup( "Setup" ) ] public LinePool pool_line;
    [ BoxGroup( "Setup" ) ] public SlotPool pool_slot;

    [ BoxGroup( "Fired Events" ) ] public GameEvent puzzle_solved_event;

    [ ReadOnly ] public List< Slot > slot_tooth_list = new List< Slot >( 16 );
    [ ReadOnly ] private Slot selection_current;

    // Delegates
    private SlotMessage onSlot_Select;
    private SlotMessage onSlot_DeSelect;
    private UnityMessage onSlot_SelectionStop;
#endregion

#region Properties
#endregion

#region Unity API
    public void LevelAwake()
    {
		slot_tooth_list.Clear();

		onSlot_Select        = ExtensionMethods.EmptyMethod;
		onSlot_DeSelect      = ExtensionMethods.EmptyMethod;
		onSlot_SelectionStop = ExtensionMethods.EmptyMethod;
    }
#endregion

#region API
    public void ResetSelectionMethods()
    {
        FFLogger.Log( "Selection Reset", this );
		onSlot_Select        = OnSlot_Select_Initial;
		onSlot_DeSelect      = ExtensionMethods.EmptyMethod;
		onSlot_SelectionStop = ResetSelectionMethods;

		selection_current = null;
	}

    public void NullSelectionMethod()
    {
		onSlot_Select        = ExtensionMethods.EmptyMethod;
		onSlot_DeSelect      = ExtensionMethods.EmptyMethod;
		onSlot_SelectionStop = ExtensionMethods.EmptyMethod;
    }

    public void OnSlot_Select( Slot slot )
    {
		onSlot_Select( slot );
	}

    public void OnSlot_DeSelect( Slot slot )
    {
		onSlot_DeSelect( slot );
	}

    public void OnSelectionStop()
    {
		onSlot_SelectionStop();
	}

    public Slot GivePairedSlot( Slot slot )
    {
		var index = slot.GridIndex;

		var grid_data   = CurrentLevelData.Instance.levelData.CurrentGridData;
		var grid_width  = grid_data.GridWidth;
		var grid_height = grid_data.GridHeight;

        if( index.x - 1 >= 0 ) // Left slot
        {
			Slot slot_left;

			var index_left    = index;
			    index_left.x -= 1;

            if( pool_slot.pool_dictionary.TryGetValue( index_left, out slot_left ) && slot_left.SlotConnected == slot )
            {
				return slot_left;
			}
		}
        
        if( index.x + 1 < grid_width ) // Right Slot
        {
			Slot slot_right;

			var index_right    = index;
			    index_right.x += 1;

            if( pool_slot.pool_dictionary.TryGetValue( index_right, out slot_right ) && slot_right.SlotConnected == slot )
            {
				return slot_right;
			}
		}
        
        if( index.y - 1 >= 0 ) // Up Slot
        {
			Slot slot_up;

			var index_up    = index;
			    index_up.y -= 1;

            if( pool_slot.pool_dictionary.TryGetValue( index_up, out slot_up ) && slot_up.SlotConnected == slot )
            {
				return slot_up;
			}
		}
        
        if( index.y + 1 < grid_height ) // Down Slot
        {
			Slot slot_down;

			var index_down    = index;
			    index_down.y -= 1;

            if( pool_slot.pool_dictionary.TryGetValue( index_down, out slot_down ) && slot_down.SlotConnected == slot )
            {
				return slot_down;
			}
		}

		return null;
	}
#endregion

#region Implementation
    private void OnSlot_Select_Initial( Slot slot ) 
    {
        if( slot.SlotOccupied )
        {
			onSlot_Select     = OnSlot_Selection_Consecutive;
			selection_current = slot;

            slot.ClearFrontConnections();
        }
        else
        {
			onSlot_Select        = ExtensionMethods.EmptyMethod;
			onSlot_DeSelect      = ExtensionMethods.EmptyMethod;
			onSlot_SelectionStop = ResetSelectionMethods;
        }
    }

    private void OnSlot_Selection_Consecutive( Slot slot )
    {
        if( selection_current == slot || !CheckIfDiagonal( selection_current, slot ) || selection_current.SlotPaired == slot ) return;

		if( slot.ToothType == ToothType.None )
        {
			slot.ClearFrontConnections();

			selection_current.PairSlot( slot );
			selection_current = slot;
        }
        else if( slot.ConnectedToothType == selection_current.ConnectedToothType && slot.ConnectedToothColor.CompareColor( selection_current.ConnectedToothColor ) )
        {
			slot.ClearStrayConnections();
			selection_current.PairSlot( slot );
			selection_current = slot;

			CheckIfPuzzleSolved();
		}
    }

    private void OnSlot_DeSelect_Initial( Slot slot )
    {

    }

    private bool CheckIfDiagonal( Slot current, Slot selection ) // Returns true if vertical and horizontal
    {
		var horizontal = current.GridIndex.y == selection.GridIndex.y && Mathf.Abs( current.GridIndex.x - selection.GridIndex.x ) <= 1;
		var vertical   = current.GridIndex.x == selection.GridIndex.x && Mathf.Abs( current.GridIndex.y - selection.GridIndex.y ) <= 1;

		return horizontal || vertical;
	}

    private void CheckIfPuzzleSolved()
    {
		bool puzzleSolved = true;

		for( var i = 0; i < slot_tooth_list.Count; i++ )
		{
			var slot = slot_tooth_list[ i ];
			var solved = slot.IsToothConnected() || slot.IsToothPaired();

			if( !solved )
			{
				puzzleSolved = false;
				break;
			}
		}

		if( puzzleSolved )
        {
            FFLogger.Log( "Puzzle Solved" );
			NullSelectionMethod();

			DOVirtual.DelayedCall( GameSettings.Instance.line_disappear_delay, puzzle_solved_event.Raise );
			// puzzle_solved_event.Raise();
		}
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}