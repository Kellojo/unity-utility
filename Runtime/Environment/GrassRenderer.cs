using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kellojo.Utility;
using UnityEngine.Rendering;

namespace Kellojo.Environment {
    public class GrassRenderer : MonoBehaviour {

        List<KeyValuePair<GrassRenderingConfig, Matrix4x4[]>> renderingConfigs = new List<KeyValuePair<GrassRenderingConfig, Matrix4x4[]>>();
        Matrix4x4[] matrix4X4s;
        [SerializeField] ShadowCastingMode ShadowCastingMode = ShadowCastingMode.Off;

        // Update is called once per frame
        void Update() {

            foreach(KeyValuePair<GrassRenderingConfig, Matrix4x4[]> keyValue in renderingConfigs) {
                Graphics.DrawMeshInstanced(keyValue.Key.GrassMesh, 0, keyValue.Key.GrassMaterial, keyValue.Value, keyValue.Value.Length, null, ShadowCastingMode);
            }
        }

        /// <summary>
        /// Adds a config for rendering grass
        /// </summary>
        /// <param name="config"></param>
        /// <param name="matrix4X4s"></param>
        public void AddRenderingConfig(GrassRenderingConfig config, Matrix4x4[] matrix4X4s) {
            renderingConfigs.Add(new KeyValuePair<GrassRenderingConfig, Matrix4x4[]>(config, matrix4X4s));
        }

        public static Matrix4x4[] GeneratePositionsForHexagon(GrassRenderingConfig config, Vector3 positionOffset, float radius) {
            Matrix4x4[] matrix4X4s = new Matrix4x4[config.Count];

            for (int i = 0; i < config.Count; i++) {
                Vector2 circlePos = Random.insideUnitCircle;
                Vector3 position = new Vector3(circlePos.x, 0, circlePos.y) * radius;
                position += positionOffset;
                Vector3 scale = Vector3.one * Random.Range(config.MinScale, config.MaxScale);

                matrix4X4s[i] = Matrix4x4.TRS(position, Extensions.RandomYRotation(), scale);
            }

            return matrix4X4s;
        }
    }
}