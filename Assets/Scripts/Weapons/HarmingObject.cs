using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

///<summary>
///Component attached to gameobjects that can damage IDeamageable objects.
///</summary>
public class HarmingObject : MonoBehaviour
{
    private float Damage { get; set; }
    [SerializeField] protected AudioClip hitSound;
    [SerializeField] protected AudioSource audioSource;
    
    private void Start()
    {
        if (this.GetComponentInChildren<Collider>() == null)
            Debug.LogError("No collider found on " + this.gameObject.name + " or its children.");
        
        if (hitSound == null)
            Debug.LogWarning("No hit sound found on " + this.gameObject.name + ".");
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogWarning("No audio source found on " + this.gameObject.name + ".");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDameageble damageable))
        {
            //Damage the other object
            damageable.Damage(Damage);
            //Play hit sound
            PlayHitSound();
            //Degrade weapon???
        }
    }
    
    private void PlayHitSound()
    {
        audioSource.clip = hitSound;
        audioSource.Play();
    }
}
