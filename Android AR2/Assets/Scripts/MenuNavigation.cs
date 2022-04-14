using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField]private bool isLowerMenuActive;
    public int UnlockedCode;
    [SerializeField]private GameObject bonusMenu;
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;
    public Sprite upArrow;
    public Sprite downArrow;
    public bool PopcornUnlocked;
    public bool DunkTankUnlocked;
    public bool BallThrowUnlocked;
    public bool FishCatchUnlocked;
    public bool BumperCarsUnlocked;
    public bool TeaCupsUnlocked;
    public bool TentUnlocked;

    public void Awake()
    {
        UnlockedCode = 1;
        bonusMenu = GameObject.Find("Bonus Menu");
        pointA = new Vector3(3f,8800,0f);
        pointB = new Vector3(3f,1500, 0f);
    }
    
    private void FixedUpdate()
    {
        // moves the lower menu up or down when the arrow button is pressed also changing the button to represent if it is up or down
        if (isLowerMenuActive == false && bonusMenu.transform.position != pointB) 
        {
            Vector3 LerppedPosition = Vector3.Lerp(bonusMenu.transform.position,pointB,2f * Time.deltaTime);
            bonusMenu.GetComponent<RectTransform>().anchoredPosition = LerppedPosition;
        }
        else if (isLowerMenuActive == true && bonusMenu.transform.position != pointA)
        {
            Vector3 LerppedPosition = Vector3.Lerp(bonusMenu.transform.position, pointA,2f * Time.deltaTime);
            bonusMenu.GetComponent<RectTransform>().anchoredPosition = LerppedPosition;
        }
        if (isLowerMenuActive == true) { bonusMenu.GetComponent<Image>().sprite = downArrow; }
        else { bonusMenu.GetComponent<Image>().sprite = upArrow; }

    }
    public void ISLowerMenuActive()
    {
        // checks if the player has activated the menu at the bottom of the screen or not
        isLowerMenuActive = !isLowerMenuActive;
        CheckUnlocks();
        
    }
    private void CheckUnlocks()
    {
        // checks each of the buttons if a object has been unlocked could be optimzed through use of a constructor
        if (PopcornUnlocked == true) { GameObject.Find("BuildBtn3").GetComponent<Button>().interactable = true; }
        else if (DunkTankUnlocked == true) { GameObject.Find("BuildBtn4").GetComponent<Button>().interactable = true; }
        else if (BallThrowUnlocked == true) { GameObject.Find("BuildBtn6").GetComponent<Button>().interactable = true; }
        else if (FishCatchUnlocked == true) { GameObject.Find("BuildBtn8").GetComponent<Button>().interactable = true; }
        else if (BumperCarsUnlocked == true) { GameObject.Find("BuildBtn12").GetComponent<Button>().interactable = true; }
        else if (TeaCupsUnlocked == true) { GameObject.Find("BuildBtn14").GetComponent<Button>().interactable = true; }
        else if (TentUnlocked == true) { GameObject.Find("BuildingBtn18").GetComponent<Button>().interactable = true; }
        if (PopcornUnlocked == false) { GameObject.Find("BuildBtn3").GetComponent<Button>().interactable = false; }
        if (DunkTankUnlocked == false) { GameObject.Find("BuildBtn4").GetComponent<Button>().interactable = false; }
        if (BallThrowUnlocked == false) { GameObject.Find("BuildBtn6").GetComponent<Button>().interactable = false; }
        if (FishCatchUnlocked == false) { GameObject.Find("BuildBtn8").GetComponent<Button>().interactable = false; }
        if (BumperCarsUnlocked == false) { GameObject.Find("BuildBtn12").GetComponent<Button>().interactable = false; }
        if (TeaCupsUnlocked == false) { GameObject.Find("BuildBtn14").GetComponent<Button>().interactable = false; }
        if (TentUnlocked == false) { GameObject.Find("BuildBtn18").GetComponent<Button>().interactable = false; }
    }

    
}
