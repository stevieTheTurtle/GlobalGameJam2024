using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class CollectTrigger : MonoBehaviour
{
    private Collider collider;
    private GameObject collectableObject;
    private void Start()
    {
        collider = this.GetComponentInChildren<Collider>();
        if(collider == null)
            Debug.LogError("No collider found on " + this.gameObject.name + " or its children.");
        
        collider.isTrigger = true;
    }

    public abstract void CollectObject();
}
