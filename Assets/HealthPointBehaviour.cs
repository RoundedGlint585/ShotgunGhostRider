using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthPointBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite fullLife;
    public Sprite emptyLife;

    public void DisableLife()
    {
        GetComponent<Image>().sprite = emptyLife;
    }

    public void EnableLife()
    {
        gameObject.GetComponent<Image>().sprite = fullLife;
    }
    void Start()
    {
        GetComponent<Image>().sprite = fullLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
