using Cinemachine;
using UnityEngine;

public class VirtualCameraControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform a;
    [SerializeField] private Transform b;

    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    private CinemachineFramingTransposer framingTransposer;

    [SerializeField] private float minCameraDistance = 10f;
    [SerializeField] private float maxCameraDistance = 60f;

    private void Start()
    {
        if (virtualCamera != null)
        {
            // Get the CinemachineFramingTransposer component
            framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            if (framingTransposer == null)
            {
                Debug.LogError("CinemachineFramingTransposer component not found.");
            }
        }
        else
        {
            Debug.LogError("Virtual camera is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (framingTransposer != null && a != null && b != null)
        {
            float distance = Vector3.Distance(a.position, b.position);
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            // Calculate the camera distance directly proportional to the objects' distance
            float proportion = (distance - minDistance) / (maxDistance - minDistance);
            float targetCameraDistance = minCameraDistance + proportion * (maxCameraDistance - minCameraDistance);

            // Adjust the camera distance
            framingTransposer.m_CameraDistance = targetCameraDistance;
        }
    }
}
