/* Created by and for usage of FF Studios (2021). */
using UnityEngine;
using DG.Tweening;

namespace FFStudio
{
	/* This class holds references to ScriptableObject assets. These ScriptableObjects are singletons, so they need to load before a Scene does.
	 * Using this class ensures at least one script from a scene holds a reference to these important ScriptableObjects. */
	public class AssetManager : MonoBehaviour
	{
#region Fields
		public GameSettings gameSettings;
		public CurrentLevelData currentLevelData;

		public Material shared_transparent_material;

		public ToothPool[] toothPools;
		public TransformPool[] transformPools;
		public SlotPool slotPool;
		public LinePool linePool;


		private void Awake()
		{
			// Flashing material
			shared_transparent_material.color = shared_transparent_material.color.SetAlpha( GameSettings.Instance.tooth_transparent_start );

			shared_transparent_material.DOFade( GameSettings.Instance.tooth_transparent_end, GameSettings.Instance.tooth_transparent_duration )
				.SetLoops( -1, LoopType.Yoyo );
			
			for( var i = 0; i < transformPools.Length; i++ )
				transformPools[ i ].InitPool( transform, false );

			for( var i = 0; i < toothPools.Length; i++ )
				toothPools[ i ].InitPool( transform, false );


			slotPool.InitPool( transform, false );
			linePool.InitPool( transform, false );
		}
#endregion
	}
}