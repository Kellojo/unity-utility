using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Environment {

    [CreateAssetMenu(menuName = "Kellojo/Enrionment/Biome")]
    public class Biome : ScriptableObject {

        public new string name;
        public Material Ground;
        public List<Spawnable> spawnables;
        public GrassRenderingConfig GrassConfig;

        [Header("Particle Settings")]
        [SerializeField] protected Spawnable Particles;
        [SerializeField, Range(0, 1)] protected float ParticleSpawnRate = 0.1f;

        public GameObject InstantiateParticles(Transform parent) {
            float r = Random.Range(0f, 1f);
            if (Particles != null && Particles.Prefab != null && Random.Range(0f, 1f) <= ParticleSpawnRate) {
                GameObject obj = Particles.Instantiate();
                obj.transform.SetParent(parent);
                obj.transform.localPosition = Vector3.zero;
                return obj;
            }

            return null;
        }


        void Update() {

        }

    }

}

