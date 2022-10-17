using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGhostBehavior : MonoBehaviour, IKillable
{

    private GameObject _player;
    Vector3 _direction;

    [SerializeField]
    float _maxSpeed = 1;

    [SerializeField]
    int lives = 7;

    [SerializeField]
    float _attackDistance = 0.5f;

    private bool _wasRenderedAtLeastOnce = false;
    private Animator animator;
    private bool isAttacking;
    private SpriteRenderer spriteRenderer;

    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("MainCharacter");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
           

            if (!_player.GetComponent<GameProgressScript>().IsFinished())
            {
                _player.GetComponent<PlayerScript>().Hit();
                //animator.SetTrigger("Hit");
            }

            //Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + 1);
            //Destroy(gameObject);
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.color.r > 0.8f)
        {
            spriteRenderer.color -= new Color(0.01f, 0.01f, 0.01f, 0);
        }
        if (lives <= 0)
        {
            animator.SetTrigger("Death");
        }
        if (_wasRenderedAtLeastOnce)
        {
            _wasRenderedAtLeastOnce = GetComponentInChildren<SpriteRenderer>().isVisible;
        }
        float distanceToPlayer = (transform.position - _player.transform.position).magnitude;
        if (distanceToPlayer < _attackDistance)
        {
            //_player.GetComponent<PlayerScript>().Hit();
            //probably need to add cooldown before death
            if (!isAttacking)
            {
                animator.SetTrigger("Attack");
                isAttacking = true;

            }
           

        }
        else
        {
            Vector3 playerMovementOffset = new Vector3(-1.0f, 0.0f, 0.0f) * _player.GetComponent<PlayerScript>().GetMaxSpeed() * Time.deltaTime * 0.3f;
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
    public void TryDamagePlayer()
    {
        float distanceToPlayer = (transform.position - _player.transform.position).magnitude;
        if (distanceToPlayer < _attackDistance)
        {
            _player.GetComponent<PlayerScript>().Hit();
            isAttacking = false;
        }

    }
    public void KillFinally()
    {
        Destroy(gameObject);
    }

    public void Kill()
    {
        lives--;
        spriteRenderer.color = Color.white;
        Debug.Log(lives);
    }
}