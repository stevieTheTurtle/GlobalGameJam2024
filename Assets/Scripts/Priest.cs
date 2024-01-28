using UnityEngine;
using UnityEngine.AI;

public class Priest : MonoBehaviour
{
    private GameManager _gameManager;
    private Vector3 _originalPosition; // Store the original position as a Vector3
    private NavMeshAgent agent;
    [SerializeField] protected AudioClip angrySound;
    [SerializeField] protected AudioClip holySpiritSound;
    [SerializeField] protected AudioSource audioSource;

    [SerializeField] private float chaseSpeed = 6.0f; // Speed when chasing a player
    [SerializeField] private float returnSpeed = 2.0f; // Speed when returning to the original position

    private PlayerManager _currentTarget; // To keep track of the current target

    void Start()
    {
        _originalPosition = transform.position; // Save the original position
        _gameManager = FindObjectOfType<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        // Initialize the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource if it doesn't exist
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (_gameManager == null || _gameManager._playerManagers == null || _gameManager._playerManagers.Count == 0)
        {
            return; // Exit if no game manager or players are available
        }

        if (_currentTarget != null && _currentTarget._currentHealth > 0)
        {
            _currentTarget = null; // Clear the current target if their health is restored
        }

        if (_currentTarget == null)
        {
            foreach (PlayerManager playerManager in _gameManager._playerManagers)
            {
                if (playerManager != null && playerManager._currentHealth == 0)
                {
                    _currentTarget = playerManager;
                    audioSource.clip = angrySound;
                    audioSource.Play();
                    break; // Stop searching once a valid target is found
                }
            }
        }

        UpdateAgentDestinationAndSpeed();


    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has a PlayerManager component
        PlayerManager playerManager = other.GetComponent<PlayerManager>();
        if (playerManager != null)
        {
            // Destroy the GameObject that holds the PlayerManager
            audioSource.clip = holySpiritSound;
            audioSource.Play();
            playerManager.lost = true;
        }
    }
    private void UpdateAgentDestinationAndSpeed()
    {
        if (_currentTarget != null)
        {
            // Chase the target player
            agent.destination = _currentTarget.transform.position;
            agent.speed = chaseSpeed;
        }
        else
        {
            // Return to the original position
            agent.destination = _originalPosition;
            agent.speed = returnSpeed;
        }
    }
}
