using Unity.VisualScripting;
using UnityEngine;

///<summary>
///Component attached to gameobjects that can damage IDeamageable objects.
///</summary>
public class HarmingObject : MonoBehaviour
{
    [SerializeField] protected float Damage = 10f;
    [SerializeField] protected AudioClip hitSound;
    [SerializeField] protected AudioSource audioSource;
    
    protected virtual void Start()
    {
        if (this.GetComponentInChildren<Collider>() == null)
            Debug.LogError("No collider found on " + this.gameObject.name + " or its children.");
        
        if (hitSound == null)
            Debug.LogWarning("No hit sound found on " + this.gameObject.name + ".");
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogWarning("No audio source found on " + this.gameObject.name + ".");
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            //Damage the other object
            damageable.TakeDamage(Damage);
            //Play hit sound
            PlayHitSound();
            //Degrade weapon???
            Destroy(this.gameObject);
        }
    }
    
    protected void PlayHitSound()
    {
        audioSource.clip = hitSound;
        audioSource.Play();
    }
}
