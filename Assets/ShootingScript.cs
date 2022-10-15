using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField]
    private float _shotCooldown = 0.5f;


    private float shootedLastTime;

    [SerializeField]
    private float shootOffsetStrength = 1.0f;

    private List<float> shootingRecoilTimers = new List<float>();
    private List<Vector3> shootingRecoilDirections = new List<Vector3>();

    [SerializeField] private AnimationCurve animCurve;

    public GameObject projectileObject;
    // Start is called before the first frame update
    void Start()
    {
        shootedLastTime = _shotCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootingRecoilDirections.Count > 0)
        {
            Vector3 finalStrength = new Vector3(0.0f, 0.0f, 0.0f);
            for (int i = 0; i < shootingRecoilDirections.Count; i++)
            {
                shootingRecoilTimers[i] += Time.deltaTime;
                float value = animCurve.Evaluate(shootingRecoilTimers[i]);
                finalStrength += shootingRecoilDirections[i] * value * shootOffsetStrength;
                
            }
            while (shootingRecoilTimers.Count > 0 && shootingRecoilTimers[0] > 1.0f)
            {
                shootingRecoilTimers.RemoveAt(0);
                shootingRecoilDirections.RemoveAt(0);
            }
            transform.position = transform.position + finalStrength;
        }

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
                shootingRecoilDirections.Add(-direction);
                shootingRecoilTimers.Add(0.0f);
            }
        }
    }
}
