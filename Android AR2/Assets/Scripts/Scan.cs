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
    

    
    private void Awake()
    {
        ARSession = GameObject.Find("AR Session Origin");
        ARSession.GetComponent<ARTrackedImageManager>().enabled = false;
        ARSession.GetComponent<ImageTracking>().enabled = false;
        ScanCanvas.SetActive(false);
    }
    
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
}
