/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FFStudio;

public class PalateTest : MonoBehaviour
{
#region Fields
    public PalateData palateData;
#endregion

#region Properties
    private void Start()
    {
		ModifyTeeth();
	}
#endregion

#region Unity API
	private void ModifyTeeth()
	{
		var tooth_data_array = palateData.palateToothData;

		for( var i = 0; i < tooth_data_array.Length; i++ )
		{
			var data  = tooth_data_array[ i ];
			var tooth = transform.GetChild( data.tooth_index );

			var palate_tooth = tooth.gameObject.AddComponent< ColorSetter >();
			palate_tooth.SetColor( data.tooth_color );
		}
	}
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if( Application.isPlaying )
        {
			var tooth_data_array = palateData.palateToothData;

            GUIStyle style = new GUIStyle { normal = new GUIStyleState { textColor = Color.black }, fontSize = 10 };

			for( var i = 0; i < tooth_data_array.Length; i++ )
			{
				var data = tooth_data_array[ i ];
				var tooth = transform.GetChild( data.tooth_index );

				var position = tooth.position + Vector3.up * 0.05f;

				Handles.color = data.tooth_color;
				Handles.Label( position, "Tooth: " + data.tooth_type.ToString() + "\nHealth " + data.tooth_health, style );

				Handles.color = Color.black;
				Handles.DrawLine( tooth.position, position );
			}
		}
    }
#endif
#endregion
}