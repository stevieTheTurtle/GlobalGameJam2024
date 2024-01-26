//Generic utility Timer class

using UnityEngine;

public class Timer
{
    private float secondsElapsed;
    private float secondsRemaining;
    private GameManager gameManager;
    
    public Timer(int waitSeconds)
    {
        secondsElapsed = 0;
        secondsRemaining = waitSeconds;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    public void Update(float deltaTime)
    {
        secondsRemaining -= deltaTime;
        secondsElapsed += deltaTime;
        
        //if(secondsRemaining <= 0f)
            
    }

    public float TimeRemaining()
    {
        return secondsRemaining;
    }

    public float TimeElapsed()
    {
        return secondsElapsed;
    }
}
