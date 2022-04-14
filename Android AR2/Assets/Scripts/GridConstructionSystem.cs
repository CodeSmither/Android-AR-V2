using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Ulility;
//Inspired by https://www.youtube.com/watch?v=dulosHPl82A&ab_channel=CodeMonkey
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
    public int CurrentBuilding;

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
        // checks the gameobject in a current base
        public PlacedBuilding GetPlacedBuilding()
        {
            return placedBuilding;
        }
        //Clears GameObject In a space
        public void ClearPlacedBuilding()
        {
            placedBuilding = null;
            grid.TriggerGridObjectChanged(x, z);
        }
        //checks if an object space is aviable
        public bool CanBuild()
        {
            return placedBuilding == null;
        }
        
    }
    private void Update()
    {   // checks if the construct button has been placed 
        if (construct == true)
        {   // then deactivates construct to prevent the function from activating more than once per press
            construct = false;
            // then it gets the users touch position and converts it into a grid position
            grid.GetXZ(DptMouse.GetTouchWorldPosition(), out int x, out int z);
            
            Vector2Int BuildingOrigin = new Vector2Int(x, z);

            // stores the objects grid position onto a list so that it doesn't overlap other objects
            List<Vector2Int> gridPositionList = buildingTypeSO.GetGridPositionList(new Vector2Int(x, z), dir);
            // then checks if the object does overlap with the list 
            bool canBuild = true;
            // checks if each grid position and checks if any the named co-ordiantes on the object overlap the object trying to be placed
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                {
                    canBuild = false;
                    break;
                }
            }
            // if can build stays true as there were no overlaps it then declares that the game object can be placed and palces it the write position taking in the previously calculated variables
            if (canBuild == true)
            {
                
                Vector2Int rotationOffset = buildingTypeSO.GetRotationOffset(dir);
                Vector3 buildingWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
                PlacedBuilding placedBuilding = PlacedBuilding.Construct(buildingWorldPosition, new Vector2Int(x, z), dir, buildingTypeSO);


                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedBuilding(placedBuilding);
                }
            }
            // else it will declare text is overlapping
            else
            {
                Utility.CreateWorldTextPopup("Buildings are Currently Overlapping", DptMouse.GetTouchWorldPosition());
            }
        }
        // this will rotate the object by going through the list of enums to the next to rotate it 90 degrees without need for a referancea
        if (rotate == true)
        {
            dir = BuildingTypeSO.GetNextDir(dir);
            // notifies the player which direction the are rotating the object to
            Utility.CreateWorldTextPopup("" + dir, DptMouse.GetTouchWorldPosition());

            rotate = false;
        }
        // checks if the object space is occupied when the button is pressed and then removes the object from the space as well as the stored list
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
    // this converts the touch of the player and changes it into 
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
    // this checks the rotation of a player and then converts it into a
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
    // checks the type of object placed by the player
    public BuildingTypeSO GetPlacedObjectTypeSO()
    {
        return buildingTypeSO;
    }

    
    // checks the players 
    private void RefreshSelectedObjectType()
    {
        OnSelectedChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Confirm() { construct = true;}
    public void Rotate() { rotate = true;}
    public void Delete() { demolish = true;}

    public void CurrentBuilding1() { buildingTypeSO = buildingTypeSOList[0]; RefreshSelectedObjectType(); }
    public void CurrentBuilding2() { buildingTypeSO = buildingTypeSOList[1]; RefreshSelectedObjectType(); }
    public void CurrentBuilding3() { buildingTypeSO = buildingTypeSOList[2]; RefreshSelectedObjectType(); }
    public void CurrentBuilding4() { buildingTypeSO = buildingTypeSOList[3]; RefreshSelectedObjectType(); }
    public void CurrentBuilding5() { buildingTypeSO = buildingTypeSOList[4]; RefreshSelectedObjectType(); }
    public void CurrentBuilding6() { buildingTypeSO = buildingTypeSOList[5]; RefreshSelectedObjectType(); }
    public void CurrentBuilding7() { buildingTypeSO = buildingTypeSOList[6]; RefreshSelectedObjectType(); }


}
