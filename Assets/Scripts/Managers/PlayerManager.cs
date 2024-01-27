using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public const float MAX_HEALTH = 100;
    public const float LAUGH_STUN = 4f;
    private float _currentHealth { get; set; }

    private PlayerInput _playerInput; // Reference to the PlayerInput component

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
        Debug.Log("Player died.");

        // Disable Player Input
        if (_playerInput != null)
        {
            _playerInput.enabled = false;
        }

        // Start the coroutine to re-enable input after 2 seconds
        StartCoroutine(ReEnableInputAfterDelay(LAUGH_STUN));
    }

    private IEnumerator ReEnableInputAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Re-enable Player Input
        if (_playerInput != null)
        {
            _playerInput.enabled = true;
        }
    }
}
