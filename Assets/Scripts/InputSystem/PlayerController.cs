using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 4.0f;
    [SerializeField] private float dashDuration = 0.5f; // Duration of the dash
    [SerializeField] private float dashSpeedMultiplier = 3f; // Speed multiplier during dash
    [SerializeField] private float dashCooldown = 2f; // Cooldown duration for dash

    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector2 movement;
    private Vector2 lookAt;
    private float speedCoefficient;
    private bool isDashing;
    private float lastDashTime;

    private List<CollectTrigger> overlappingCollectTriggers;
    public bool isTryingToInteract { get; set; }

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        overlappingCollectTriggers = new List<CollectTrigger>();
        isTryingToInteract = false;
        isDashing = false;
        lastDashTime = -dashCooldown; // Initialize to allow immediate dashing
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnLookAt(InputAction.CallbackContext context)
    {
        lookAt = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        // Check if the dash command was performed, if the cooldown has elapsed, and if the player is moving
        if (context.performed && Time.time - lastDashTime >= dashCooldown && movement != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
    }


    private IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Attacking");
        }
    }

    public void OnTest(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("TryingToInteract");
            isTryingToInteract = true;
        }
    }

    void Update()
    {
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        Vector3 lookingAt = new Vector3(lookAt.x, 0, lookAt.y);

        speedCoefficient = Mathf.Cos(Vector3.Angle(move, gameObject.transform.forward) / 3f / 180f * Mathf.PI);

        if (isDashing)
        {
            controller.Move(move * Time.deltaTime * playerSpeed * dashSpeedMultiplier);
        }
        else
        {
            controller.Move(move * Time.deltaTime * playerSpeed * speedCoefficient);

            if (lookingAt != Vector3.zero)
            {
                gameObject.transform.forward = lookingAt;
            }
        }

        if (isTryingToInteract && overlappingCollectTriggers.Count > 0)
        {
            overlappingCollectTriggers[0].CollectObject();
            overlappingCollectTriggers.RemoveAt(0);
        }

        isTryingToInteract = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CollectTrigger collectTrigger = other.GetComponentInChildren<CollectTrigger>();
        if (collectTrigger != null)
        {
            overlappingCollectTriggers.Add(collectTrigger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CollectTrigger collectTrigger = other.GetComponentInChildren<CollectTrigger>();
        if (collectTrigger != null)
        {
            overlappingCollectTriggers.Remove(collectTrigger);
        }
    }
}
