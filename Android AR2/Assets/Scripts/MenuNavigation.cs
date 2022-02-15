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

    public void Awake()
    {
        UnlockedCode = 1;
        bonusMenu = GameObject.Find("Bonus Menu");
        pointA = new Vector3(3f,-5200f,0f);
        pointB = new Vector3(3f,-12700f, 0f);
    }

    private void FixedUpdate()
    {
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
        isLowerMenuActive = !isLowerMenuActive;
        CheckUnlocks();
        if (isLowerMenuActive == true) { Unlocks(); }
    }
    private void CheckUnlocks()
    {
        UnlockedCode = 1;
        if (PopcornUnlocked == true) { UnlockedCode = UnlockedCode * 2; }
        else if (DunkTankUnlocked == true) { UnlockedCode = UnlockedCode * 3; }
        else if (BallThrowUnlocked == true) { UnlockedCode = UnlockedCode * 5; }
        else if (FishCatchUnlocked == true) { UnlockedCode = UnlockedCode * 7; }
        else if (BumperCarsUnlocked == true) { UnlockedCode = UnlockedCode * 11; }

    }

    private void Unlocks()
    {
        for (int x = 2; x <= 13; x++)
        {
            Button CurrentBtn = GameObject.Find("BuildBtn" + (x - 1))?.GetComponent<Button>();
            if (CurrentBtn != null)
            {
                if (UnlockedCode % x == 0) { CurrentBtn.interactable = true; }
                else { CurrentBtn.interactable = false; }
            }
            
        }
    }
}
