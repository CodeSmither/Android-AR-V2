using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Inspired by Inspired by https://www.youtube.com/watch?v=dulosHPl82A&ab_channel=CodeMonkey
public class BuildingPreview : MonoBehaviour
{
    private Transform visual;
    private BuildingTypeSO buildingtTypeSO;

    private void Start()
    {
        //refreshes the visual to the selected building type when the game starts
        RefreshVisual();
        // calls an event to check when the game object is changed again
        GridConstructionSystem.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
    }

    private void Instance_OnSelectedChanged(object sender, System.EventArgs e)
    {
        RefreshVisual();
        
    }

    private void LateUpdate()
    {
        // will update an object after it's needed position has be calcuated int the regular update command to prevent the visuals outpassing the normal movement of the object and moving all over the place
        Vector3 targetPosition = GridConstructionSystem.Instance.GetMouseWorldSnappedPosition();
        targetPosition.y = -1.14f;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

        transform.rotation = Quaternion.Lerp(transform.rotation, GridConstructionSystem.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }

    // checks if the visual for an object has changed both in what kind of object it should be but also it's position
    private void RefreshVisual()
    {
        if (visual != null)
        {
            Destroy(visual.gameObject);
            visual = null;
        }

        BuildingTypeSO buildingtTypeSO = GridConstructionSystem.Instance.GetPlacedObjectTypeSO();

        if (buildingtTypeSO != null)
        {
            visual = Instantiate(buildingtTypeSO.visual, Vector3.zero, Quaternion.identity);
            
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
            SetLayerPreview(visual.gameObject, 3);
        }
    }
    // declares which layer the preview of an object should appear on thus not intersecting with other raycasts which are activated.
    private void SetLayerPreview(GameObject targetGameObject, int layer)
    {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform)
        {
            SetLayerPreview(child.gameObject, layer);
        }
    }



}
