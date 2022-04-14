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
        //Creates a default point for the object to move to
        MemoryPoint = new Vector3(0.303f, 0, 2.727f);
        scan = GameObject.Find("MenuNavigation").GetComponent<Scan>();
    }

    private void Update()
    {
        // creates a raycast which is projected from the players touch
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
    // allows other objects to get a single instance of the touch the player is placing
    public static Vector3 GetTouchWorldPosition() => Instance.GetTouchWorldPosition_Instance();

    private Vector3 GetTouchWorldPosition_Instance()
    {
        // checks if the palyer is touching screen and isn't in scan mode then moves the raycast to the players touch
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
       //ensures all null results move the player's touch back to the centre of the screen
                else { return MemoryPoint; }
            }
            else { return MemoryPoint; }
        }
        else { return MemoryPoint; }
        
        
    }

}
