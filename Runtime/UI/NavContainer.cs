using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.UI {
    public class NavContainer : MonoBehaviour {


        [SerializeField] RectTransform InitialPage;
        [SerializeField] List<RectTransform> Pages;

        protected RectTransform currentPage { private set; get; }
        protected RectTransform previousPage { private set; get; }

        protected void Awake() {
            NavigateTo(InitialPage, true);
        }

        public void NavigateTo(RectTransform newPage, bool skipAnimation = false) {
            if (newPage == currentPage) {
                return;
            }

            foreach (RectTransform page in Pages) {
                if (page != newPage) {
                    FlyOutPage(page, skipAnimation);
                } else {
                    FlyInPage(page, skipAnimation);
                }
            }
        }

        public void FlyOutPage(RectTransform page, bool skipAnimation = false) {
            page.DOLocalMoveX(page.localPosition.x - page.rect.width, skipAnimation ? 0 : 0.25f).SetEase(Ease.OutCirc).OnComplete(() => {
                page.gameObject.SetActive(false);
            });
        }
        public void FlyInPage(RectTransform page, bool skipAnimation = false) {
            if (page.gameObject.activeInHierarchy) {
                return;
            }

            page.gameObject.SetActive(true);
            page.DOLocalMoveX(page.localPosition.x + page.rect.width, skipAnimation ? 0 : 0.25f).SetEase(Ease.OutCirc).OnComplete(() => {
                currentPage = page;
            }); ;
        }

        public void NavigateBack() {
            FlyOutPage(currentPage);
            FlyInPage(previousPage != null ? previousPage : InitialPage);
        }

    }

}


