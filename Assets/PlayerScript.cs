using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private int _lifeCount = 4;

    private bool _isDead = false;

    public int GetLifes()
    {
        return _lifeCount;
    }

    public int Hit()
    {
        _lifeCount -= 1;
        if(_lifeCount == 0)
        {
            _isDead = true;
        }
        return _lifeCount;
    }

    public bool IsDead()
    {
        return _isDead;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
