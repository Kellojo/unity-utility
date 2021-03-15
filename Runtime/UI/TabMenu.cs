using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace Kellojo.UI {
    public class TabMenu : MonoBehaviour {

        Button[] Buttons;

        private void Awake() {
            Buttons = GetComponentsInChildren<Button>();

            foreach(Button button in Buttons) {
                button.onClick.AddListener(() => { OnButtonPress(button); });
            }
        }


        void OnButtonPress(Button button) {
            foreach (Button btn in Buttons) {
                TMP_Text text = btn.GetComponentInChildren<TMP_Text>();

                if (btn == button) {
                    text.fontStyle = FontStyles.Bold | FontStyles.Underline;
                } else {
                    text.fontStyle = FontStyles.Normal;
                }

                

            }
        }


    }

}
