using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Kellojo.Utility;
using UnityEngine.Events;
using Kellojo.InputSystem;

namespace Kellojo.UI {
    /// <summary>
    /// A basic menu component, which opens and closes
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class Menu : MonoBehaviour {

        RectTransform ScaleAnimationTarget;
        CanvasGroup CanvasGroup;
        GenericInput inputActions;

        public UnityEvent OnOpen;
        public UnityEvent OnClose;

        bool isOpen;
        public bool IsOpen {
            get {
                return isOpen;
            }
            private set {
                if (value) {
                    Open();
                } else {
                    Close();
                }
            }
        }

        protected void Awake() {
            CanvasGroup = GetComponent<CanvasGroup>();
            ScaleAnimationTarget = GetComponent<RectTransform>();
            Close(true);

            inputActions = new GenericInput();
            inputActions.UserInterface.Close.performed += context => { Close(); };
        }

        protected void OnEnable() {
            inputActions.Enable();
        }
        protected void OnDisable() {
            inputActions.Disable();
        }

        public void Open() {
            OnOpen?.Invoke();
            ScaleAnimationTarget.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0);
            ScaleAnimationTarget.DOScale(Vector3.one, 0.25f);
            CanvasGroup.Show();
            isOpen = true;
        }
        public void Close(bool withoutAnimation = false) {
            OnClose?.Invoke();
            CanvasGroup.Hide(withoutAnimation);
            ScaleAnimationTarget.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.25f);
            isOpen = false;
        }
        /// <summary>
        /// Toggles the menu visibility
        /// </summary>
        public void Toggle() {
            IsOpen = !IsOpen;
        }



    }
}


