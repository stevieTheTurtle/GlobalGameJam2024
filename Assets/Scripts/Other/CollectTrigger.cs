using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectTrigger : MonoBehaviour
{
    private Collider collider;
    private ICollectable collectableObject;
    
    private void Start()
    {
        collectableObject = GetComponentInChildren<ICollectable>();
        if(collectableObject == null)
            Debug.LogError("No ICollectable found on " + this.gameObject.name + " or its children.");
        
        collider = this.GetComponentInChildren<Collider>();
        if(collider == null)
            Debug.LogError("No collider found on " + this.gameObject.name + " or its children.");
        
        collider.isTrigger = true;
    }

    public void Interact(PlayerManager playerManager)
    {
        collectableObject.CollectObjectFor(playerManager);
        Destroy(this.gameObject);
    }
}
