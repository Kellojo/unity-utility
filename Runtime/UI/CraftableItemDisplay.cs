using System.Collections;
using System.Collections.Generic;
using Kellojo.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Kellojo.UI {
    public class CraftableItemDisplay : MonoBehaviour {

        [SerializeField] GridLayoutGroup Grid;
        [SerializeField] GameObject ItemTilePrefab;


        private void Awake() {
            UpdateInventory();
        }

        void UpdateInventory() {
            foreach (Transform child in Grid.transform) {
                Destroy(child.gameObject);
            }

            foreach (Item item in Item.GetAllItems()) {
                GameObject itemTile = Instantiate(ItemTilePrefab, Grid.transform);
                ItemTile tile = itemTile.GetComponent<ItemTile>();
                tile.SetItem(item, 0);

            }
        }


    }
}