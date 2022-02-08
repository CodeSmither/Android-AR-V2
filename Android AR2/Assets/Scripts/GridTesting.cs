using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Ulility;

public class GridTesting : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Grid grid;
    [SerializeField] private GameObject cursor;
    private void Start()
    {
        grid = new Grid(4, 2 , 10f);
    }

    private void Update()
    {
        Vector3 touchWorldPosition = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
        touchWorldPosition.z = 0f;
        gameObject.transform.position = touchWorldPosition;

        if(Input.touches.Length > 0)
        {
            grid.SetValue(cursor.transform.position), 56);
        }
    }
}
