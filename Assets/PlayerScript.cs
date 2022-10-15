using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private int _maxLifeCount = 4;

    private int _lifeCount = 4;

    [SerializeField]
    private float _maxSpeed = 1.0f;

    private bool _isDead = false;


    public int GetMaxLifesCount()
    {
        return _maxLifeCount;
    }
    public int GetLifes()
    {
        return _lifeCount;
    }

    public float GetMaxSpeed()
    {
        return _maxSpeed;
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
        _lifeCount = _maxLifeCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
