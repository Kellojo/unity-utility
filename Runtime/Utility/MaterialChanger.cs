using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kellojo.Utility {

    public class MaterialChanger {
        private GameObject gameObject;
        private Dictionary<MeshRenderer, Material[]> defaultMaterials = new Dictionary<MeshRenderer, Material[]>();

        public MaterialChanger(GameObject gameObject) {
            this.gameObject = gameObject;

            StoreDefaultMaterials();
        }

        /// <summary>
        /// Stores the default materials on all mesh renderers
        /// </summary>
        private void StoreDefaultMaterials() {
            defaultMaterials.Clear();
            MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>(true);
            foreach (MeshRenderer mr in meshRenderers) {
                defaultMaterials.Add(mr, mr.sharedMaterials);
            }
        }

        /// <summary>
        /// Set's a material on all mesh renderers
        /// </summary>
        /// <param name="material"></param>
        public void SetMaterialOnAllMeshRenderers(Material material) {
            foreach (MeshRenderer mr in defaultMaterials.Keys) {
                mr.materials = Enumerable.Repeat(material, mr.materials.Length).ToArray(); ;
            }
        }

        /// <summary>
        /// restores the default material on all mesh renderers
        /// </summary>
        public void RestoreDefaultMaterials() {
            foreach (MeshRenderer mr in defaultMaterials.Keys) {
                mr.sharedMaterials = defaultMaterials[mr];
            }
        }
    }
}