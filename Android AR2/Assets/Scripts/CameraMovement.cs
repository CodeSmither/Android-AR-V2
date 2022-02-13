using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject Maincamera;
    [SerializeField] private float Radius;

    // Start is called before the first frame update
    void Start()
    {
        Maincamera = GameObject.FindGameObjectWithTag("MainCamera");
        Radius = 120f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.x != Mathf.Cos(transform.rotation.y) * Radius || transform.position.z != Mathf.Sin(transform.rotation.y) * Radius)
        {
            Maincamera.transform.position = Maincamera.transform.rotation * new Vector3 (0,-10,-Radius);
        }
    }
}
