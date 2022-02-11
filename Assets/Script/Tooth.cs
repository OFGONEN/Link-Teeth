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
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Spawn()
    {
		gameObject.SetActive( true );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
