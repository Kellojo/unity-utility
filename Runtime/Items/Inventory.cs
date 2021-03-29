using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Kellojo.Items {
    public class Inventory {
        Dictionary<Item, int> Storage = new Dictionary<Item, int>();
        public UnityEvent OnChange = new UnityEvent();

        public List<Item> DistincsItems {
            get {
                return Storage.Keys.ToList();
            }
        }

        int size = -1;
        int slotLimit = -1;
        public bool IsUnlimitedSize {
            get {
                return size <= 0;
            }
        }
        public bool IsUnlimitedSlots{
            get {
                return slotLimit <= 0;
            }
        }
        public int OccupiedSize {
            get {
                int count = 0;
                foreach (Item item in Storage.Keys) {
                    count += Storage[item];
                }

                return count;
            }
        }
        public int OccupiedSlots {
            get {
                return Storage.Keys.Count;
            }
        }
        public bool IsFull {
            get {
                return !IsUnlimitedSize && OccupiedSize >= size;
            }
        }

        public Inventory(int size = -1, int slotLimit = -1) {
            this.size = size;
            this.slotLimit = slotLimit;
        }

        public bool AddItem(Item item) {
            if (!CanAcceptOneMoreItemOfType(item)) {
                return false;
            }

            if (Storage.ContainsKey(item)) {
                Storage[item]++;
            } else {
                Storage.Add(item, 1);
            }
            OnChange?.Invoke();
            return true;
        }
        public bool AddItem(Item item, int slotIndex) {
            try {
                KeyValuePair<Item, int> slot = Storage.ElementAt(slotIndex);
                if (slot.Key == item && CanAcceptOneMoreItemOfType(item)) {
                    Storage[item] = ++Storage[item];
                    OnChange?.Invoke();
                    return true;
                }
            } catch { }
            return false;
        }
        public bool CanAddItems(Item item, int amount) {
            return (OccupiedSize + amount <= size || IsUnlimitedSize) && (CanAcceptOneMoreItemOfType(item) || IsUnlimitedSlots);
        }
        public bool RemoveItem(Item item) {
            if (Storage.ContainsKey(item) && Storage[item] > 0) {
                Storage[item] = --Storage[item];

                if (Storage[item] == 0) {
                    Storage.Remove(item);
                }

                OnChange?.Invoke();
                return true;
            }

            return false;
        }
        public Item RemoveFirstItem() {
            Item item = PeekItem();
            if (RemoveItem(item)) {
                return item;
            }
            return null;
        }
        public Item RemoveItem(int slotIndex) {
            try {
                KeyValuePair<Item, int> slot = Storage.ElementAt(slotIndex);
                if (slot.Value > 0) {
                    Storage[slot.Key] = --Storage[slot.Key];
                    return slot.Key;
                }
  
            } catch { }

            return null;
        }
        public bool CanRemoveItems(Item item, int amount) {
            return Storage.ContainsKey(item) && Storage[item] >= amount;
        }
        /// <summary>
        /// Moves items from one inventory to another
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool MoveItemsTo(Item item, int amount, Inventory target) {
            if (CanRemoveItems(item, amount) && target.CanAddItems(item, amount)) {
                for(int i = 0; i < amount; i++) {
                    RemoveItem(item);
                }

                for (int i = 0; i < amount; i++) {
                    target.AddItem(item);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Peeks the first item available in the inventory
        /// </summary>
        /// <returns></returns>
        public Item PeekItem() {
            if (Storage.Keys.Count > 0) {

                foreach(Item item in Storage.Keys) {
                    if (Storage[item] > 0) {
                        return item;
                    }
                }
            }

            return null;
        }
        public Item PeekItem(int slotIndex) {
            try {
                KeyValuePair<Item, int> slot = Storage.ElementAt(slotIndex);
                return slot.Key;
            } catch { }

            return null;
        }

        public int GetItemAmount(Item item) {
            return Storage[item];
        }

        /// <summary>
        /// Can the inventory hold one more item of the given type?
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool CanAcceptOneMoreItemOfType(Item item) {
            bool isAlreadyInSlot = Storage.ContainsKey(item);

            return isAlreadyInSlot && (IsUnlimitedSize || OccupiedSize < size) ||
            !isAlreadyInSlot && (IsUnlimitedSlots || OccupiedSlots < slotLimit);
        }

        public Dictionary<Item, int>.KeyCollection Items => Storage.Keys;
    }

}
