using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Kellojo.Grid {
    public class ExpandableHexagonGridSystem : HexagonGridSystem {

        protected List<HexagonCell> expansionOptions = new List<HexagonCell>();
        new PreviewableHexagonCell CellPrefab;

        private void Awake() {
            CreateCell(0, 0);

            UpdateExpansionOptions();
        }


        void UpdateExpansionOptions() {

            // remove no longer valid options

            // get new expansion options
            List<HexagonCoords> allOptions = GetExpansionOptions();
            expansionOptions.ForEach(option => {
                allOptions.RemoveAll(item => option.coordinates == item);
            });

            // we now only have new options available
            allOptions.ForEach(option => {
                InstantiateExpansionPreview(option);
            });
        }

        /// <summary>
        /// Get's all expansion options (i.e. surrounding tiles without tiles on them)
        /// </summary>
        /// <returns></returns>
        List<HexagonCoords> GetExpansionOptions() {
            List<HexagonCoords> options = new List<HexagonCoords>();

            cells.ForEach(cell => {

                // exclude preview cells
                PreviewableHexagonCell previewableHexagonCell = (PreviewableHexagonCell)cell;
                if (previewableHexagonCell.IsPreview) {
                    return;
                }

                // get surrounding cells
                IEnumerable<HexagonCoords> neighbours = cell.coordinates.Neighbors();
                foreach (HexagonCoords coordinates in neighbours) {
                    if (!IsCoordinateOccupiedByCell(coordinates)) {
                        options.Add(coordinates);
                    }
                }
            });


            return options;
        }
    
        protected void InstantiateExpansionPreview(HexagonCoords coordinates) {
            //Vector2 offsetCoordinates = coordinates.ToOffsetCoordinates();
            PreviewableHexagonCell cell = (PreviewableHexagonCell)CreateCell(coordinates.q, coordinates.r);
            cell.IsPreview = true;
            cell.OnExitPreview += OnCellExitsPreview;
        }

        void OnCellExitsPreview(PreviewableHexagonCell cell) {
            cell.OnExitPreview -= OnCellExitsPreview;
            UpdateExpansionOptions();
        }


    }
}


