using Kellojo.GameMode;
using Kellojo.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kellojo.Manager {
    public class App : MonoBehaviour {

        [SerializeField] CanvasGroup LoadingScreen;
        GameModeManager GameModeManager;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap() {
            GameObject app = Instantiate(Resources.Load("App")) as GameObject;

            if (app == null)
                throw new ApplicationException();

            DontDestroyOnLoad(app);

            
        }

        private void Awake() {
            GameModeManager = GetComponent<GameModeManager>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            StartCoroutine(HideLoadingScreen(true));
        }

        protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            
        }

        public virtual IEnumerator ShowLoadingScreen() {
            LoadingScreen.Show(false, 1f);
            LoadingScreen.blocksRaycasts = true;
            LoadingScreen.interactable = true;
            yield return new WaitForSeconds(1f);
        }
        public virtual IEnumerator HideLoadingScreen(bool withoutAnimation = false) {
           LoadingScreen.Hide(withoutAnimation, 1f);
            LoadingScreen.blocksRaycasts = false;
            LoadingScreen.interactable = false;
            yield return new WaitForSeconds(1f);
        }

    }


}

