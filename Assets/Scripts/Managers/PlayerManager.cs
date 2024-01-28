using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public const float MAX_HEALTH = 100;
    public const float LAUGH_STUN = 4f;

    [SerializeField] private float laughTimeReuction;
    [SerializeField] private float laughTimer;

    [SerializeField] public float _currentHealth;

    private PlayerInput _playerInput; // Reference to the PlayerInput component
    private bool isLaughing = false;
    public bool lost = false;

    void Start()
    {
        _currentHealth = MAX_HEALTH;
        _playerInput = GetComponent<PlayerInput>(); // Get the PlayerInput component
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public bool IsAlive()
    {
        return _currentHealth > 0;
    }

    public void Heal(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, MAX_HEALTH);
    }

    private void Die()
    {
        if (isLaughing) { return; }
        isLaughing = true;
        laughTimer = LAUGH_STUN;
        Debug.Log("Player died.");

        // Disable specific action maps
        if (_playerInput != null)
        {
            _playerInput.actions.FindActionMap("Gameplay").Disable();
            _playerInput.actions.FindActionMap("LaughMap").Enable();
            this.transform.Rotate(90, 0, 0);
        }
    }

    void Update()
    {
        if (!isLaughing) return;

        if (laughTimer <= 0)
        {
            // Re-enable specific action maps
            if (_playerInput != null)
            {
                _playerInput.actions.FindActionMap("Gameplay").Enable(); // Example action map name
                _playerInput.actions.FindActionMap("LaughMap").Disable();
                this.transform.Rotate(-90, 0, 0);
            }

            isLaughing = false;
            _currentHealth = MAX_HEALTH;
        }
        
        laughTimer -= Time.deltaTime;

        if (lost) // Assuming 'lost' is a condition you define somewhere
        {
            // Disable the Gameplay action map
            _playerInput.actions.FindActionMap("Gameplay").Disable();

            // Find and disable the child GameObject named "Visual"
            Transform visualChild = transform.Find("Visuals");
            Collider childCollider = GetComponent<Collider>();
            if (childCollider != null)
            {
                childCollider.enabled = false;
            }
            if (visualChild != null)
            {
                visualChild.gameObject.SetActive(false);
            }
        }
    }

    public void ReduceLaughTimer()
    {
        laughTimer -= laughTimeReuction;
    }
}
