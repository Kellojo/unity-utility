using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kellojo.Items;

namespace Kellojo.Environment {
    public class DepleatingSpawnableInstance : SpawnableInstance, IHarvestable {

        public Item Resource;
        public int AvailableResources = 50;
        int RemainingResources;

        protected void Awake() {
            RemainingResources = AvailableResources;
        }

        public Item Harvest() {
            if (RemainingResources > 0) {
                return Resource;
            }
            RemainingResources = RemainingResources--;
            return null;
        }
    }
}
