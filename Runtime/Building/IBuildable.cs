using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kellojo.Building {
    public interface IBuildable {

        /// <summary>
        /// Called, when building begins
        /// </summary>
        void OnStartBuilding();
        /// <summary>
        /// Called, when the building gets placed and leaves the preview.
        /// </summary>
        void OnBuildingPlaced();
        void OnSegmentPlaced(GameObject segment, int index);

    }
}


