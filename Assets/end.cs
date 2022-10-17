using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class end : MonoBehaviour
{
    private int timer = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
    }
    public void LoadStartLevel()
    {
        if (timer > 300)
        {
            SceneManager.LoadScene(0);
        }
     
    }
}
