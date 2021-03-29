using Kellojo.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kellojo.Building {

    [CreateAssetMenu(menuName = "Kellojo/Building/Building Type")]
    public class BuildingType : Item {

        [Header("Building Settings")]
        [SerializeField] GameObject BuildingPrefab;
        [SerializeField] EBuildingMode BuildingMode = EBuildingMode.Default;

        public GameObject Instantiate(Quaternion rotation) {
            GameObject obj = Instantiate(BuildingPrefab);
            obj.transform.rotation = rotation;

            Building building = obj.GetComponent<Building>();
            if (building != null) {
                building.BuildingType = this;
            }

            return obj;
        }
        public EBuildingMode GetBuildingMode() { return BuildingMode; }

        public Collider GetCollider() {
            return BuildingPrefab.GetComponent<Collider>();
        }
    }

}

