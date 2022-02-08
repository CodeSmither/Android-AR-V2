using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPrefab : MonoBehaviour
{
    private int buildingID;

    public int BuildingID
    {
        get
        {
            return buildingID;
        }
        set
        {
            buildingID = value;
        }
    }

    public BuildingPrefab(int buildingID)
    {

    }
}
