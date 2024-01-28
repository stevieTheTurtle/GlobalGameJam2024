using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectTrigger : MonoBehaviour
{
    [SerializeField]
    private Collider collider;
    [SerializeField]
    private GameObject collectableObjectGO;
    
    private ICollectable collectableObject;
    
    private void Start()
    {
        collectableObject = GetComponentInChildren<ICollectable>();
        collectableObjectGO = collectableObject.GetTransform().gameObject;
        if(collectableObject == null)
            Debug.LogError("No ICollectable found on " + this.gameObject.name + " or its children.");
        
        collider = this.GetComponentInChildren<Collider>();
        if(collider == null)
            Debug.LogError("No collider found on " + this.gameObject.name + " or its children.");
        
        collider.isTrigger = true;
        
        Destroy(this.gameObject, 16f);
    }

    public void Interact(PlayerManager playerManager)
    {
        collectableObject.CollectObjectFor(playerManager);
        
        Destroy(this.gameObject);
    }
}
