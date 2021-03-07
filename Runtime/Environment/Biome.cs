using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Environment {

    [CreateAssetMenu(menuName = "Kellojo/Enrionment/Biome")]
    public class Biome : ScriptableObject {

        public new string name;
        public Material Ground;
        public List<Spawnable> spawnables;
        public Spawnable Particles;

        public GameObject InstantiateParticles(Transform parent) {
            if (Particles != null && Particles.Prefab != null) {
                GameObject obj = Particles.Instantiate();
                obj.transform.SetParent(parent);
                obj.transform.localPosition = Vector3.zero;
                return obj;
            }

            return null;
        }


    }

}

