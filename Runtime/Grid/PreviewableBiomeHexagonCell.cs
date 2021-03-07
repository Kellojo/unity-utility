using Kellojo.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kellojo.Utility;


namespace Kellojo.Grid {
    public class PreviewableBiomeHexagonCell : PreviewableHexagonCell {
        protected Biome Biome;
        MeshRenderer meshRenderer;

        [Header("Spawnables")]
        [SerializeField] protected List<Biome> Biomes;
        [SerializeField, Layer] protected int SpawnableLayer;

        protected void Start() {
            meshRenderer = GetComponent<MeshRenderer>();
            Biome = Biomes.PickRandom();
            PopulateHexagon();

            base.Start();
        }

        void PopulateHexagon() {
            meshRenderer.sharedMaterial = Biome.Ground;
            Biome.InstantiateParticles(transform);

            foreach (Spawnable spawnable in Biome.spawnables) {
                int count = spawnable.GetCount();
                for (int i = 0; i < count; i++) {
                    GameObject obj = spawnable.Instantiate();
                    obj.transform.SetParent(transform);
                    obj.transform.localPosition = coordinates.RandomPosition3DOnHexagon();
                    obj.transform.rotation = Extensions.RandomYRotation();
                    obj.SetLayerRecursively(SpawnableLayer);

                    SpawnableInstance instance = obj.GetComponent<SpawnableInstance>();
                    if (instance != null) {
                        instance.HexagonCell = this;
                    }
                }
            }
        }

    }
}
