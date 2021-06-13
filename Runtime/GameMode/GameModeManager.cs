using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kellojo.Manager;
using UnityEngine.SceneManagement;

namespace Kellojo.GameMode {

    [RequireComponent(typeof(App))]
    public class GameModeManager : MonoBehaviour {

        protected IGameMode currentGameMode;
        bool isSwitchingMode = false;


        App App;

        private void Awake() {
            App = GetComponent<App>();

            HandleGameModeStartup();
        }

        protected virtual void HandleGameModeStartup() {}


        /// <summary>
        /// Switches the game mode
        /// </summary>
        /// <param name="gameMode"></param>
        /// <returns></returns>
        protected IEnumerator SwitchMode(IGameMode gameMode) {
            Debug.Log("Loading...");
            yield return new WaitUntil(() => !isSwitchingMode);
            if (currentGameMode == gameMode) yield break;

            isSwitchingMode = true;
            yield return App.ShowLoadingScreen();

            if (currentGameMode != null)
                yield return currentGameMode.OnEnd();
            currentGameMode = gameMode;
            yield return currentGameMode.OnStart();
            yield return App.HideLoadingScreen();
            isSwitchingMode = false;
            Debug.Log("Done!...");
        }

    }

}

