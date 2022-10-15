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

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= levelLength)
        {
            isFinished = true;
        }
    }
}
