using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSensing : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }



    public void AstablishIdentity(int ThisBuildingID , string currentconnections)
    {
        BuildingPrefab buildingPrefab = new BuildingPrefab(ThisBuildingID);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    
}
