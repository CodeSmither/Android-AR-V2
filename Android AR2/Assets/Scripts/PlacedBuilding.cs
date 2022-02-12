using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedBuilding : MonoBehaviour
{
    public static PlacedBuilding Construct(Vector3 worldPosition, Vector2Int origin, BuildingTypeSO.Dir dir, BuildingTypeSO buildingTypeSO)
    {
        Transform placedBuildingTransform = Instantiate(buildingTypeSO.prefab, worldPosition, Quaternion.Euler(0, buildingTypeSO.GetRotationAngle(dir), 0));

        PlacedBuilding placebuilding = placedBuildingTransform.GetComponent<PlacedBuilding>();

        placebuilding.buildingTypeSO = buildingTypeSO;
        placebuilding.buildingorigin = origin;
        placebuilding.dir = dir;

        return placebuilding;
    }

    private BuildingTypeSO buildingTypeSO;
    private Vector2Int buildingorigin;
    private BuildingTypeSO.Dir dir;

    public List<Vector2Int> GetGridPositionList()
    {
        return buildingTypeSO.GetGridPositionList(buildingorigin, dir);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public override string ToString()
    {
        return buildingTypeSO.nameString;
    }
}
