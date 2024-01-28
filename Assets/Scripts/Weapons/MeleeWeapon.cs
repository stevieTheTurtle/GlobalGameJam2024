using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : HarmingObject, IWeapon
{
    [SerializeField] AudioClip attackSound;
    [SerializeField] private List<Collider> _colliders;

    private void Start()
    {
        if(_colliders.Count == 0)
            Debug.LogError("No collider found on " + this.gameObject.name + " or its children.");
        
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Attack()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
        
        Debug.Log(this.gameObject.name + "has been used.");
        PlayAttackSound();

        StartCoroutine(DisableColliderWithDelay(0.75f));
    }
    
    IEnumerator DisableColliderWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
    }

    public void Release()
    {
        Debug.Log(this.gameObject.name + "has been used.");
        //TODO: play release sound??
    }

    //played by player controller when attacking
    public void PlayAttackSound()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
    }
    
}
