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
                if (materialChanger != null) {
                    ApplyPreviewMaterials();
                }
                if (!isPreview) {
                    OnExitPreview?.Invoke(this);
                }
            }
        }
        public Action<PreviewableHexagonCell> OnExitPreview;


        MaterialChanger materialChanger;

        protected void Start() {
            materialChanger = new MaterialChanger(gameObject);
            ApplyPreviewMaterials();
        }


        public virtual void OnPointerEnter(PointerEventData eventData) {
            if (!isPreview) {
                return;
            }

            AbortCurrentTween();
            tween = transform.DOLocalMoveY(HOVER_HEIGHT, 0.25f).SetEase(Ease.OutCubic);
        }
        public virtual void OnPointerExit(PointerEventData eventData) {
            if (!isPreview) {
                return;
            }

            AbortCurrentTween();
            tween = transform.DOLocalMoveY(0, 0.25f).SetEase(Ease.OutCubic);
        }

        public virtual void OnPointerClick(PointerEventData eventData) {
            if (!isPreview || (eventData != null && eventData.button != PointerEventData.InputButton.Left)) {
                return;
            }

            IsPreview = false;
            PlayExitPreviewAnimation();
        }


        protected void AbortCurrentTween() {
            if (tween != null) {
                tween.Kill();
                tween = null;
            }
        }
        protected void PlayExitPreviewAnimation() {
            AbortCurrentTween();
            Sequence sequence = DOTween.Sequence();
            sequence.Join(transform.DOLocalJump(new Vector3(transform.position.x, 0, transform.position.z), 0.1f, 1, 0.25f).SetEase(Ease.OutCubic));
            sequence.Join(transform.DOPunchScale(Vector3.one * 0.25f, 0.25f));
        }

        void ApplyPreviewMaterials() {
            if (isPreview) {
                materialChanger.SetMaterialOnAllMeshRenderers(previewMaterial);
            } else {
                materialChanger.RestoreDefaultMaterials();
            }
        }
    }

}
