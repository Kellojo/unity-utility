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
        float snapDistance = .5f;
        bool isPlacedOnSnapPoint;

        List<ISnapPoint> snapPoints;
        ISnapPoint currentSnapPoint;

        public SplineBasedBuildingMode(
            BuildingType BuildingType,
            Material validMaterial,
            Material invalidMaterial,
            LayerMask placementPositionLayerMask,
            LayerMask checkMask,
            int previewLayer
        ) : base(BuildingType, validMaterial, invalidMaterial, placementPositionLayerMask, checkMask, previewLayer) {
            SegmentedBuilding = CurrentBuilding.GetComponent<SegmentedBuilding>();
            snapPoints = new List<ISnapPoint>();
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
            Vector3 mousePosition = GetPlacementPosition(camera, currentSegment.transform.position);

            Transform snapPoint = GetClosestSnapPoint(mousePosition);
            isPlacedOnSnapPoint = snapPoint != null;
            if (snapPoint != null) {
                currentSegment.transform.position = snapPoint.position;
                currentSegment.transform.rotation = snapPoint.rotation;
                currentSnapPoint = snapPoint.GetComponent<ISnapPoint>();
            } else {
                currentSegment.transform.position = mousePosition;
                currentSnapPoint = null;
            }
            
            if (CanSpawnBuilding(currentSegment, currentSegment.transform.position) || snapPoint != null) {
                segmentMaterialChanger.SetMaterialOnAllMeshRenderers(validMaterial);
            } else {
                segmentMaterialChanger.SetMaterialOnAllMeshRenderers(invalidMaterial);
            }
        }
        public override void RotatePreview(float rotationStep) {
            Vector3 target = currentSegment.transform.rotation.eulerAngles;
            target.y += rotationStep;
            currentSegment.transform.DORotate(target, 0.25f).SetEase(Ease.OutCubic);
        }
        public override bool PlaceBuilding() {
            if (isPlacedOnSnapPoint) {
                snapPoints.Add(currentSnapPoint);
            }

            // if this is not the first segment, complete the placement
            if (index > 0 && isPlacedOnSnapPoint) {
                SegmentedBuilding.OnSegmentPlaced(currentSegment, index);
                snapPoints.ForEach(snapPoint => snapPoint.OnConnectBuilding(SegmentedBuilding));
                SegmentedBuilding.OnBuildingPlaced();
                return true;
            } else {
                GetNextSegment();
            }
            
            return false;
        }

        protected Transform GetClosestSnapPoint(Vector3 position) {
            Collider[] possibleSnapPoints = Physics.OverlapSphere(position, snapDistance);

            foreach(Collider coll in possibleSnapPoints) {
                ISnapPoint snapPoint = coll.GetComponent<ISnapPoint>();
                if (snapPoint != null && !snapPoint.IsOccupied()) {
                    return coll.transform;
                }
            }

            return null;
        }
    }
}


