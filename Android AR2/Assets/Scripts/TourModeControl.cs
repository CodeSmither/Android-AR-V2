using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourModeControl : MonoBehaviour
{
    private GameObject Canvas;
    private GameObject TouringCamera;
    private GameObject TouringCanvas;
    private GameObject BuildingPreview;

    private void Awake()
    {
        BuildingPreview = GameObject.Find("BuildingPreview");
        Canvas = GameObject.Find("Canvas");
        TouringCamera = GameObject.Find("TouringCamera");
        TouringCanvas = GameObject.Find("TouringCanvas");
        Invoke("CanvasDeactivation",0.1f);
    }
    // disables touring canvas just after the game starts so other objects can gain references of it
    private void CanvasDeactivation()
    {
        TouringCamera.SetActive(false);
        TouringCanvas.SetActive(false);
    }
    // enables the tour mode 
    public void EnableTour()
    {
        Canvas.SetActive(false);
        TouringCamera.SetActive(true);
        TouringCanvas.SetActive(true);
        BuildingPreview.SetActive(false);

    }
    // disables the tour mode
    public void DisableTour()
    {
        Canvas.SetActive(true);
        TouringCamera.SetActive(false);
        TouringCanvas.SetActive(false);
        BuildingPreview.SetActive(true);
    }
}
