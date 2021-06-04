using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kellojo.Environment {
    [System.Serializable]
    public class GrassRenderingConfig {

        [SerializeField] public bool RenderGrass = true;
        [SerializeField] public Mesh GrassMesh;
        [SerializeField] public Material GrassMaterial;
        [SerializeField] public int Count = 100;
        [SerializeField] public float MinScale = 0.5f;
        [SerializeField] public float MaxScale = 1f;
    }
}


