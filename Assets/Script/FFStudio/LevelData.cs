/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
	[ CreateAssetMenu( fileName = "LevelData", menuName = "FF/Data/LevelData" ) ]
	public class LevelData : ScriptableObject
    {
		[ BoxGroup( "Setup" ), NaughtyAttributes.Scene() ] public int sceneIndex;
        [ BoxGroup( "Setup" ) ] public bool overrideAsActiveScene;

        [ BoxGroup( "Level" ) ] public RealGridData[] grid_data_array;
        [ BoxGroup( "Level" ) ] public PalateData palate_data_upper;
        [ BoxGroup( "Level" ) ] public PalateData palate_data_lower;

        [ HideInEditorMode ] public int grid_data_index;

		public RealGridData CurrentGridData => grid_data_array[ grid_data_index ];

	}
}
