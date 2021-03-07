using Kellojo.Utility;
using SplineMesh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kellojo.Building {
    public class SegmentedBuilding : Building, IBuildable {

        [SerializeField] GameObject SegmentPrefab;
        [SerializeField] protected Spline Spline;
        [SerializeField] SplineExtrusion SplineExtruder;

        private void Awake() {
            SplineExtruder.enabled = false;
        }

        public void OnStartBuilding() { }
        public virtual void OnBuildingPlaced() { }
        public void OnSegmentPlaced(GameObject segment, int index) {
            Transform splineTarget = segment.GetComponent<TransformTarget>().Target;
            Vector3 position = segment.transform.localPosition + splineTarget.localPosition;
            Vector3 direction = segment.transform.localPosition + splineTarget.localPosition + transform.InverseTransformDirection(splineTarget.forward);

            List<SplineNode> nodes = Spline.nodes;
            if (index == 0) {
                nodes[index].Position = Vector3.zero + splineTarget.localPosition;
                nodes[index].Direction = direction;
                return;
            }

            SplineExtruder.enabled = index > 0;

            if (index < nodes.Count) {
                nodes[index].Position = position;
                nodes[index].Direction = direction;
            } else {
                Spline.AddNode(new SplineNode(position, direction));
            }
        }

        public GameObject RequestNewSegment() {
            GameObject segment = Instantiate(SegmentPrefab);
            segment.transform.SetParent(transform);
            return segment;
        }
    }
}
 

