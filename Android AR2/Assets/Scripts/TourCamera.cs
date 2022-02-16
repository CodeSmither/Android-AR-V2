using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourCamera : MonoBehaviour
{
    private Camera ArCamera;
    private Camera TouringCamera;
    private int x;
    private int z;
    private RectTransform NorthButton;
    private RectTransform SouthButton;
    private RectTransform WestButton;
    private RectTransform EastButton;
    private RectTransform Centre;

    private void Start()
    {
        ArCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
        TouringCamera = GameObject.Find("TouringCamera").GetComponent<Camera>();
        z = 6;
        x = 6;
        NorthButton = GameObject.Find("North").GetComponent<RectTransform>();
        SouthButton = GameObject.Find("South").GetComponent<RectTransform>();
        WestButton = GameObject.Find("West").GetComponent<RectTransform>();
        EastButton = GameObject.Find("East").GetComponent<RectTransform>();
        Centre = GameObject.Find("Centre").GetComponent<RectTransform>();
    }

    private void Update()
    {
        TouringCamera.gameObject.transform.rotation = ArCamera.gameObject.transform.rotation;
        Centre.transform.rotation = Quaternion.Euler(new Vector3 (0,0,ArCamera.gameObject.transform.rotation.y));
        NorthButton.transform.rotation = Quaternion.Euler(new Vector3 (0, 0,-ArCamera.gameObject.transform.rotation.y));
        SouthButton.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -ArCamera.gameObject.transform.rotation.y));
        EastButton.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -ArCamera.gameObject.transform.rotation.y));
        WestButton.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -ArCamera.gameObject.transform.rotation.y));
    }

    public void MoveForward() {if(z < 10 && z > -1) TouringCamera.gameObject.transform.position += new Vector3(0, 0, 0.3f); z += 1; }
    public void MoveBackward() { if (z < 11 && z > -1) TouringCamera.gameObject.transform.position += new Vector3(0, 0, -0.3f); z -= 1; }
    public void MoveRight() { if (x < 11 && x > -1) TouringCamera.gameObject.transform.position += new Vector3(0.3f, 0, 0); x -= 1; }
    public void MoveLeft() { if (x < 11 && x > -1) TouringCamera.gameObject.transform.position += new Vector3(-0.3f, 0,  0); x -= -1; }
}
