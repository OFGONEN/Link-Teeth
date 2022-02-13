/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Tooth : MonoBehaviour
{
#region Fields
    [ BoxGroup( "Setup" ) ] public ToothType tooth_type;
    [ BoxGroup( "Setup" ) ] public ColorSetter tooth_color_setter;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( Color color )
    {
		gameObject.SetActive( true );

		tooth_color_setter.SetColor( color );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
