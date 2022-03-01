/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "pool_slot", menuName = "FF/Data/Pool/Slot" ) ]
public class SlotPool : ComponentPool< Slot >
{
	public Dictionary< Vector2Int, Slot > pool_dictionary;

	public override void InitPool( Transform parent, bool active )
    {
		base.InitPool( parent, active );

		pool_dictionary = new Dictionary< Vector2Int, Slot >( stackSize );
	}

	public override void ReturnEntity( Slot entity )
	{
		pool_dictionary.Remove( entity.GridIndex );
		base.ReturnEntity( entity );
	}

    [ Button() ]
    public void LogDictionary()
    {
        foreach( var element in pool_dictionary )
        {
            FFLogger.Log( "Slot: " + element.Value.GridIndex, element.Value );
        }
    }
}