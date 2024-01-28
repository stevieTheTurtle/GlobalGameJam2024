using UnityEngine;

public class MeleeWeapon : HarmingObject, IWeapon
{
    [SerializeField] AudioClip attackSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Attack()
    {
        Debug.Log(this.gameObject.name + "has been used.");
        PlayAttackSound();
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
