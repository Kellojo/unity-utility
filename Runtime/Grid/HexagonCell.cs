using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kellojo.Grid {
    public class HexagonCell : MonoBehaviour {
        public HexagonCoords coordinates;
        public HexagonGridSystem GridSystem;


        private void OnDrawGizmos() {
#if UNITY_EDITOR
            UnityEditor.Handles.color = Color.black;
            UnityEditor.Handles.Label(transform.position, coordinates.ToString());
#endif
        }
    }

}

