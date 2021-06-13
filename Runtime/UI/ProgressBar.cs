using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Kellojo.Utility;

namespace Kellojo.UI {

    public class ProgressBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] Image progressBar;
        [SerializeField] TMP_Text innerText;
        [SerializeField] CanvasGroup innerTextGroup;
        [SerializeField] public Image innerImage;
        [SerializeField] CanvasGroup innerImageGroup;

        [SerializeField] int _startValue = 0;
        [SerializeField] int _currentValue = 0;
        [SerializeField] int _maxValue = 1;

        [SerializeField] string filledText = "Research";
        [SerializeField] Color filledColor = Color.white;
        Color _defaultColor;

        public UnityEvent OnPress;
        UnityEngine.UI.Button InnerButton;

        public int startValue {
            set {
                _startValue = value;
                UpdateProgressBar();
            }
            get {
                return _startValue;
            }
        }
        public int maxValue {
            set {
                _maxValue = value;
                UpdateProgressBar();
            }
            get {
                return _maxValue;
            }
        }
        public int currentValue {
            set {
                _currentValue = value;
                UpdateProgressBar();
            }
            get {
                return _currentValue;
            }
        }

        TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> tweener;

        private void Awake() {
            _defaultColor = progressBar.color;
            InnerButton = GetComponentInChildren<UnityEngine.UI.Button>();
            UpdateProgressBar();
            InnerButton.onClick.AddListener(OnPress.Invoke);
        }
        private void OnValidate() {
            UpdateProgressBar();
        }

        void UpdateProgressBar() {
            float targetFill = (float)(currentValue - startValue) / (float)(maxValue - startValue);

            tweener?.Kill();
            tweener = null;
            tweener = progressBar.DOFillAmount(targetFill, 0.5f).SetEase(Ease.OutCubic);

            innerText.SetText(FormatInnerText(targetFill));

            if (targetFill >= 1) {
                progressBar.DOColor(filledColor, 0.25f).SetEase(Ease.OutBounce);
            } else {
                progressBar.DOColor(_defaultColor, 0.25f).SetEase(Ease.OutCubic);
            }
        }

        protected virtual string FormatInnerText(float targetFill) {
            if (targetFill >= 1) {
                return filledText;
            } else {
                return currentValue + "/" + maxValue;
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
           
        }
        public void OnPointerExit(PointerEventData eventData) {
           
        }

    }
}