using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Collider collider;
    
    void Start()
    {
        collider = this.GetComponentInChildren<Collider>();
        if(collider == null)
            Debug.LogError("No collider found on " + this.gameObject.name + " or its children.");
        collider.isTrigger = true;
    }
}
