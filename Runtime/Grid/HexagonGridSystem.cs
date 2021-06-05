using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kellojo.Grid {
    public class HexagonGridSystem : MonoBehaviour {

        [SerializeField] protected HexagonCell CellPrefab;
        [SerializeField] protected List<HexagonCell> cells;


        protected virtual void Awake() {
            cells = new List<HexagonCell>();
        }

        /// <summary>
        /// Creates a hexagon cell in the grid
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <param name="i"></param>
        public HexagonCell CreateCell(int x, int z) {
            HexagonCell cell = Instantiate(CellPrefab);
            cell.GridSystem = this;
            cell.coordinates = new HexagonCoords(x, z);
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = cell.coordinates.Position3D();
            cell.gameObject.name = "Cell " + cell.coordinates.ToString();
            cells.Add(cell);
            return cell;
        }

        /// <summary>
        /// Checks, if at the given coordinate a cell already exists
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public bool IsCoordinateOccupiedByCell(HexagonCoords coordinates) {
            foreach (HexagonCell cell in cells) {
                if (cell.coordinates == coordinates) {
                    return true;
                }
            }

            return false;
        }
    }
}


