/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

public class Slot : MonoBehaviour
{
#region Fields

    // Private \\
    private GridToothData toothData;
#endregion

#region Properties
    public ToothType ToothType => toothData.tooth_type;
    public Color ToothColor => toothData.tooth_color;
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( GridToothData data )
    {
		gameObject.SetActive( true );
		toothData = data;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}