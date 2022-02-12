using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DptMouse : MonoBehaviour
{

    public static DptMouse Instance { get; private set; }

    [SerializeField] private LayerMask touchColliderLayerMask = new LayerMask();
    [SerializeField] private Vector3 MemoryPoint;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, touchColliderLayerMask))
            {
                transform.position = raycastHit.point;
            }
        }
        
    }

    public static Vector3 GetTouchWorldPosition() => Instance.GetTouchWorldPosition_Instance();

    private Vector3 GetTouchWorldPosition_Instance()
    {
        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, touchColliderLayerMask))
            {
                MemoryPoint = raycastHit.point;
                return raycastHit.point;
                
            }
            else
            {
                return MemoryPoint;
            }
        }
        else
        {
            return MemoryPoint;
        }
        
    }

}
