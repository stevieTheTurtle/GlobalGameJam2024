using UnityEngine;

public class Projectile : HarmingObject
{
    [SerializeField] protected AudioClip swooshSound;
    [SerializeField] private float lifetime = 10f; // Lifetime of the projectile in seconds
    [SerializeField] private float _speed = 10f;
    void Start()
    {
        // Initialize the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource if it doesn't exist
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Destroy the projectile after its lifetime has elapsed
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Destroy(this.gameObject);
    }
}