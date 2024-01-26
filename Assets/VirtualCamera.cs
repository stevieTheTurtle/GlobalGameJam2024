using Cinemachine;
using UnityEngine;

public class VirtualCameraControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform a;
    [SerializeField] private Transform b;

    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    // Optional: parameters for adjusting the field of view
    [SerializeField] private float minFieldOfView = 10f;
    [SerializeField] private float maxFieldOfView = 60f;

    // Update is called once per frame
    void Update()
    {
        if (a != null && b != null && virtualCamera != null)
        {
            // Calculate the distance between the two objects
            float distance = Vector3.Distance(a.position, b.position);

            distance = distance < minDistance ? minDistance : distance;

            // Adjust the field of view based on the distance
            // Here, you'll need to determine how the distance affects the field of view.
            // This is a simple linear mapping example:
            float targetFieldOfView = Mathf.Lerp(minFieldOfView, maxFieldOfView, distance/maxDistance);
            virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(targetFieldOfView, minFieldOfView, maxFieldOfView);
        }
    }
}
