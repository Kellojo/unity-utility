using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kellojo.Items;
using DG.Tweening;

namespace Kellojo.Environment {
    public class DepleatingSpawnableInstance : SpawnableInstance, IHarvestable {

        public Item Resource;
        public int AvailableResources = 50;
        public float RecoveryTime = 60f;
        int RemainingResources;

        protected void Awake() {
            RemainingResources = AvailableResources;
        }

        public Item Harvest() {
            if (RemainingResources > 0) {
                RemainingResources--;

                if (RemainingResources == 0) {
                    OnResourceDepleted();
                }

                return Resource;
            }
            return null;
        }

        protected virtual void OnResourceDepleted() {
            transform.DOScale(Vector3.zero, 0.5f);
            StopAllCoroutines();
            StartCoroutine(Revocer());
        }

        protected IEnumerator Revocer() {
            yield return new WaitForSeconds(RecoveryTime / 2);
            transform.DOScale(Vector3.one, RecoveryTime / 2);
            yield return new WaitForSeconds(RecoveryTime / 2);
            RemainingResources = AvailableResources;
        }
    }
}
