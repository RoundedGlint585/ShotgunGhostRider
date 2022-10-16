using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    float levelLength = 60.0f;

    private float currentTime = 0.0f;

    private bool isFinished;
    void Start()
    {
        
    }

    public float GetLevelLength()
    {
        return levelLength;
    }
    
    public float GetCurrentTime()
    {
        return currentTime;
    }

    public bool IsFinished()
    {
        return isFinished;
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerScript>().GetLifes() > 0)
        {
            currentTime += Time.deltaTime;
        }
        if(currentTime >= levelLength)
        {
            isFinished = true;
        }
    }
}
