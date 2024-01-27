using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    private PlayerInputManager _playerInputManager;
    private Transform _centerOfView;
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // Reference to the virtual camera

    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    private CinemachineFramingTransposer framingTransposer;

    [SerializeField] private float minCameraDistance = 10f;
    [SerializeField] private float maxCameraDistance = 60f;

    private List<Transform> _playerTransforms = new List<Transform>(); // Initialize the list

    void Start()
    {
        _centerOfView = FindObjectOfType<CenterOfView>().transform;
        // Assuming virtualCamera is assigned in the Inspector
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

    }

    void Update()
    {
        if (_playerTransforms.Count > 0)
        {
            Vector3 sumOfPositions = Vector3.zero;
            foreach (Transform playerTransform in _playerTransforms)
            {
                sumOfPositions += playerTransform.position;
            }
            Vector3 averagePosition = sumOfPositions / _playerTransforms.Count;

            if (_centerOfView != null)
            {
                _centerOfView.position = averagePosition;
            }

            // Set the Cinemachine camera to follow the average position
            virtualCamera.Follow = _centerOfView;
        }

        // Adjust camera based on the maximum distance between any two players
        if (framingTransposer != null && _playerTransforms.Count > 1)
        {
            float maxPlayerDistance = CalculateMaxPlayerDistance();
            float proportion = Mathf.InverseLerp(minDistance, maxDistance, maxPlayerDistance);
            float targetCameraDistance = Mathf.Lerp(minCameraDistance, maxCameraDistance, proportion);

            framingTransposer.m_CameraDistance = targetCameraDistance;
        }
    }

    private float CalculateMaxPlayerDistance()
    {
        float maxDistance = 0f;
        for (int i = 0; i < _playerTransforms.Count; i++)
        {
            for (int j = i + 1; j < _playerTransforms.Count; j++)
            {
                float distance = Vector3.Distance(_playerTransforms[i].position, _playerTransforms[j].position);
                maxDistance = Mathf.Max(maxDistance, distance);
            }
        }
        return maxDistance;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        _playerTransforms.Add(playerInput.transform);
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        _playerTransforms.Remove(playerInput.transform);
    }

    private void OnDestroy()
    {
    }
}
