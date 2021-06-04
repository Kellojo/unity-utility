using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Kellojo.Utility;
using UnityEngine.Events;
using Kellojo.InputSystem;
using TinyMessenger;
using Kellojo.Events;
using System.Runtime.Remoting.Messaging;

namespace Kellojo.UI {
    /// <summary>
    /// A basic menu component, which opens and closes
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class Menu : MonoBehaviour {

        RectTransform ScaleAnimationTarget;
        CanvasGroup CanvasGroup;
        GenericInput inputActions;

        public UnityEvent<Menu> OnOpen;
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
            EventBus.Publish(new MenuOpenedMessage());
            OnOpen?.Invoke(this);
            ScaleAnimationTarget.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0);
            ScaleAnimationTarget.DOScale(Vector3.one, 0.25f);
            CanvasGroup.Show();
            isOpen = true;
        }
        public void Close(bool withoutAnimation = false) {
            EventBus.Publish(new MenuClosedMessage());
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



        public class MenuOpenedMessage : ITinyMessage {
            public object Sender => throw new System.NotImplementedException();
        }
        public class MenuClosedMessage : ITinyMessage {
            public object Sender => throw new System.NotImplementedException();
        }

    }
}


