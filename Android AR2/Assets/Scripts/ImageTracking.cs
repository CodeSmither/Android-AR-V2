using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Prefablist;
    private Dictionary<string, GameObject> createdPreFabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager ImageStorer;
    private Scan scan;
    private GameObject ArCamera;
    [HideInInspector]
    private MenuNavigation menuNavigation;
    private void Awake()
    {
        scan = GameObject.Find("MenuNavigation").GetComponent<Scan>();
        ImageStorer = FindObjectOfType<ARTrackedImageManager>();
        ArCamera = GameObject.Find("AR Camera");
        menuNavigation = GameObject.Find("MenuNavigation").GetComponent<MenuNavigation>();
        foreach(GameObject prefab in Prefablist)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            newPrefab.tag = "Scan";
            createdPreFabs.Add(prefab.name, newPrefab);
            newPrefab.SetActive(false);
        }
    }
    // Checks if the Image which has been created has changed
    private void OnEnable()
    {
        ImageStorer.trackedImagesChanged += ChangeInImages;
    }
    // Checks if the Image which has been created has changed
    private void OnDisable()
    {
        ImageStorer.trackedImagesChanged -= ChangeInImages;
        
    }
    // Checks if the Image event has been updated then applies an effect based on if an object has been added, updated or removed
    private void ChangeInImages(ARTrackedImagesChangedEventArgs eventargs)
    {
        foreach(ARTrackedImage trackableImage in eventargs.added)
        {
            UpdateImage(trackableImage);
          
            
        }
        foreach (ARTrackedImage trackableImage in eventargs.updated)
        {
            UpdateImage(trackableImage);
        }
        foreach (ARTrackedImage trackableImage in eventargs.removed)
        {
            createdPreFabs[trackableImage.name].SetActive(false);
        }
    }
    //  updates the image when the image used is changed
    private void UpdateImage(ARTrackedImage trackableImage)
    {
        string Imagename = trackableImage.referenceImage.name;
        AssignGameObject(trackableImage.referenceImage.name, trackableImage.transform.position);
        
    }
    // sets a gameobject to an image if it matches the gameobject in the library
    private void AssignGameObject(string name, Vector3 newPosition)
    {
        if(Prefablist != null)
        {
            scan.UnlockedItems(name);
            createdPreFabs[name].SetActive(true);
            createdPreFabs[name].transform.position = newPosition;
            createdPreFabs[name].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            foreach(GameObject go in createdPreFabs.Values)
            {
                if (go.name != name) { go.SetActive(false); }
            }
        }
    }
    
    

}
