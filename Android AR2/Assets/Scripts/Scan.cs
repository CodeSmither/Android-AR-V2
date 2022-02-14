using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    [SerializeField]private GameObject ScanCanvas;
    [SerializeField]private GameObject NormalCanvas;

    private void Start()
    {
        NormalCanvas = GameObject.Find("Canvas");
        ScanCanvas = GameObject.Find("ScanCanvas");
        ScanCanvas.SetActive(false);
    }

    public void EnableScan()
    {
        Object[] gameObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        foreach(GameObject Objects in gameObjects)
        {
            
        }
    }
    public void DisableScan()
    {
        ScanCanvas.SetActive(false);
        NormalCanvas.SetActive(true);
    }
}
