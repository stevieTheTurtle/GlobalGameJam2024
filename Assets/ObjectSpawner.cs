using System.Collections;
using System.Timers;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objects;
    
    void Start()
    {
        Timer t = new Timer();
        t.Elapsed += new ElapsedEventHandler(OnTimer);
        t.Interval = 3000;
        t.Start();
    }
    
    void Update()
    {
        
    }
    
    void OnTimer(object source, ElapsedEventArgs e)
    {
        Debug.Log("Timer elapsed");
    }
}
