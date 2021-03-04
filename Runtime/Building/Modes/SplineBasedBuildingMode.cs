using DG.Tweening;
using Kellojo.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Building.Modes {
    public class SplineBasedBuildingMode : BuildingModeBase {

        protected GameObject currentSegment;
        protected SegmentedBuilding SegmentedBuilding;
        protected MaterialChanger segmentMaterialChanger;
        int index;

        public SplineBasedBuildingMode(
            BuildingType BuildingType,
            Material validMaterial,
            Material invalidMaterial,
            LayerMask placementPositionLayerMask,
            LayerMask checkMask,
            int previewLayer
        ) : base(BuildingType, validMaterial, invalidMaterial, placementPositionLayerMask, checkMask, previewLayer) {
            SegmentedBuilding = CurrentBuilding.GetComponent<SegmentedBuilding>();
            GetNextSegment();
        }

        void GetNextSegment() {
            Quaternion previousRotation = Quaternion.identity;
            if (currentSegment != null) {
                if (index == 0) {
                    Vector3 position = currentSegment.transform.position;
                    CurrentBuilding.transform.position = position;
                    currentSegment.transform.position = position;
                }

                previousRotation = currentSegment.transform.rotation;
                SegmentedBuilding.OnSegmentPlaced(currentSegment, index);
                index++;
            }

            currentSegment = SegmentedBuilding.RequestNewSegment();
            currentSegment.SetLayerRecursively(previewLayer);
            currentSegment.transform.rotation = previousRotation;

            segmentMaterialChanger?.RestoreDefaultMaterials();
            segmentMaterialChanger = new MaterialChanger(currentSegment);
        }

        public override void UpdatePreview(Camera camera) {
            currentSegment.transform.position = GetPlacementPosition(camera, currentSegment.transform.position);
        }
        public override void RotatePreview(float rotationStep) {
            Vector3 target = currentSegment.transform.rotation.eulerAngles;
            target.y += rotationStep;
            currentSegment.transform.DORotate(target, 0.75f).SetEase(Ease.OutElastic);
        }
        public override bool PlaceBuilding() {
            //return base.PlaceBuilding();
            GetNextSegment();
            return false;
        }
    }
}


