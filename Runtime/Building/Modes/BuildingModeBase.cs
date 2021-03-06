using DG.Tweening;
using Kellojo.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kellojo.Building.Modes {
    public class BuildingModeBase {

        protected GameObject CurrentBuilding;
        protected BuildingType BuildingType;
        protected MaterialChanger materialChanger;
        protected Material validMaterial;
        protected Material invalidMaterial;
        protected LayerMask placementPositionLayerMask;
        protected LayerMask placementCheckLayerMask;
        protected int previewLayer;

        public BuildingModeBase(BuildingType BuildingType, Material validMaterial, Material invalidMaterial, LayerMask positionMask, LayerMask checkMask, int previewLayer) {
            this.BuildingType = BuildingType;
            this.validMaterial = validMaterial;
            this.invalidMaterial = invalidMaterial;
            placementPositionLayerMask = positionMask;
            placementCheckLayerMask = checkMask;
            this.previewLayer = previewLayer;

            CurrentBuilding = BuildingType.Instantiate();
            CurrentBuilding.SetLayerRecursively(previewLayer);
            materialChanger = new MaterialChanger(CurrentBuilding);
        }

        public virtual void UpdatePreview(Camera camera) {
            CurrentBuilding.transform.position = GetPlacementPosition(camera, CurrentBuilding.transform.position);

            // update preview material (valid or not?)
            if (CanSpawnBuilding(CurrentBuilding, CurrentBuilding.transform.position)) {
                materialChanger.SetMaterialOnAllMeshRenderers(validMaterial);
            } else {
                materialChanger.SetMaterialOnAllMeshRenderers(invalidMaterial);
            }
        }
        public virtual void RotatePreview(float rotationStep) {
            Vector3 target = CurrentBuilding.transform.rotation.eulerAngles;
            target.y += rotationStep;
            CurrentBuilding.transform.DORotate(target, 0.25f).SetEase(Ease.OutCubic);
        }
        public virtual bool PlaceBuilding() {
            if (CanSpawnBuilding(CurrentBuilding, CurrentBuilding.transform.position)) {
                CurrentBuilding.SetLayerRecursively(LayerMask.NameToLayer("Default"));
                materialChanger.RestoreDefaultMaterials();

                IBuildable buildable = CurrentBuilding.GetComponent<IBuildable>();
                if (buildable != null) {
                    buildable.OnBuildingPlaced();
                }

                return true;
            }

            return false;
        }
        public virtual void AbortBuilding() {
            Object.Destroy(CurrentBuilding);
        }

        /// <summary>
        /// Can a building be spawned at a given position
        /// </summary>
        /// <param name="buildingType"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public virtual bool CanSpawnBuilding(GameObject building, Vector3 position) {
            Collider collider = building.GetComponent<Collider>();
            Collider[] colliders = Physics.OverlapBox(position, collider.bounds.extents, building.transform.rotation, placementCheckLayerMask);
            return colliders.Length == 0;
        }
        /// <summary>
        /// Get's the current placement position for the given frame
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public virtual Vector3 GetPlacementPosition(Camera camera, Vector3 position) {
            RaycastHit hit = new RaycastHit();
            if (camera.GetMouseWorldPosition(ref hit, placementPositionLayerMask)) {
                return hit.point;
            };

            return position;
        }
    }
}
