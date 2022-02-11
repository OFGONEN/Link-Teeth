﻿/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public class GameSettings : ScriptableObject
    {
#region Singleton Related
        private static GameSettings instance;

        private delegate GameSettings ReturnGameSettings();
        private static ReturnGameSettings returnInstance = LoadInstance;

		public static GameSettings Instance => returnInstance();
#endregion
        
#region Fields
        [ BoxGroup( "Remote Config" ) ] public bool useRemoteConfig_GameSettings;
        [ BoxGroup( "Remote Config" ) ] public bool useRemoteConfig_Components;

        public int maxLevelCount;
        [ BoxGroup( "UI Settings" ), Tooltip( "Duration of the movement for ui element"          ) ] public float ui_Entity_Move_TweenDuration;
        [ BoxGroup( "UI Settings" ), Tooltip( "Duration of the fading for ui element"            ) ] public float ui_Entity_Fade_TweenDuration;
		[ BoxGroup( "UI Settings" ), Tooltip( "Duration of the scaling for ui element"           ) ] public float ui_Entity_Scale_TweenDuration;
		[ BoxGroup( "UI Settings" ), Tooltip( "Duration of the movement for floating ui element" ) ] public float ui_Entity_FloatingMove_TweenDuration;
		[ BoxGroup( "UI Settings" ), Tooltip( "Joy Stick"                                        ) ] public float ui_Entity_JoyStick_Gap;
        [ BoxGroup( "UI Settings" ), Tooltip( "Percentage of the screen to register a swipe"     ) ] public int swipeThreshold;
		
        [ BoxGroup( "Patient" ) ] public float patient_movement_duration;
        [ BoxGroup( "Patient" ) ] public float patient_look_speed;

		[ BoxGroup( "Tooth" ) ] public float tooth_transparent_start;
		[ BoxGroup( "Tooth" ) ] public float tooth_transparent_end;
		[ BoxGroup( "Tooth" ) ] public float tooth_transparent_duration;
		[ BoxGroup( "Tooth" ) ] public float tooth_fill_value_min;
		[ BoxGroup( "Tooth" ) ] public float tooth_fill_value_max;

		[ BoxGroup( "Tooth" ) ] public ToothPool tooth_pool_canine;
		[ BoxGroup( "Tooth" ) ] public ToothPool tooth_pool_molar;
		[ BoxGroup( "Tooth" ) ] public ToothPool tooth_pool_premolar;

		[ BoxGroup( "Palate" ) ] public float palate_table_movement_duration;
		[ BoxGroup( "Palate" ) ] public float palate_mouth_movement_duration;

		[ BoxGroup( "Grid" ) ] public float grid_square_lenght;

        [ BoxGroup( "Material" ) ] public Material material_flashing;
        [ BoxGroup( "Material" ) ] public Material material_filling;

        [ BoxGroup( "Debug" ) ] public float debug_ui_text_float_height;
        [ BoxGroup( "Debug" ) ] public float debug_ui_text_float_duration;
#endregion

#region Implementation
        private static GameSettings LoadInstance()
		{
			if( instance == null )
				instance = Resources.Load< GameSettings >( "game_settings" );

			returnInstance = ReturnInstance;

			return instance;
		}

		private static GameSettings ReturnInstance()
        {
            return instance;
        }
#endregion
    }
}
