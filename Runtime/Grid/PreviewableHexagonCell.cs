using Kellojo.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using DG.Tweening.Core;
using System;

namespace Kellojo.Grid {

    public class PreviewableHexagonCell : HexagonCell, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

        const float HOVER_HEIGHT = 0.25f;
        TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> tween = null;

        [SerializeField] Material previewMaterial;

        bool isPreview = false;
        public bool IsPreview {
            get { return isPreview; }
            set {
                isPreview = value;

                if (isPreview) {
                    materialChanger.SetMaterialOnAllMeshRenderers(previewMaterial);
                } else {
                    materialChanger.RestoreDefaultMaterials();
                }
            }
        }
        public Action<PreviewableHexagonCell> OnExitPreview;


        MaterialChanger materialChanger;

        private void Awake() {
            materialChanger = new MaterialChanger(gameObject);
        }


        public void OnPointerEnter(PointerEventData eventData) {
            if (!isPreview) {
                return;
            }

            AbortCurrentTween();
            tween = transform.DOLocalMoveY(HOVER_HEIGHT, 0.25f).SetEase(Ease.OutCubic);
        }
        public void OnPointerExit(PointerEventData eventData) {
            if (!isPreview) {
                return;
            }

            AbortCurrentTween();
            tween = transform.DOLocalMoveY(0, 0.25f).SetEase(Ease.OutCubic);
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (!isPreview) {
                return;
            }

            // abort highlight tween
            AbortCurrentTween();
            IsPreview = false;
            OnExitPreview?.Invoke(this);

            // animation
            Sequence sequence = DOTween.Sequence();
            sequence.Join(transform.DOLocalJump(new Vector3(transform.position.x, 0, transform.position.z), 0.1f, 1, 0.25f).SetEase(Ease.OutCubic));
            sequence.Join(transform.DOPunchScale(Vector3.one * 0.25f, 0.25f));
        }


        void AbortCurrentTween() {
            if (tween != null) {
                tween.Kill();
                tween = null;
            }
        }
    }

}
