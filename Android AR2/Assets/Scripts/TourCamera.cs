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
    

    private void Awake()
    {
        
        ArCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
        TouringCamera = GameObject.Find("TouringCamera").GetComponent<Camera>();
        Canvas = GameObject.Find("TouringCanvas").GetComponent<RectTransform>();
        z = 5;
        x = 5;
        
    }

    private void FixedUpdate()
    {
        // ensures the rotation of the two cameras is the same.
        TouringCamera.transform.rotation = ArCamera.transform.rotation;
    }


    //controls players movement in tour mode and prevents them moving outside of the bounds of the grid
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
