using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfView : MonoBehaviour
{
    [SerializeField] Transform Player1;
    [SerializeField] Transform Player2;

    // Update is called once per frame
    void Update()
    {
        if (Player1 != null && Player2 != null)
        {
            Vector3 middlePoint = (Player1.position + Player2.position) / 2;
            transform.position = middlePoint;
        }
    }
}
