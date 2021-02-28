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

    }
}

