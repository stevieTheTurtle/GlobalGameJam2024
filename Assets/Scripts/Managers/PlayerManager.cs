using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public const float MAX_HEALTH = 100;
    public const float LAUGH_STUN = 4f;

    [SerializeField] private float _currentHealth;

    private PlayerInput _playerInput; // Reference to the PlayerInput component
    private bool isLaughing = false;

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
        Debug.Log("Player died.");

        // Disable specific action maps
        if (_playerInput != null)
        {
            _playerInput.actions.FindActionMap("Gameplay").Disable(); // Example action map name
            this.transform.Rotate(90, 0, 0);
        }

        // Start the coroutine to re-enable input after delay
        StartCoroutine(ReEnableInputAfterDelay(LAUGH_STUN));
    }

    private IEnumerator ReEnableInputAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Re-enable specific action maps
        if (_playerInput != null)
        {
            _playerInput.actions.FindActionMap("Gameplay").Enable(); // Example action map name
            this.transform.Rotate(-90, 0, 0);
        }
        isLaughing = false;
        _currentHealth = MAX_HEALTH;
    }

}
