using System.Collections;
using System.Timers;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objects;

    [SerializeField] 
    private List<Transform> spawnPoints;

    [SerializeField]
    private float timerMilliseconds = 3f;
    private float timeCounter;
    
    void Start()
    {
        timeCounter = timerMilliseconds;
    }
    
    void Update()
    {
        timeCounter -= Time.deltaTime * 1000f;
        
        if (timeCounter <= 0f)
        {
            Debug.Log("Timer elapsed, spawning object");
            int objectIndex = Random.Range(0, objects.Count);
            int spawnPointIndex = Random.Range(0, spawnPoints.Count);
            SpawnObject(objects[objectIndex], spawnPoints[spawnPointIndex]);
            
            timeCounter = timerMilliseconds;
        }
    }

    void SpawnObject(GameObject obj, Transform spawnPoint)
    {
        Instantiate(obj, spawnPoint.position, spawnPoint.rotation);
    }
}
