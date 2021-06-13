using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kellojo.SaveSystem {
    public class SaveableEntity : MonoBehaviour {
        [SerializeField] private string id = string.Empty;

        public string Id => id;

        public void TryCreateId() {
            if (id == string.Empty) {
                GenerateId();
            }
        }

        [ContextMenu("Generate Save ID")]
        void GenerateId() {
            id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Captures the state of the entity
        /// </summary>
        /// <returns></returns>
        public object CaptureState() {
            var state = new Dictionary<string, object>();
            foreach(var saveable in GetComponents<ISaveable>()) {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }

            return state;
        }

        /// <summary>
        /// Restores the state of the entity
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public object RestoreState(object state) {
            var dictionary = (Dictionary<string, object>) state;
            foreach (var saveable in GetComponents<ISaveable>()) {

                if (dictionary.TryGetValue(saveable.GetType().ToString(), out object value)) {
                    saveable.RestoreState(value);
                }
            }

            return state;
        }
    }
}


