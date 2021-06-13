using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kellojo.Utility;
using UnityEngine.Rendering;

namespace Kellojo.Environment {
    public class GrassRenderer : MonoBehaviour {

        List<KeyValuePair<GrassRenderingConfig, Dictionary<GrassRenderingConfig.GrassTypeConfig, Matrix4x4[]>>> renderingConfigs = new List<KeyValuePair<GrassRenderingConfig, Dictionary<GrassRenderingConfig.GrassTypeConfig ,Matrix4x4[]>>>();
        Matrix4x4[] matrix4X4s;
        [SerializeField] ShadowCastingMode ShadowCastingMode = ShadowCastingMode.Off;

        // Update is called once per frame
        void Update() {
            foreach(var keyValue in renderingConfigs) {
                GrassRenderingConfig config = keyValue.Key;
                foreach (var type in config.GrassTypes) {
                    Graphics.DrawMeshInstanced(type.GrassMesh, 0, type.GrassMaterial, keyValue.Value[type], keyValue.Value[type].Length, null, ShadowCastingMode);
                }
            }
        }

        /// <summary>
        /// Adds a config for rendering grass
        /// </summary>
        /// <param name="config"></param>
        /// <param name="matrix4X4s"></param>
        public void AddRenderingConfig(GrassRenderingConfig config, Dictionary<GrassRenderingConfig.GrassTypeConfig, Matrix4x4[]> positionMap) {
            foreach (var type in config.GrassTypes) {
                var keyValuePair = new KeyValuePair<GrassRenderingConfig, Dictionary<GrassRenderingConfig.GrassTypeConfig, Matrix4x4[]>>(config, positionMap);
                renderingConfigs.Add(keyValuePair);
            }
        }

        public static Matrix4x4[] GeneratePositionsForHexagon(GrassRenderingConfig.GrassTypeConfig config, MeshCollider meshCollider) {
            Matrix4x4[] matrix4X4s = new Matrix4x4[config.Count];

            for (int i = 0; i < config.Count; i++) {
                Vector3 position = meshCollider.RandomPointOnSurface();
                position.y = 0;
                Vector3 scale = Vector3.one * Random.Range(config.MinScale, config.MaxScale);
                matrix4X4s[i] = Matrix4x4.TRS(position, Extensions.RandomYRotation(), scale);
            }

            return matrix4X4s;
        }
    }
}