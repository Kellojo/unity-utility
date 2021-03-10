using Kellojo.Building;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Items {
    public class IOPort : MonoBehaviour, ISnapPoint {

        [SerializeField] protected GameObject indicatorInwards;
        [SerializeField] protected GameObject indicatorOutwards;

        IItemStorage Owner;
        public IItemStorage ConnectedStorage;

        bool isOccupied = false;

        public bool acceptsItems;
        public bool sendsItems;

        public void SetOwner(IItemStorage owner) {
            Owner = owner;
        }

        private void Awake() {
            indicatorInwards.SetActive(acceptsItems);
            indicatorOutwards.SetActive(sendsItems);
        }
        private void Update() {
            if (ConnectedStorage != null && Owner != null) {
                if (sendsItems && Owner.PeekItem() != null) {
                    if (ConnectedStorage.TryStoreItem(Owner.PeekItem())) {
                        Owner?.TryGetItem();
                    }
                }
                if (acceptsItems && ConnectedStorage.PeekItem() != null) {
                    if (Owner.TryStoreItem(ConnectedStorage.PeekItem())) {
                        ConnectedStorage?.TryGetItem();
                    }
                }
            }
        }

        public bool IsOccupied() {
            return isOccupied;
        }

        public void OnConnectBuilding(Building.Building building) {
            IItemStorage storage = building.GetComponent<IItemStorage>();

            if (storage == null) {
                Debug.LogError(string.Format("{0} does not implement IStorage but is attached to an IOPort", building));
            }
            ConnectedStorage = storage;

            isOccupied = true;
        }
    }
}


