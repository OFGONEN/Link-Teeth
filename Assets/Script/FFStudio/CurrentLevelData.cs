﻿/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
    public class CurrentLevelData : ScriptableObject
    {
#region Fields
		[ HideInInspector ] public int currentLevel_Real;
		[ HideInInspector ] public int currentLevel_Shown;
		[ HideInInspector ] public LevelData levelData;

        [ BoxGroup( "Shared" ) ] public PalateData current_palate_data_upper;
        [ BoxGroup( "Shared" ) ] public PalateData current_palate_data_lower;

        private static CurrentLevelData instance;

        private delegate CurrentLevelData ReturnCurrentLevel();
        private static ReturnCurrentLevel returnInstance = LoadInstance;

        public static CurrentLevelData Instance
        {
            get
            {
                return returnInstance();
            }
        }
#endregion

#region API
		public void LoadCurrentLevelData()
		{
			if( currentLevel_Real > GameSettings.Instance.maxLevelCount )
				currentLevel_Real = Random.Range( 1, GameSettings.Instance.maxLevelCount );

			levelData = Resources.Load<LevelData>( "level_data_" + currentLevel_Real );

			current_palate_data_upper = levelData.palate_data_upper;
			current_palate_data_lower = levelData.palate_data_lower;
		}
#endregion

#region Implementation
        static CurrentLevelData LoadInstance()
		{
			if( instance == null )
				instance = Resources.Load<CurrentLevelData>( "level_current" );

			returnInstance = ReturnInstance;

            return instance;
        }

        static CurrentLevelData ReturnInstance()
        {
            return instance;
        }
#endregion
    }
}