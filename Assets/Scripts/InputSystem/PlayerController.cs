using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 4.0f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector2 movement;
    
    private List<CollectTrigger> overlappingCollectTriggers; 
    public bool isTryingToInteract { get; set; }
        
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        
        overlappingCollectTriggers = new List<CollectTrigger>();
        isTryingToInteract = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attacking");
    }
    public void OnTest(InputAction.CallbackContext context)
    {
        Debug.Log("TryingToInteract");
        isTryingToInteract = true;
    }
    
    void Update()
    {
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        controller.Move(playerVelocity * Time.deltaTime);
        
        
        //Tries to collect object in the frame
        if (isTryingToInteract)
        {
            overlappingCollectTriggers[0].Interact(this.GetComponent<PlayerManager>());
            overlappingCollectTriggers.RemoveAt(0);
        }
        
        isTryingToInteract = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CollectTrigger collectTrigger = other.GetComponentInChildren<CollectTrigger>();
        if(collectTrigger != null)
            overlappingCollectTriggers.Add(collectTrigger);
    }
    
    private void OnTriggerExit(Collider other)
    {
        CollectTrigger collectTrigger = other.GetComponentInChildren<CollectTrigger>();
        if(collectTrigger != null)
            overlappingCollectTriggers.Remove(collectTrigger);
    }
}


