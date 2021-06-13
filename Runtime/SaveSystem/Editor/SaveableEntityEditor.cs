using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Kellojo.SaveSystem.Editor {

    [CustomEditor(typeof(SaveableEntity))]
    [CanEditMultipleObjects]
    public class SaveableEntityEditor : UnityEditor.Editor {

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            var entity = (SaveableEntity)target;
            entity.TryCreateId();
        }

    }
}

