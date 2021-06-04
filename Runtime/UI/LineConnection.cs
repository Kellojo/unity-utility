using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Kellojo.UI {


    [ExecuteInEditMode]
    public class LineConnection : MonoBehaviour
    {
        public RectTransform StartPoint;
        public RectTransform EndPoint;
        public int Padding = 75 / 2 + 10;
        RectTransform rectTransform;

        [SerializeField] private bool _active;
        public bool Active {
            get {
                return _active;
            }
            set {
                _active = value;
                ApplyColor();
            }
        }
        [SerializeField] private Color ActiveColor;
        Color DefaultColor;
        Image image;

        protected void Awake() {
            image = GetComponent<Image>();
            DefaultColor = image.color;
            rectTransform = GetComponent<RectTransform>();

            ApplyColor();
        }

        private void Start() {
            Layout();
        }
        private void OnValidate() {
            Layout();
            ApplyColor();
        }

        public void Layout() {
            if (StartPoint == null || EndPoint == null) {
                return;
            }
            if (rectTransform == null) {
                rectTransform = GetComponent<RectTransform>();
            }

            Vector3 start = StartPoint.anchoredPosition;
            Vector3 end = EndPoint.anchoredPosition;

            Vector3 center = start + (end - start) / 2;
            rectTransform.anchoredPosition = center;

            float height = (end - start).magnitude - 2 * Padding;
            Vector2 size = rectTransform.sizeDelta;
            size.y = height;
            rectTransform.sizeDelta = size;

            Vector3 diff = start - end;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            rectTransform.localRotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        void ApplyColor() {
            image.DOColor(Active ? ActiveColor : DefaultColor, 0.25f).SetEase(Ease.OutCirc);
        }

    }

}