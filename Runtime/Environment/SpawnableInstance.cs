using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kellojo.Grid;


namespace Kellojo.Environment {

    public class SpawnableInstance : MonoBehaviour {
        [HideInInspector] public PreviewableBiomeHexagonCell HexagonCell;
        public bool IsPreview {
            get {
                return HexagonCell.IsPreview;
            }
        }

    }

}

