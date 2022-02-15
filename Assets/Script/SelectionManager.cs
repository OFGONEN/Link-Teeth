/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "manager_selection", menuName = "FF/Manager/Selection" ) ]
public class SelectionManager : ScriptableObject
{
#region Fields
    [ BoxGroup( "Setup" ) ] public LinePool pool_line;

    [ ReadOnly ] private Slot selection_current;

    // Delegates
    private SlotMessage onSlot_Select;
    private SlotMessage onSlot_DeSelect;
#endregion

#region Properties
#endregion

#region Unity API
    public void LevelAwake()
    {
        onSlot_Select        = ExtensionMethods.EmptyMethod;
		onSlot_DeSelect      = ExtensionMethods.EmptyMethod;
		onSlot_SelectionStop = ExtensionMethods.EmptyMethod;
    }
#endregion

#region API
    public void OnLevelStart()
    {
		onSlot_Select   = OnSlot_Select_Initial;
		onSlot_DeSelect = OnSlot_DeSelect_Initial;
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
    }
#endregion

#region Implementation
    private void OnSlot_Select_Initial( Slot slot ) 
    {
        if( slot.ToothType == ToothType.None )
        {
            if( slot.SlotOccupied )
            {
                FFLogger.Log( "Occupied" );
            }
            else
            {
				onSlot_Select   = ExtensionMethods.EmptyMethod;
				onSlot_DeSelect = ExtensionMethods.EmptyMethod;
			}
        }
    }

    private void OnSlot_Selection_Consecutive( Slot slot )
    {

    }

    private void OnSlot_DeSelect_Initial( Slot slot )
    {

    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}