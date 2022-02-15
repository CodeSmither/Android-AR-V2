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
    [HideInInspector]
    public Text currentStatus;
    private List<string> ListOfObjects = new List<string>();
    private int ObjectCount;
    private MenuNavigation menuNavigation;

    private void Awake()
    {
        ImageStorer = FindObjectOfType<ARTrackedImageManager>();
        menuNavigation = GameObject.Find("MenuNavigation").GetComponent<MenuNavigation>();
        foreach(GameObject prefab in Prefablist)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            createdPreFabs.Add(prefab.name, newPrefab);
            
            
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
    private void ChangeInImages(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage trackableImage in eventArgs.added)
        {
            UpdateImage(trackableImage);
            ObjectCount += 1;
            AddedtoList(trackableImage);
            
        }
        foreach (ARTrackedImage trackableImage in eventArgs.updated)
        {
            UpdateImage(trackableImage);
        }
        foreach (ARTrackedImage trackableImage in eventArgs.removed)
        {
            createdPreFabs[trackableImage.name].SetActive(false);
            ObjectCount -= 1;
            RemovedfromList(trackableImage);
        }
    }
    private void UpdateImage(ARTrackedImage trackableImage)
    {
        string Imagename = trackableImage.referenceImage.name;
        Vector3 Imageposition = trackableImage.transform.position;

        GameObject prefab = createdPreFabs[Imagename];
        prefab.transform.position = Imageposition;
        prefab.SetActive(true);

        foreach(GameObject currentObject in createdPreFabs.Values)
        {
           
        }
    }
    private void AddedtoList(ARTrackedImage trackableImage)
    {
        string Imagename = trackableImage.referenceImage.name;
        ListOfObjects.Add(Imagename);
        CurrentStatus();
        if(trackableImage.name == "PopCorn") { menuNavigation.PopcornUnlocked = true; }
        else if(trackableImage.name == "DunkTank") { menuNavigation.DunkTankUnlocked = true; }
        else if(trackableImage.name == "BallThrow") { menuNavigation.BallThrowUnlocked = true; }
        else if(trackableImage.name == "FishCatch") { menuNavigation.FishCatchUnlocked = true; }
        else if(trackableImage.name == "BumperCars") { menuNavigation.BumperCarsUnlocked = true; }
    }
    private void RemovedfromList(ARTrackedImage trackableImage)
    {
        string Imagename = trackableImage.referenceImage.name;
        ListOfObjects.RemoveAt(ObjectCount);
        CurrentStatus();
    }
    private void CurrentStatus()
    {
        currentStatus.text = "Viewed objects:" + string.Join(" ",ListOfObjects.ToArray());
    }
    private void Start()
    {
        currentStatus = GameObject.Find("Viewing Object").GetComponent<Text>();
    }
    
    
}
