using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class ConsumableBook : MonoBehaviour, ICollectable
{
    
    [SerializeField] [Range(0.5f,1f)] private float probabilityOfSuccess;
    private float _probabilityOfFailure;
    
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float healAmount = 10f;
    
    [SerializeField] private float animationMillis = 500f;
    
    [Header("Visuals")]
    [SerializeField] private GameObject sadBookModel;
    [SerializeField] private GameObject hentaiBookModel;
    
    [Header("Sound")]
    [SerializeField] private AudioClip sadSound;
    [SerializeField] private AudioClip hentaiSound;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        if (sadBookModel == null)
            Debug.LogError("No sad book model found on " + this.gameObject.name + ".");
        
        if (hentaiBookModel == null)
            Debug.LogError("No hentai book model found on " + this.gameObject.name + ".");
        
        if (sadSound == null)
            Debug.LogError("No sad sound found on " + this.gameObject.name + ".");
        
        if (hentaiSound == null)
            Debug.LogError("No hentai sound found on " + this.gameObject.name + ".");
        
        if (audioSource == null)
            Debug.LogError("No audio source found on " + this.gameObject.name + ".");
    }

    public void CollectObjectFor(PlayerManager playerManager)
    {
        this.transform.parent = playerManager.transform;
        
        if (Random.value < probabilityOfSuccess)
        {
            //SUCCESS CONDITION
            Debug.Log("Player got a sad book!");
            StartCoroutine(successCoroutine(playerManager));
            PlaySadSound();
            playerManager.Heal(healAmount);
        }
        else
        {
            //FAILURE CONDITION
            Debug.Log("Player got an hentai book!");
            PlayHentaiSound();
            playerManager.TakeDamage(damageAmount);
        }
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    IEnumerator successCoroutine(PlayerManager playerManager)
    {
        GameObject sadBook = Instantiate(sadBookModel, playerManager.transform.position, Quaternion.identity);
        sadBook.transform.parent = playerManager.transform;
        sadBook.transform.localPosition = new Vector3(0, 1f, 1f);
        
        yield return new WaitForSeconds(animationMillis * 1000f);
        
        Destroy(this.gameObject);
    }
    
    IEnumerator failureCoroutine(PlayerManager playerManager)
    {
        GameObject sadBook = Instantiate(sadBookModel, playerManager.transform.position, Quaternion.identity);
        sadBook.transform.parent = playerManager.transform;
        sadBook.transform.localPosition = new Vector3(0, 1f, 1f);
        
        yield return new WaitForSeconds(animationMillis * 1000f);
        
        Destroy(this.gameObject);
    }
    
    private void PlayHentaiSound()
    {
        audioSource.clip = hentaiSound;
        audioSource.Play();
    }

    private void PlaySadSound()
    {
        audioSource.clip = sadSound;
        audioSource.Play();
    }
}
