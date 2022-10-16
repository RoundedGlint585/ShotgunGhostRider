using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private int _maxLifeCount = 4;

    private int _lifeCount = 4;

    [SerializeField]
    private float _maxSpeed = 1.0f;

    [SerializeField]
    private float _internalSpeed = 1.0f; // only for main menu

    private bool _isDead = false;

    public bool needToMove = false;
    public bool isMainMenu = false;
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
    public void SetMaxSpeed(float value)
    {
        _maxSpeed = value;
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
        if (needToMove)
        {
            transform.position = transform.position + new Vector3(1, 0, 0) * _internalSpeed * Time.deltaTime;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
            if (!transform.GetChild(0).gameObject.GetComponent < SpriteRenderer>().isVisible){
                SceneManager.LoadScene(1);
            }
        }   
    }
}
