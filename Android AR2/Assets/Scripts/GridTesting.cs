using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Ulility;
using UnityEngine.UI;

public class GridTesting : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Grid grid;
    [SerializeField] private GameObject cursor;
    [SerializeField] private Text ObjTouch;
    private void Start()
    {
        grid = new Grid(4, 2 , 10f);
    }

    private void Update()
    {
        if(Input.touches.Length > 0)
        {
            Vector3 touchWorldPosition = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchWorldPosition.z = 0f;
            gameObject.transform.position = touchWorldPosition;
            
            int xRound = Mathf.RoundToInt(cursor.transform.position.x);
            int yRound = Mathf.RoundToInt(cursor.transform.position.y);
            grid.SetValue(xRound, yRound, 56);
        }
        ObjTouch.text = Input.touches.Length.ToString();
    }
}
