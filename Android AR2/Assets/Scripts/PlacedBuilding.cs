using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Inspired by https://www.youtube.com/watch?v=dulosHPl82A&ab_channel=CodeMonkey
public class PlacedBuilding : MonoBehaviour
{
    // Creates a constructer which stores the attributes of a singular placed building
    public static PlacedBuilding Construct(Vector3 worldPosition, Vector2Int origin, BuildingTypeSO.Dir dir, BuildingTypeSO buildingTypeSO)
    {
        Transform placedBuildingTransform = Instantiate(buildingTypeSO.prefab, worldPosition, Quaternion.Euler(0, buildingTypeSO.GetRotationAngle(dir), 0));

        PlacedBuilding placebuilding = placedBuildingTransform.GetComponent<PlacedBuilding>();

        placebuilding.buildingTypeSO = buildingTypeSO;
        placebuilding.buildingorigin = origin;
        placebuilding.dir = dir;

        return placebuilding;
    }
    //stores the attributes collected from the constructer
    private BuildingTypeSO buildingTypeSO;
    private Vector2Int buildingorigin;
    private BuildingTypeSO.Dir dir;
    
    // shows this object position as a ocuppied spot on the grid
    public List<Vector2Int> GetGridPositionList()
    {
        return buildingTypeSO.GetGridPositionList(buildingorigin, dir);
    }
    // removes the gameobject when it is no longer needed.
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    

    
}
