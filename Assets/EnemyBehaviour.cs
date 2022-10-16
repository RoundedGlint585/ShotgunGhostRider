using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private GameObject _player;
    Vector3 _direction;

    [SerializeField]
    float _maxSpeed = 1;

    [SerializeField]
    float _attackDistance = 0.5f;

    private bool _wasRenderedAtLeastOnce = false;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("MainCharacter");
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            animator = GetComponentInChildren<Animator>();
            _player.GetComponent<PlayerScript>().Hit();
            animator.SetTrigger("Hit");
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + 1);
            //Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if( _wasRenderedAtLeastOnce)
        {
            _wasRenderedAtLeastOnce = GetComponentInChildren<SpriteRenderer>().isVisible;
        }
        float distanceToPlayer = (transform.position - _player.transform.position).magnitude;
        if (distanceToPlayer < _attackDistance)
        {
            //_player.GetComponent<PlayerScript>().Hit();
            //probably need to add cooldown before death
            
        }
        else
        {
            Vector3 playerMovementOffset = new Vector3(-1.0f, 0.0f, 0.0f) * _player.GetComponent<PlayerScript>().GetMaxSpeed() * Time.deltaTime;
            float step = _maxSpeed * Time.deltaTime;
            Vector3 toPlayerMovement = (_player.transform.position - transform.position).normalized * step;
            transform.position = transform.position + playerMovementOffset + toPlayerMovement;
            //transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, step);
        }
        if (!GetComponentInChildren<SpriteRenderer>().isVisible && _wasRenderedAtLeastOnce)
        {
            Destroy(gameObject);
        }
    }
}
