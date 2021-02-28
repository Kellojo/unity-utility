using Kellojo.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kellojo.Utility;


namespace Kellojo.Grid {
    public class PreviewableBiomeHexagonCell : PreviewableHexagonCell {
        protected Biome Biome;

        [Header("Spawnables")]
        [SerializeField] protected List<Biome> Biomes;
        [SerializeField, Layer] protected int SpawnableLayer;

        protected void Start() {
            Biome = Biomes.PickRandom();
            PopulateHexagon();

            base.Start();
        }

        void PopulateHexagon() {
            foreach (Spawnable spawnable in Biome.spawnables) {
                for (int i = 0; i < spawnable.maxCount; i++) {
                    GameObject obj = spawnable.Instantiate();
                    obj.transform.SetParent(transform);
                    obj.transform.localPosition = coordinates.RandomPosition3DOnHexagon();
                    obj.SetLayerRecursively(SpawnableLayer);
                }
            }
        }

    }
}
