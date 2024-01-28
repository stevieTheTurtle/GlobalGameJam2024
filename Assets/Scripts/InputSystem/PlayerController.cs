using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 4.0f;
    [SerializeField] private float dashDuration = 0.5f; // Duration of the dash
    [SerializeField] private float dashSpeedMultiplier = 3f; // Speed multiplier during dash
    [SerializeField] private float dashCooldown = 2f; // Cooldown duration for dash
    
    private PlayerManager _playerManager;
    private CharacterController _controller;
    private Animator _animator;
    private Vector3 _playerVelocity;
    private Vector2 _movement;
    private Vector2 _lookAt;
    private float _speedCoefficient;
    private bool _isDashing;
    private float _lastDashTime;

    private List<CollectTrigger> _overlappingCollectTriggers;
    public bool IsTryingToInteract { get; set; }

    public ICollectable HoldedItem;
    public bool isHoldingSomething;

    private void Start()
    {
        _playerManager = GetComponent<PlayerManager>();
        _animator = GetComponent<Animator>();
        _controller = gameObject.AddComponent<CharacterController>();
        
        _overlappingCollectTriggers = new List<CollectTrigger>();
        IsTryingToInteract = false;
        _isDashing = false;
        _lastDashTime = -dashCooldown; // Initialize to allow immediate dashing
    }
    
    public void OnTryToBeSerious(InputAction.CallbackContext context)
    {
        _playerManager.ReduceLaughTimer();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
        _animator.SetFloat("MoveSpeed", _movement.magnitude * 5f);
    }

    public void OnLookAt(InputAction.CallbackContext context)
    {
        _lookAt = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        // Check if the dash command was performed, if the cooldown has elapsed, and if the player is moving
        if (context.performed && Time.time - _lastDashTime >= dashCooldown && _movement != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
    }


    private IEnumerator Dash()
    {
        _isDashing = true;
        _lastDashTime = Time.time;

        yield return new WaitForSeconds(dashDuration);

        _isDashing = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (HoldedItem is IWeapon weapon) //I mean if the holded item also imlements IWeapon as well
            {
                Debug.Log("Attacking");
                weapon.Attack();
            }
            else
            {
                _animator.SetTrigger("MeleeAttack");
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("TryingToInteract");
            IsTryingToInteract = true;
        }
    }

    void Update()
    {
        Vector3 move = new Vector3(_movement.x, 0, _movement.y);
        Vector3 lookingAt = new Vector3(_lookAt.x, 0, _lookAt.y);

        _speedCoefficient = Mathf.Cos(Vector3.Angle(move, gameObject.transform.forward) / 3f / 180f * Mathf.PI);

        if (_isDashing)
        {
            _controller.Move(move * Time.deltaTime * playerSpeed * dashSpeedMultiplier);
        }
        else
        {
            _controller.Move(move * Time.deltaTime * playerSpeed * _speedCoefficient);

            if (lookingAt != Vector3.zero)
            {
                gameObject.transform.forward = lookingAt;
            }
        }

        if (IsTryingToInteract && _overlappingCollectTriggers.Count > 0)
        {
            Debug.Log("Interacting with "+_overlappingCollectTriggers[0].gameObject.name);
            _overlappingCollectTriggers[0].Interact(this.GetComponent<PlayerManager>());
            _overlappingCollectTriggers.RemoveAt(0);
        }

        IsTryingToInteract = false;

        HoldedItem = GetComponentInChildren<ICollectable>();
        isHoldingSomething = HoldedItem != null;

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this.gameObject.name+" entered trigger of "+other.gameObject.name);
        CollectTrigger collectTrigger = other.GetComponentInChildren<CollectTrigger>();
        if (collectTrigger != null)
        {
            //Debug.Log("adding "+collectTrigger.gameObject.name+" to overlapping collect triggers");
            if(!_overlappingCollectTriggers.Contains(collectTrigger))
                _overlappingCollectTriggers.Add(collectTrigger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(this.gameObject.name+" entered trigger of "+other.gameObject.name);
        CollectTrigger collectTrigger = other.GetComponentInChildren<CollectTrigger>();
        if (collectTrigger != null)
        {
            //Debug.Log("removing "+collectTrigger.gameObject.name+" to overlapping collect triggers");
            if(_overlappingCollectTriggers.Contains(collectTrigger))
                _overlappingCollectTriggers.Remove(collectTrigger);
        }
    }
}
