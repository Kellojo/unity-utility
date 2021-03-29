using System.Collections;
using System.Collections.Generic;
using Kellojo.Items;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Kellojo.UI {

    public class ItemTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler {

        [SerializeField] TMP_Text NameText;
        [SerializeField] TMP_Text AmountText;
        [SerializeField] Image Icon;
        CanvasGroup CanvasGroup;
        public Item Item {
            get; private set;
        }
        public Inventory Inventory {
            get; private set;
        }
        RectTransform DragParent;
        Transform initialParent;
        Canvas DragScaleProvider;

        RectTransform rectTransform;


        private void Awake() {
            CanvasGroup = GetComponent<CanvasGroup>();
            NameText.DOFade(0, 0);
            rectTransform = GetComponent<RectTransform>();
        }

        public void UpdateAmount(int amount) {
            AmountText.SetText(amount.ToString());
        }
        public void SetItem(Item item, int amount, Inventory inventory, Canvas dragScaleProvider, RectTransform dragParent) {
            if (item != null) {
                Item = item;
                Inventory = inventory;
                NameText.SetText(item.name);
                AmountText.SetText(amount.ToString());
                Icon.sprite = item.Icon;
                DragParent = dragParent;
                DragScaleProvider = dragScaleProvider;
            }
        }


        public void OnPointerEnter(PointerEventData eventData) {
            NameText.DOFade(1, 0.25f).SetEase(Ease.OutCubic);
        }
        public void OnPointerExit(PointerEventData eventData) {
            NameText.DOFade(0, 0.25f).SetEase(Ease.OutCubic);
        }


        public void OnBeginDrag(PointerEventData eventData) {
            initialParent = transform.parent;
            transform.SetParent(DragParent);
            CanvasGroup.blocksRaycasts = false;
        }
        public void OnDrag(PointerEventData eventData) {
            rectTransform.anchoredPosition += eventData.delta / DragScaleProvider.scaleFactor;
        }
        public void OnEndDrag(PointerEventData eventData) {
            transform.SetParent(initialParent);
            CanvasGroup.blocksRaycasts = true;
        }
    }

}

