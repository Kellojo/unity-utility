using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Kellojo.Utility {

    public static class Extensions {

        /// <summary>
        /// Get's a random enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomEnum<T>() {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        }

        public static T PickRandom<T>(this IEnumerable<T> source) {
            return source.PickRandom(1).Single();
        }
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count) {
            return source.Shuffle().Take(count);
        }
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
            return source.OrderBy(x => Guid.NewGuid());
        }

        /// <summary>
        /// Get's the world position of the mouse from a given camera
        /// True, if hit something, false otherwise
        /// </summary>
        /// <param name="camera"></param>
        public static bool GetMouseWorldPosition(this Camera camera, ref RaycastHit hit, LayerMask layerMask, float maxDistance = 9999f) {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out hit, maxDistance, layerMask);
        }

        /// <summary>
        /// Set's the layer recursively on a gameObject
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="layer"></param>
        public static void SetLayerRecursively(this GameObject gameObject, int layer) {
            foreach (Transform trans in gameObject.GetComponentsInChildren<Transform>(true)) {
                trans.gameObject.layer = layer;
            }
        }

        /// <summary>
        /// Get's a quaternion with a random y rotation
        /// </summary>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        public static Quaternion RandomYRotation() {
            return Quaternion.Euler(new Vector3(
                0,
                UnityEngine.Random.Range(0, 360f),  
                0
            ));
        }

        /// <summary>
        /// Rotates the rotation on the y axis for 180 degrees (esentially invertign it's direction)
        /// </summary>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static Quaternion GetInvertedYRotation(this Quaternion rotation) {
            Vector3 angles = rotation.eulerAngles;
            angles.y += 180f;
            return Quaternion.Euler(angles);
        }

    }
}

