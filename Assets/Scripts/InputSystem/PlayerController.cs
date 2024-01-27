using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector2 movement;

    public void OnEnable()
    {

    }
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
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
        IDamageable damageable;
        if (this.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.TakeDamage(100);
        }
        else
        {
            // The component was not found
        }

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
    }
}


