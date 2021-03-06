using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Items {

    public interface IItemStorage {

        bool TryStoreItem(Item item);
        Item PeekItem();
        Item TryGetItem();

    }

}


