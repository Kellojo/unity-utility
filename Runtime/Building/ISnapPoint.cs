using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Building {
    public interface ISnapPoint {
        bool IsOccupied();
        void OnConnectBuilding(Building building);
    }
}

