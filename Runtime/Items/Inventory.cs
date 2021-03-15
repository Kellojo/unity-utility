using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Items {
    public class Inventory {
        Dictionary<Item, int> Storage = new Dictionary<Item, int>();

        public void AddItem(Item item) {
            if (Storage.ContainsKey(item)) {
                Storage[item]++;
            } else {
                Storage.Add(item, 1);
            }
        }
        public bool RemoveItem(Item item) {
            if (Storage.ContainsKey(item) && Storage[item] > 0) {
                Storage[item] = Storage[item]--;
                return true;
            }

            return false;
        }

        public Dictionary<Item, int>.KeyCollection Items => Storage.Keys;
    }

}
