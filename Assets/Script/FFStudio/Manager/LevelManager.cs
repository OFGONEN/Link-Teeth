/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace FFStudio
{
    public class LevelManager : MonoBehaviour
    {
#region Fields
        [ Header( "Event Listeners" ) ]
        public EventListenerDelegateResponse levelLoadedListener;
        public EventListenerDelegateResponse levelRevealedListener;
        public EventListenerDelegateResponse levelStartedListener;
        public EventListenerDelegateResponse palate_mouth_end_listener;

        [ Header( "Fired Events" ) ]
        public GameEvent levelFailedEvent;
        public GameEvent levelCompleted;

        [ Header( "Level Releated" ) ]
        public SharedFloatNotifier levelProgress;

        [ Header( "Managers" ) ]
        public SelectionManager manager_selection;
#endregion

#region UnityAPI
        private void OnEnable()
        {
            levelLoadedListener.OnEnable();
            levelRevealedListener.OnEnable();
            levelStartedListener.OnEnable();

			palate_mouth_end_listener.OnEnable();
		}

        private void OnDisable()
        {
            levelLoadedListener.OnDisable();
            levelRevealedListener.OnDisable();
            levelStartedListener.OnDisable();

			palate_mouth_end_listener.OnDisable();
        }

        private void Awake()
        {
            levelLoadedListener.response       = LevelLoadedResponse;
            levelRevealedListener.response     = LevelRevealedResponse;
            levelStartedListener.response      = LevelStartedResponse;
            palate_mouth_end_listener.response = PalateMouthEndResponse;

			manager_selection.LevelAwake();
		}
#endregion

#region Implementation
        private void LevelLoadedResponse()
        {
			levelProgress.SharedValue = 0;

			var levelData = CurrentLevelData.Instance.levelData;

            // Set Active Scene
			if( levelData.overrideAsActiveScene )
				SceneManager.SetActiveScene( SceneManager.GetSceneAt( 1 ) );
            else
				SceneManager.SetActiveScene( SceneManager.GetSceneAt( 0 ) );
		}

        private void LevelRevealedResponse()
        {
			palate_mouth_end_listener.response = PalateMouthEndResponse;
		}

        private void LevelStartedResponse()
        {
		}

        private void PalateMouthEndResponse()
        {
			DOVirtual.DelayedCall( GameSettings.Instance.ui_level_complete_delay, levelCompleted.Raise );
			palate_mouth_end_listener.response = ExtensionMethods.EmptyMethod;
		}
#endregion
    }
}