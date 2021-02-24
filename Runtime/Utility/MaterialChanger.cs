using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Utility {

    public class MaterialChanger {
        private GameObject gameObject;
        private Dictionary<MeshRenderer, Material> defaultMaterials = new Dictionary<MeshRenderer, Material>();

        public MaterialChanger(GameObject gameObject) {
            this.gameObject = gameObject;

            StoreDefaultMaterials();
        }

        private void StoreDefaultMaterials() {
            defaultMaterials.Clear();
            MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in meshRenderers) {
                defaultMaterials.Add(mr, mr.material);
            }
        }

        public void SetMaterialOnAllMeshRenderers(Material material) {
            foreach (MeshRenderer mr in defaultMaterials.Keys) {
                mr.material = material;
            }
        }

        public void RestoreDefaultMaterials() {
            foreach (MeshRenderer mr in defaultMaterials.Keys) {
                mr.material = defaultMaterials[mr];
            }
        }
    }
}