using SplineMesh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kellojo.Building {
    public class SegmentedBuilding : Building, IBuildable {

        [SerializeField] GameObject SegmentPrefab;
        [SerializeField] Spline Spline;

        public void OnBuildingPlaced() {
            
        }
        public void OnSegmentPlaced(GameObject segment, int index) {
            Vector3 position = segment.transform.localPosition;
            Vector3 direction = segment.transform.localPosition + transform.InverseTransformDirection(segment.transform.forward);

            List<SplineNode> nodes = Spline.nodes;
            if (index == 0) {
                nodes[index].Position = Vector3.zero;
                return;
            }

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
 

