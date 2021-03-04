using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Kellojo.Utility;
using DG.Tweening;
using Kellojo.Building.Modes;

namespace Kellojo.Building {

    public class BuildingManager : MonoBehaviour {

        
        [SerializeField] List<BuildingType> BuildingTypes;
        [SerializeField, Tooltip("Layermask used for finding a valid placement position")] LayerMask PlacementPositionLayerMask;
        [SerializeField, Tooltip("Layermask used for finding object that block building")] LayerMask PlacementCheckLayerMask;
        [SerializeField] Material PreviewMaterialValid;
        [SerializeField] Material PreviewMaterialInvalid;

        [SerializeField, Range(0, 360)] float RotationStep = 45f;

        BuildingModeBase buildingMode;
        new Camera camera;
        int previewLayer;

        protected void Awake() {
            camera = Camera.main;
            previewLayer = LayerMask.NameToLayer("Ignore Raycast");
        }
        protected void Update() {
            buildingMode?.UpdatePreview(camera);
        }


        protected void OnStartBuilding(BuildingType buildingType) {
            buildingMode?.AbortBuilding();

            if (buildingType.GetBuildingMode() == EBuildingMode.Default) {
                buildingMode = new DefaultBuildingMode(buildingType, PreviewMaterialValid, PreviewMaterialInvalid, PlacementPositionLayerMask, PlacementCheckLayerMask, previewLayer);
            } else if (buildingType.GetBuildingMode() == EBuildingMode.SplineBased) {
                buildingMode = new SplineBasedBuildingMode(buildingType, PreviewMaterialValid, PreviewMaterialInvalid, PlacementPositionLayerMask, PlacementCheckLayerMask, previewLayer);
            }

            

            /*Building building = currentBuilding.GetComponent<Building>();
            if (buildingType.GetBuildingMode() == EBuildingMode.SplineBased) {
                SegmentedBuilding segmentedBuilding = (SegmentedBuilding)building;
                currentSegment = segmentedBuilding.RequestNewSegment();
            }*/
        }
        protected void OnRotatePreview() {
            buildingMode?.RotatePreview(RotationStep);
        }
        protected void AbortBuilding() {
            buildingMode?.AbortBuilding();
            buildingMode = null;
        }
        protected void PlaceBuilding() {
            if (buildingMode != null && buildingMode.PlaceBuilding()) {
                buildingMode = null;
            }
        }
        
    }

}

