using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
            return source.OrderBy(x => UnityEngine.Random.value);
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
        /// Rotates the rotation on the y axis for 180 degrees (esentially inverting it's direction)
        /// </summary>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static Quaternion GetInvertedYRotation(this Quaternion rotation) {
            Vector3 angles = rotation.eulerAngles;
            angles.y += 180f;
            return Quaternion.Euler(angles);
        }


        public static void Show(this CanvasGroup canvasGroup, bool withoutAnimation = false, float duration = 0.25f) {
            canvasGroup.DOFade(1, withoutAnimation ? 0 : duration).SetEase(Ease.OutCubic);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        public static void Hide(this CanvasGroup canvasGroup, bool withoutAnimation = false, float duration = 0.25f) {
            canvasGroup.DOFade(0, withoutAnimation ? 0 : duration).SetEase(Ease.OutCubic);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        public static void Toggle(this CanvasGroup canvasGroup, bool visible) {
            if (visible) {
                Show(canvasGroup);
            } else {
                Hide(canvasGroup);
            }
        }

        /// <summary>
        /// Get's a random point in some bounds
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public static Vector3 RandomPointInBounds(this Bounds bounds) {
            return new Vector3(
                UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
                UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
            );
        }

        /// <summary>
        /// Get's a random point on the surface
        /// </summary>
        /// <param name="meshCollider"></param>
        /// <returns></returns>
        public static Vector3 RandomPointOnSurface(this MeshCollider meshCollider) {
            int iterations = 0;
            while (true) {
                iterations++;

                Vector3 randomPoint = meshCollider.bounds.RandomPointInBounds();
                randomPoint.y += meshCollider.bounds.max.y + 1;
                Ray ray = new Ray(randomPoint, Vector3.down);
                RaycastHit hit;
                if (meshCollider.Raycast(ray, out hit, meshCollider.bounds.max.y + 100)) {
                    return hit.point;
                }
            }
        }


        public static string TimeAgo(this DateTime dateTime) {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60)) {
                result = string.Format("{0} seconds ago", timeSpan.Seconds);
            } else if (timeSpan <= TimeSpan.FromMinutes(60)) {
                result = timeSpan.Minutes > 1 ?
                    String.Format("about {0} minutes ago", timeSpan.Minutes) :
                    "about a minute ago";
            } else if (timeSpan <= TimeSpan.FromHours(24)) {
                result = timeSpan.Hours > 1 ?
                    String.Format("about {0} hours ago", timeSpan.Hours) :
                    "about an hour ago";
            } else if (timeSpan <= TimeSpan.FromDays(30)) {
                result = timeSpan.Days > 1 ?
                    String.Format("about {0} days ago", timeSpan.Days) :
                    "yesterday";
            } else if (timeSpan <= TimeSpan.FromDays(365)) {
                result = timeSpan.Days > 30 ?
                    String.Format("about {0} months ago", timeSpan.Days / 30) :
                    "about a month ago";
            } else {
                result = timeSpan.Days > 365 ?
                    String.Format("about {0} years ago", timeSpan.Days / 365) :
                    "about a year ago";
            }

            return result;
        }

    }
}

