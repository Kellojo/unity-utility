using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Kellojo.Utility;
using DG.Tweening;


namespace Kellojo.Building {

    public class BuildingManager : MonoBehaviour {


        
        [SerializeField] List<BuildingType> BuildingTypes;
        [SerializeField] LayerMask PlacementPositionLayerMask;
        [SerializeField] LayerMask PlacementCheckLayerMask;
        [SerializeField] Material PreviewMaterialValid;
        [SerializeField] Material PreviewMaterialInvalid;

        [SerializeField, Range(0, 360)] float RotationStep = 45f;

        GameObject currentBuilding;
        MaterialChanger materialChanger;
        new Camera camera;

        protected void Awake() {
            camera = Camera.main;
        }
        protected void Update() {
            UpdatePreview();
        }


        protected void OnStartBuilding(BuildingType buildingType) {
            currentBuilding = buildingType.Instantiate();
            currentBuilding.SetLayerRecursively(LayerMask.NameToLayer("Ignore Raycast"));
            materialChanger = new MaterialChanger(currentBuilding);
        }
        protected void UpdatePreview() {
            if (currentBuilding == null) {
                return;
            }

            // update position
            RaycastHit hit = new RaycastHit();
            if (camera.GetMouseWorldPosition(ref hit, PlacementPositionLayerMask)) {
                currentBuilding.transform.position = hit.point;
            }

            // update preview material (valid or not?)
            if (CanSpawnBuilding(currentBuilding, currentBuilding.transform.position)) {
                materialChanger.SetMaterialOnAllMeshRenderers(PreviewMaterialValid);
            } else {
                materialChanger.SetMaterialOnAllMeshRenderers(PreviewMaterialInvalid);
            }
        }
        protected void OnRotatePreview() {
            if (currentBuilding == null) {
                return;
            }

            Vector3 target = currentBuilding.transform.rotation.eulerAngles;
            target.y += RotationStep; 
            currentBuilding.transform.DORotate(target, 0.75f).SetEase(Ease.OutElastic);
        }
        protected void AbortBuilding() {
            Destroy(currentBuilding);
            currentBuilding = null;
        }
        protected void PlaceBuilding() {
            if (CanSpawnBuilding(currentBuilding, currentBuilding.transform.position)) {
                currentBuilding.SetLayerRecursively(LayerMask.NameToLayer("Default"));
                materialChanger.RestoreDefaultMaterials();

                materialChanger = null;
                currentBuilding = null;
            }
        }



        /// <summary>
        /// Can a building be spawned at a given position
        /// </summary>
        /// <param name="buildingType"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        protected bool CanSpawnBuilding(GameObject building, Vector3 position) {
            Collider collider = building.GetComponent<Collider>();
            Collider[] colliders = Physics.OverlapBox(position, collider.bounds.extents, building.transform.rotation, PlacementCheckLayerMask);
            return colliders.Length == 0;
        }
        
    }

}

