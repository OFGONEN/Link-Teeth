/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class PalateTooth : MonoBehaviour
{
#region Fields
    [ ShowInInspector, ReadOnly ] private PalateToothSet tooth_set;
    [ ShowInInspector, ReadOnly ] private Renderer tooth_renderer;
    [ ShowInInspector, ReadOnly ] private ColorSetter_Update tooth_setter_color;
    [ ShowInInspector, ReadOnly ] private FillSetter tooth_setter_fill;

    private PalateToothData tooth_data;
	private int tooth_health;
#endregion

#region Properties
#endregion

#region Unity API
	private void OnDisable()
	{
		tooth_set.RemoveList( this );
	}
#endregion

#region API
    public void Spawn( PalateToothSet set, PalateToothData data )
    {
		tooth_set    = set;
		tooth_data   = data;
		tooth_health = data.tooth_health;

		tooth_renderer     = GetComponent< Renderer >();
		tooth_setter_color = gameObject.AddComponent< ColorSetter_Update >();
		tooth_setter_fill  = gameObject.AddComponent< FillSetter >();

		tooth_renderer.sharedMaterial = GameSettings.Instance.material_flashing;
		tooth_setter_color.SetColor( data.tooth_color );
		tooth_setter_fill.SetupFillRange( GameSettings.Instance.tooth_fill_value_min, GameSettings.Instance.tooth_fill_value_max );

		tooth_set.AddList( this );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}