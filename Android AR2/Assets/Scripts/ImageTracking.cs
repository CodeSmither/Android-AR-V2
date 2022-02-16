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
    private GameObject ArCamera;
    [HideInInspector]
    private MenuNavigation menuNavigation;
    private void Awake()
    {
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
            AddedtoList(trackableImage);
            
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
        Object[] gameObjects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject Objects in gameObjects)
            if (Objects.GetComponent<Renderer>().isVisible && Vector3.Distance(Objects.gameObject.transform.position, ArCamera.transform.position) > 0.1f)
            {
                if(Objects.name == "PopcornCartVersion1") { menuNavigation.PopcornUnlocked = true; }
                else if (Objects.name == "DunkTank") { menuNavigation.PopcornUnlocked = true; }
                else if (Objects.name == "BallThrow") { menuNavigation.PopcornUnlocked = true; }
                else if (Objects.name == "FishCatch") { menuNavigation.PopcornUnlocked = true; }
                else if (Objects.name == "BumperCars") { menuNavigation.PopcornUnlocked = true; }
            }
    }
    

}
