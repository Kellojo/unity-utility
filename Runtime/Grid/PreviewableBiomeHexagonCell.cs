using Kellojo.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kellojo.Utility;
using Zenject;

namespace Kellojo.Grid {
    public class PreviewableBiomeHexagonCell : PreviewableHexagonCell {
        protected Biome Biome;
        MeshRenderer meshRenderer;

        [Header("Spawnables")]
        [SerializeField] protected List<Biome> Biomes;
        [SerializeField, Layer] protected int SpawnableLayer;
        [Inject] GrassRenderer GrassRenderer;

        protected void Awake() {
            gameObject.AddComponent<ZenAutoInjecter>();
        }


        protected void Start() {
            meshRenderer = GetComponent<MeshRenderer>();
            Biome = Biomes.PickRandom();
            PopulateHexagon();

            base.Start();
        }

        void PopulateHexagon() {
            meshRenderer.sharedMaterial = Biome.Ground;
            Biome.InstantiateParticles(transform);

            if (Biome.GrassConfig.RenderGrass) {
                GrassRenderer.AddRenderingConfig(Biome.GrassConfig, GrassRenderer.GeneratePositionsForHexagon(Biome.GrassConfig, transform.position, coordinates.innerRadius));
            }
            

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
