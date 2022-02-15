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
    private MenuNavigation menuNavigation;
    private void Awake()
    {
        ImageStorer = FindObjectOfType<ARTrackedImageManager>();
        menuNavigation = GameObject.Find("MenuNavigation").GetComponent<MenuNavigation>();
        foreach(GameObject prefab in Prefablist)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
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
    private void ChangeInImages(ARTrackedImagesChangedEventArgs args)
    {
        foreach(ARTrackedImage trackableImage in args.added)
        {
            UpdateImage(trackableImage);
            AddedtoList(trackableImage);
            
        }
        foreach (ARTrackedImage trackableImage in args.updated)
        {
            UpdateImage(trackableImage);
        }
        foreach (ARTrackedImage trackableImage in args.removed)
        {
            createdPreFabs[trackableImage.name].SetActive(false);
            RemovedfromList(trackableImage);
        }
    }
    private void UpdateImage(ARTrackedImage trackableImage)
    {
        string Imagename = trackableImage.referenceImage.name;
        AssignGameObject(trackableImage.referenceImage.name, trackableImage.transform.position);
        /*Vector3 Imageposition = trackableImage.transform.position;
        GameObject prefab = createdPreFabs[Imagename];
        prefab.transform.position = Imageposition;
        prefab.SetActive(true);*/
    }
    private void AssignGameObject(string name, Vector3 newPosition)
    {
        if(Prefablist != null)
        {
            createdPreFabs[name].SetActive(true);
            createdPreFabs[name].transform.position = newPosition;
            createdPreFabs[name].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            foreach(GameObject go in createdPreFabs.Values)
            {
                if (go.name != name) { go.SetActive(false); }
            }
        }
    }
    private void AddedtoList(ARTrackedImage trackableImage)
    {
        string Imagename = trackableImage.referenceImage.name;
        /*
        if(trackableImage.name == "PopCorn") { menuNavigation.PopcornUnlocked = true; }
        else if(trackableImage.name == "DunkTank") { menuNavigation.DunkTankUnlocked = true; }
        else if(trackableImage.name == "BallThrow") { menuNavigation.BallThrowUnlocked = true; }
        else if(trackableImage.name == "FishCatch") { menuNavigation.FishCatchUnlocked = true; }
        else if(trackableImage.name == "BumperCars") { menuNavigation.BumperCarsUnlocked = true; }
        */
    }
    private void RemovedfromList(ARTrackedImage trackableImage)
    {
        string Imagename = trackableImage.referenceImage.name;
    }
    

}
