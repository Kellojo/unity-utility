using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Kellojo.Utility {
    public static class Math {


        /// <summary>
        /// Get's a point on a circle at a given angle
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector2 PointOnCircle(float radius, float angle) {
            return new Vector2(radius * Mathf.Cos(angle * Mathf.PI / 180), radius * Mathf.Sin(angle * Mathf.PI / 180));
        }

        /// <summary>
        /// Get's the angle for a child on a circle
        /// </summary>
        /// <param name="childIndex"></param>
        /// <param name="childCount"></param>
        /// <param name="maxAngle"></param>
        /// <param name="startAngle"></param>
        /// <returns></returns>
        public static float GetAngleForPoinOnCircle(int childIndex, int childCount, float maxAngle = 360, float startAngle = 0) {
            float alignmentAngle = -maxAngle / 2;
            return alignmentAngle + startAngle + maxAngle / childCount * (childIndex + 0.5f);
        }
    }
}
