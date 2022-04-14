using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Unused Animator Features
public class Animation : MonoBehaviour
{
    private Animator animetion;
    // prefab animator
    private bool interacted;
    // check if prefab is interacted with
    public bool Interacted
    {
        get
        {
            return interacted;
        }
        set
        {
            interacted = value;
        }
    }

    private void Awake()
    {
        //sets up animation for prefab
        interacted = false;
        GameObject child = gameObject.GetComponentInChildren<GameObject>();
        animetion = child.GetComponentInChildren<Animator>();
        animetion.Rebind();
    }

    private void FixedUpdate()
    {
        // Sets the interaction to the player prefab
        animetion.SetBool("Interacted", interacted);
    }
}
