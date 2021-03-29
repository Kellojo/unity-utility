using Kellojo.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Building {
    public class Building : MonoBehaviour, IBuildable {
        [HideInInspector] public BuildingType BuildingType;
        [System.NonSerialized] public Inventory Inventory;

        private bool inventoryAccessible = false;
        public bool IsInteractable {
            get {
                return Inventory != null && inventoryAccessible;
            }
        }

        public virtual void OnBuildingPlaced() {
            Invoke("OnBuildingAvailableForInteraction", 0.5f);
        }
        public virtual void OnSegmentPlaced(GameObject segment, int index) {
            
        }
        public virtual void OnStartBuilding() {
           
        }

        void OnBuildingAvailableForInteraction() {
            inventoryAccessible = true;
        }
    }

}

