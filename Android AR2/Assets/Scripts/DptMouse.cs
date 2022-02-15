using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DptMouse : MonoBehaviour
{

    public static DptMouse Instance { get; private set; }

    [SerializeField] private LayerMask touchColliderLayerMask = new LayerMask();
    [SerializeField] private LayerMask UIColliderLayerMask = new LayerMask();
    [SerializeField] private Vector3 MemoryPoint;
    [SerializeField] private Scan scan;


    private void Awake()
    {
        Instance = this;
        MemoryPoint = new Vector3(0.303f, 0, 2.727f);
        scan = GameObject.Find("MenuNavigation").GetComponent<Scan>();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && scan.Scanning == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (!Physics.Raycast(ray, out RaycastHit raycastHitUI, 999f, UIColliderLayerMask))
            {
                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, touchColliderLayerMask))
                {
                    transform.position = raycastHit.point;
                }
            }
            
        }
        
    }

    public static Vector3 GetTouchWorldPosition() => Instance.GetTouchWorldPosition_Instance();

    private Vector3 GetTouchWorldPosition_Instance()
    {
        if (Input.touchCount > 0 && scan.Scanning == false)
        {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (!Physics.Raycast(ray, out RaycastHit raycastHitUI, 999f, UIColliderLayerMask))
            {
                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, touchColliderLayerMask))
                {
                    MemoryPoint = raycastHit.point;
                    return raycastHit.point;

                }
                else { return MemoryPoint; }
            }
            else { return MemoryPoint; }
        }
        else { return MemoryPoint; }
        
        
    }

}
