using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Button : UnityEngine.UI.Button {
    UnityEngine.UI.Button button;

    TMP_Text Text;
    [SerializeField] Color32 HighlightColor = new Color32(255, 209, 102, 255);
    Color32 DefaultColor;

    protected override void Awake() {
        base.Awake();
        Text = GetComponentInChildren<TMP_Text>();
        DefaultColor = Text.color;
    }

    public override void OnSelect(BaseEventData eventData) {
        base.OnSelect(eventData);
        Text.DOColor(HighlightColor, 0.2f).SetEase(Ease.OutCirc);
    }
    public override void OnDeselect(BaseEventData eventData) {
        base.OnDeselect(eventData);
        Text.DOColor(DefaultColor, 0.2f).SetEase(Ease.OutCirc);
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        base.OnPointerEnter(eventData);
        Text.DOColor(HighlightColor, 0.2f).SetEase(Ease.OutCirc);
    }
    public override void OnPointerExit(PointerEventData eventData) {
        base.OnPointerExit(eventData);
        Text.DOColor(DefaultColor, 0.2f).SetEase(Ease.OutCirc);
    }

}
