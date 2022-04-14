using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Scan : MonoBehaviour
{
    [SerializeField]private GameObject ScanCanvas;
    [SerializeField]private GameObject NormalCanvas;
    public bool Scanning;
    public GameObject ARSession;
    private Object[] gameObjects;
    private MenuNavigation menuNavigation;
    

    
    private void Awake()
    {
        menuNavigation = GameObject.Find("MenuNavigation").GetComponent<MenuNavigation>();
        ARSession = GameObject.Find("AR Session Origin");
        ARSession.GetComponent<ARTrackedImageManager>().enabled = false;
        ARSession.GetComponent<ImageTracking>().enabled = false;
        ScanCanvas.SetActive(false);
    }
    // Enables and disables the scan mode which allow AR textures to be inputted into to the game 
        public void EnableScan()
        {
            Scanning = true;
            gameObjects = FindObjectsOfType(typeof(GameObject));
            foreach(GameObject Objects in gameObjects)
            {
                
                if (Objects.layer != 7 && Objects.layer != 5) { Objects.gameObject.SetActive(true); }
                else if (Objects.layer == 7 || Objects.layer == 5) { Objects.gameObject.SetActive(false); }
                
            }
            ScanCanvas.SetActive(true);
            ARSession.GetComponent<ARTrackedImageManager>().enabled = true;
            ARSession.GetComponent<ImageTracking>().enabled = true;
        }
        public void DisableScan()
        {
            Scanning = false;
            foreach (GameObject Objects in gameObjects)
            {
                if (Objects.layer == 7 || Objects.layer == 5) { Objects.gameObject.SetActive(true); }
            }

            ScanCanvas.SetActive(false);
            ARSession.GetComponent<ARTrackedImageManager>().enabled = false;
            ARSession.GetComponent<ImageTracking>().enabled = false;
        }
    //checks if the prefabs in the scan mode have appeared thus the player has unlocked them within the game
        public void UnlockedItems(string prefabname)
        {
            if (prefabname == "PopcornCartVersion1")
            {
                menuNavigation.PopcornUnlocked = true;
            }

            if (prefabname == "DunkTankFinalVersion")
            {
                menuNavigation.DunkTankUnlocked = true;
            }
            if(prefabname == "BallThrowUpdate2")
            {
                menuNavigation.BumperCarsUnlocked = true;
            }
            if (prefabname == "FishCatch")
            {
                menuNavigation.FishCatchUnlocked = true;
            }
            if (prefabname == "BumperCarsUpdate2")
            {
                menuNavigation.BumperCarsUnlocked = true;
            }
            if (prefabname == "TeaCups")
            {
                menuNavigation.TeaCupsUnlocked = true;
            }
            if (prefabname == "Tent")
            {
                menuNavigation.TentUnlocked = true;
            }
        }
}
