using System.Collections;
using System.Collections.Generic;
using Kellojo.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Kellojo.UI {
    public class InventoryDisplay : MonoBehaviour, IDropHandler {

        [SerializeField] GridLayoutGroup Grid;
        [SerializeField] GameObject ItemTilePrefab;
        [SerializeField] RectTransform DragParent;
        [SerializeField] Canvas DragScaleProvider;
        Inventory inventory;

        List<ItemTile> tiles = new List<ItemTile>();


        public void SetInventory(Inventory inventory) {
            if (inventory != null) {
                inventory.OnChange.RemoveListener(UpdateInventory);
            }

            this.inventory = inventory;
            inventory.OnChange.AddListener(UpdateInventory);

            foreach (Transform child in Grid.transform) {
                Destroy(child.gameObject);
            }
            tiles = new List<ItemTile>();

            UpdateInventory();
        }

        void UpdateInventory() {
            List<Item> distincsItems = inventory.DistincsItems;
            List<ItemTile> toBeDestroyed = new List<ItemTile>();

            foreach (ItemTile tile in tiles) {
                if (!inventory.DistincsItems.Contains(tile.Item)) {
                    toBeDestroyed.Add(tile);
                }
            }

            toBeDestroyed.ForEach(tile => { tiles.Remove(tile); Destroy(tile.gameObject); });

            foreach (Item item in inventory.Items) {
                ItemTile matchingTile = tiles.Find(tile => tile.Item == item);

                if (matchingTile != null) {
                    matchingTile.UpdateAmount(inventory.GetItemAmount(item));
                } else {
                    CreateTimeForItem(item);
                }
            }
        }

        void CreateTimeForItem(Item item) {
            GameObject tile = Instantiate(ItemTilePrefab, Grid.transform);
            ItemTile itemTile = tile.GetComponent<ItemTile>();
            itemTile.SetItem(item, inventory.GetItemAmount(item), inventory, DragScaleProvider, DragParent);
            tiles.Add(itemTile);
        }


        public void OnDrop(PointerEventData eventData) {
            GameObject obj = eventData.pointerDrag;
            ItemTile itemTile = obj.GetComponent<ItemTile>();
            if (itemTile == null) {
                return;
            }

            Inventory source = itemTile.Inventory;
            int amount = source.GetItemAmount(itemTile.Item);

            if (itemTile != null && source.MoveItemsTo(itemTile.Item, amount, inventory)) {
                Destroy(obj);
            }
        }
    }

}
