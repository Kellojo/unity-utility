using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kellojo.Building {

    [CreateAssetMenu(menuName = "Kellojo/Building/Building Type")]
    public class BuildingType : ScriptableObject {

        [SerializeField] GameObject Prefab;
        [SerializeField] EBuildingMode BuildingMode = EBuildingMode.Default;

        public GameObject Instantiate() {
            return Instantiate(Prefab);
        }
        public EBuildingMode GetBuildingMode() { return BuildingMode; }

        public Collider GetCollider() {
            return Prefab.GetComponent<Collider>();
        }
    }

}

