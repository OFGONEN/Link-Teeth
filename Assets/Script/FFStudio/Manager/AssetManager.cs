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


		private void Awake()
		{
			shared_transparent_material.color = shared_transparent_material.color.SetAlpha( GameSettings.Instance.tooth_transparent_start );

			shared_transparent_material.DOFade( GameSettings.Instance.tooth_transparent_end, GameSettings.Instance.tooth_transparent_duration )
				.SetLoops( -1, LoopType.Yoyo );
		}
#endregion
	}
}