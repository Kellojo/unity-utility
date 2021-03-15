using System.Collections;
using System.Collections.Generic;
using Kellojo.Items;
using UnityEngine;
using UnityEngine.UI;


namespace Kellojo.UI {
    public class InventoryDisplay : MonoBehaviour {

        [SerializeField] GridLayoutGroup Grid;
        [SerializeField] GameObject ItemTilePrefab;
        Inventory inventory;


        public void SetInventory(Inventory inventory) {
            this.inventory = inventory;
            UpdateInventory();
        }

        void UpdateInventory() {
            foreach (Transform child in Grid.transform) {
                Destroy(child.gameObject);
            }

            foreach (Item item in inventory.Items) {
                GameObject itemTile = Instantiate(ItemTilePrefab, Grid.transform);

            }
        }
    }

}
