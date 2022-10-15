using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private GameObject _player;
    Vector3 _direction;

    [SerializeField]
    float _maxDistance = 1;

    [SerializeField]
    float _attackDistance = 0.5f;

    [SerializeField]
    float _attackCooldown = 1.0f;

    private float _lastAttackTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("MainCharacter");

    }

    // Update is called once per frame
    void Update()
    {
        _lastAttackTimer += Time.deltaTime;
        float step = _maxDistance * Time.deltaTime;
        float distanceToPlayer = (transform.position - _player.transform.position).magnitude;
        if (distanceToPlayer < _attackDistance)
        {
            _player.GetComponent<PlayerScript>().Hit();
            //probably need to add cooldown before death
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, step);
        }
    }
}
