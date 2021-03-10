using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Building.Modes {
    public class DefaultBuildingMode : BuildingModeBase {

        public DefaultBuildingMode(
            BuildingType BuildingType,
            Quaternion rotation,
            Material validMaterial,
            Material invalidMaterial,
            LayerMask placementPositionLayerMask,
            LayerMask checkMask,
            int previewLayer
        ) : base(BuildingType, rotation, validMaterial, invalidMaterial, placementPositionLayerMask, checkMask, previewLayer) {

        }

    }
}
