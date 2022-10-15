using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private Vector3 _direction;

    [SerializeField]
    float _maxSpeed = 100.0f;
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody2D>().isKinematic = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + _direction * Time.deltaTime * _maxSpeed;
    }
}
