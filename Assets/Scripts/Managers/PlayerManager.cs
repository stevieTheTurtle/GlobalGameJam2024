using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public const float MAX_Health = 100;

    // Public implementation of the Health property from IDamageable
    public float Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Health = MAX_Health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Implement the TakeDamage method
    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            // Handle what happens when health is depleted
        }
    }

    // Implement the IsAlive method
    public bool IsAlive()
    {
        return Health > 0;
    }

    // Implement the Heal method
    public void Heal(float amount)
    {
        Health += amount;
        Health = Mathf.Clamp(Health, 0, MAX_Health);
    }

    public void Die()
    {

    }
}