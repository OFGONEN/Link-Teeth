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

        [ BoxGroup( "Level" ) ] public GridData[] grid_data_array;
        [ BoxGroup( "Level" ) ] public PalateData palate_data_upper;
        [ BoxGroup( "Level" ) ] public PalateData palate_data_lower;
    }
}
