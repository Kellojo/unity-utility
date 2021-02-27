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
    }




}

