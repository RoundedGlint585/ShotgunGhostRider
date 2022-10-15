using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField]
    private float _shotCooldown = 0.5f;


    private float shootedLastTime;

    public GameObject projectileObject;
    // Start is called before the first frame update
    void Start()
    {
        shootedLastTime = _shotCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        shootedLastTime += Time.deltaTime;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;
        Vector3 currentPosition = transform.position;
        Vector3 direction = (mousePosition - currentPosition).normalized;
        if (Input.GetMouseButtonDown(0))
        {
            if (shootedLastTime > _shotCooldown)
            {
                GameObject projectile;
                projectile = Instantiate(projectileObject, transform);
                projectile.GetComponent<ProjectileBehaviour>().SetDirection(direction);
                shootedLastTime = 0.0f;
               
            }
        }
    }
}
