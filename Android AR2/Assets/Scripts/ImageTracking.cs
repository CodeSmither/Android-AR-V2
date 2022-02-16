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
    private void OnEnable()
    {
        ImageStorer.trackedImagesChanged += ChangeInImages;
    }
    private void OnDisable()
    {
        ImageStorer.trackedImagesChanged -= ChangeInImages;
        
    }
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
    private void UpdateImage(ARTrackedImage trackableImage)
    {
        string Imagename = trackableImage.referenceImage.name;
        AssignGameObject(trackableImage.referenceImage.name, trackableImage.transform.position);
        
    }
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
