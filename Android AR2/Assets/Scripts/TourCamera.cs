using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourCamera : MonoBehaviour
{
    private Camera ArCamera;
    private Camera TouringCamera;
    [SerializeField]private int x;
    [SerializeField]private int z;
    [SerializeField] private RectTransform Canvas;
    [SerializeField]private RectTransform NorthButton;
    [SerializeField]private RectTransform SouthButton;
    [SerializeField]private RectTransform WestButton;
    [SerializeField]private RectTransform EastButton;

    private void Awake()
    {
        
        ArCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
        TouringCamera = GameObject.Find("TouringCamera").GetComponent<Camera>();
        Canvas = GameObject.Find("TouringCanvas").GetComponent<RectTransform>();
        z = 5;
        x = 5;
        NorthButton = GameObject.Find("North").GetComponent<RectTransform>();
        SouthButton = GameObject.Find("South").GetComponent<RectTransform>();
        WestButton = GameObject.Find("West").GetComponent<RectTransform>();
        EastButton = GameObject.Find("East").GetComponent<RectTransform>();
        
    }

    private void FixedUpdate()
    {
        
        
        NorthButton.localRotation = Quaternion.Euler(new Vector3 (0, 0, 0));
        SouthButton.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        EastButton.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        WestButton.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

    }

    public void MoveForward() {
        if (z < 10 && z > -1) {
            TouringCamera.gameObject.transform.position += new Vector3(0, 0, 0.3f);
            z += 1;
        }
    }
    public void MoveBackward() {
        if (z < 11 && z > 0) { 
            TouringCamera.gameObject.transform.position += new Vector3(0, 0, -0.3f); 
            z -= 1; 
        } 
    }
    public void MoveRight() {
        if (x < 11 && x > 0) { 
            TouringCamera.gameObject.transform.position += new Vector3(0.3f, 0, 0); 
            x -= 1; } 
    }
    public void MoveLeft() {
        if (x < 10 && x > -1) { TouringCamera.gameObject.transform.position += new Vector3(-0.3f, 0, 0); 
            x -= -1; } 
    }
}
