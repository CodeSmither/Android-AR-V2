using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Ulility;

public class GridConstructionSystem : MonoBehaviour
{
    public static GridConstructionSystem Instance { get; private set; }

    public event EventHandler OnSelectedChanged;


    [SerializeField] private List<BuildingTypeSO> buildingTypeSOList;
    [SerializeField] private BuildingTypeSO buildingTypeSO;
    private GridXZ<GridObject> grid;
    private BuildingTypeSO.Dir dir = BuildingTypeSO.Dir.Down;

    public bool construct;
    public bool rotate;
    public bool demolish;

    private void Awake()
    {
        Instance = this;

        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 10f / 33f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3((-gridWidth * (gridWidth / 2))/33.333f, -1.14f, ((-gridHeight * (gridHeight / 2))/33.333f)+ 2.47f), (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));

        buildingTypeSO = buildingTypeSOList[0];
    }
    public class GridObject
    {
        private GridXZ<GridObject> grid;
        private int x;
        private int z;
        private PlacedBuilding placedBuilding;

        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public void SetPlacedBuilding(PlacedBuilding placedBuilding)
        {
            this.placedBuilding = placedBuilding;
            grid.TriggerGridObjectChanged(x, z);
        }

        public PlacedBuilding GetPlacedBuilding()
        {
            return placedBuilding;
        }

        public void ClearPlacedBuilding()
        {
            placedBuilding = null;
            grid.TriggerGridObjectChanged(x, z);
        }

        public bool CanBuild()
        {
            return placedBuilding == null;
        }

        public override string ToString()
        {
            return x + ", " + z + "\n" + placedBuilding;
        }
    }
    private void Update()
    {
        if (construct == true)
        {
            grid.GetXZ(DptMouse.GetTouchWorldPosition(), out int x, out int z);

            List<Vector2Int> gridPositionList = buildingTypeSO.GetGridPositionList(new Vector2Int(x, z), dir);

            bool canBuild = true;
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                {
                    canBuild = false;
                    break;
                }
            }

            if (canBuild)
            {
                Vector2Int rotationOffset = buildingTypeSO.GetRotationOffset(dir);
                Vector3 buildingWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
                PlacedBuilding placedBuilding = PlacedBuilding.Construct(buildingWorldPosition, new Vector2Int(x, z), dir, buildingTypeSO);


                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedBuilding(placedBuilding);
                }
                construct = false;
            }
            else
            {
                Utility.CreateWorldTextPopup("Buildings are Currently Overlapping", DptMouse.GetTouchWorldPosition());
                construct = false;
            }
        }

        if (rotate == true)
        {
            dir = BuildingTypeSO.GetNextDir(dir);
            Utility.CreateWorldTextPopup("" + dir, DptMouse.GetTouchWorldPosition());
            rotate = false;
        }

        if (demolish == true)
        {
            GridObject gridObject = grid.GetGridObject(DptMouse.GetTouchWorldPosition());
            PlacedBuilding placedBuilding = gridObject.GetPlacedBuilding();
            if (placedBuilding != null)
            {
                placedBuilding.DestroySelf();

                List<Vector2Int> gridPositionList = placedBuilding.GetGridPositionList();

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedBuilding();
                }
            }
            demolish = false;
        }

        
    }

    public Vector3 GetMouseWorldSnappedPosition()
    {
        Vector3 mousePosition = DptMouse.GetTouchWorldPosition();
        grid.GetXZ(mousePosition, out int x, out int z);

        if (buildingTypeSO != null)
        {
            Vector2Int rotationOffset = buildingTypeSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        }
        else
        {
            return mousePosition;
        }
    }

    public Quaternion GetPlacedObjectRotation()
    {
        if (buildingTypeSO != null)
        {
            return Quaternion.Euler(0, buildingTypeSO.GetRotationAngle(dir), 0);
        }
        else
        {
            return Quaternion.identity;
        }
    }

    public BuildingTypeSO GetPlacedObjectTypeSO()
    {
        return buildingTypeSO;
    }

    public void Confirm() { construct = true;}
    public void Rotate() { rotate = true;}
    public void Delete() { demolish = true;}

    public void Building1() { buildingTypeSO = buildingTypeSOList[0]; }
    public void Building2() { buildingTypeSO = buildingTypeSOList[1]; }
    public void Building3() { buildingTypeSO = buildingTypeSOList[2]; }
    public void Building4() { buildingTypeSO = buildingTypeSOList[3]; }
    public void Building5() { buildingTypeSO = buildingTypeSOList[4]; }
}
