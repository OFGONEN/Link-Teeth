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

    public Slot selection_current;

    // Delegates
    private SlotMessage onSlot_Select;
    private SlotMessage onSlot_DeSelect;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnSlot_Select( Slot slot )
    {
    }

    public void OnSlot_DeSelect( Slot slot )
    {
    }

    public void OnSlot_Nothing()
    {
    }
#endregion

#region Implementation
    private void OnSlot_Select_Initial( Slot slot ) 
    {

    }

    private void OnSlot_Selection_Consecutive( Slot slot )
    {

    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}