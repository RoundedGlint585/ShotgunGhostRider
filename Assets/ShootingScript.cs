using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ShootingScript : MonoBehaviour
{
    public enum GunType
    {
        Pistol = 1,
        Shotgun = 2,
        HeavyShotgun = 3,
        SniperRifle = 4
    }
    [SerializeField]
    public Sprite PistolSprite;
    [SerializeField]
    public Sprite ShotgunSprite;
    [SerializeField]
    public Sprite LongShotgunSprite;
    [SerializeField]
    public Sprite SniperRifleSprite;

    public int projectileCount = 1;
    [SerializeField]
    public GunType gunType;

    [SerializeField]
    public bool _canShoot = true;
    [SerializeField]
    private float _shotCooldown = 0.4f;

    public AudioClip pistol;
    public AudioClip shotgun;

    private AudioClip shot;

    public AudioSource audioSource;


    private float shootedLastTime;

    [SerializeField]
    private float shootOffsetStrength = 1.0f;

    private List<float> shootingRecoilTimers = new List<float>();
    private List<Vector3> shootingRecoilDirections = new List<Vector3>();

    [SerializeField] private AnimationCurve animCurve;

    public GameObject projectileObject;


    private GameObject playerMovementBB;


    [SerializeField]
    private float pistolOffsetStrength = 0.005f;

    [SerializeField]
    private float multipleProjectileSpread = 0.0f;

    [SerializeField]
    private int shotgunProjectileCount =3;
    [SerializeField]
    private float shootOffsetStrengthShotgun = 0.01f;
    [SerializeField]
    private float shootSpreadStrengthShotgun = 1.5f;
    [SerializeField]
    private float shootOffsetStrengthHeavyShotgun = 0.02f;
    [SerializeField]
    private float shootSpreadStrengthHeavyShotgun = 1.0f;




    private float yMin, yMax;
    private float xMin, xMax;
    // Start is called before the first frame update
    void Start()
    {
        shootedLastTime = _shotCooldown;
        playerMovementBB = GameObject.Find("PlayerMovementBB");
        gunType = UpgradeStorage.gunType;

        Vector3 max = playerMovementBB.gameObject.GetComponent<SpriteRenderer>().bounds.max;
        Vector3 min = playerMovementBB.gameObject.GetComponent<SpriteRenderer>().bounds.min;
        yMin = min.y;
        yMax = max.y;
        xMin = min.x;
        xMax = max.x;

        SetValueForType(gunType);
    }
    

    public void SetValueForType(GunType gunType)
    {
        SpriteRenderer spriteRenderer = GameObject.Find("Gun").GetComponent<SpriteRenderer>();
        Transform shootingPoint = GameObject.Find("ShootingPoint").transform;
        switch (gunType)
        {
            case GunType.Pistol:

                _shotCooldown = 0.3f;
                projectileCount = 1;
                shootOffsetStrength = pistolOffsetStrength;
                multipleProjectileSpread = 0.0f;
                spriteRenderer.sprite = PistolSprite;
                shootingPoint.localPosition = new Vector3(-0.5f, 0.05f, 0);
                shot = pistol;
                break;

            case GunType.Shotgun:
                _shotCooldown = 0.5f;
                shootOffsetStrength = shootOffsetStrengthShotgun;
                multipleProjectileSpread = shootSpreadStrengthShotgun;
                projectileCount = shotgunProjectileCount+2;
                spriteRenderer.sprite = ShotgunSprite;
                shootingPoint.localPosition = new Vector3(-0.2f, 0.05f, 0);
                shot = shotgun;
                break;

            case GunType.HeavyShotgun:
                _shotCooldown = 0.5f;
                shootOffsetStrength = shootOffsetStrengthHeavyShotgun;
                multipleProjectileSpread = shootSpreadStrengthHeavyShotgun;
                projectileCount = shotgunProjectileCount;
                spriteRenderer.sprite = LongShotgunSprite;
                shootingPoint.localPosition = new Vector3(0f, 0.05f, 0);
                shot = shotgun;
                break;

            case GunType.SniperRifle:
                //TODO: SniperRifle LOgic
                _shotCooldown = 0.4f;
                projectileCount = 1;
                spriteRenderer.sprite = SniperRifleSprite;
                shootingPoint.localPosition = new Vector3(0.1f, 0.05f, 0);
                break;
                              
        
        }

    }

    // Update is called once per frame
    void Update()
    {
        //SetValueForType(gunType);
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


            Vector3 resolvedPosition = transform.position + finalStrength;
            resolvedPosition.x = Math.Min(xMax, resolvedPosition.x);
            resolvedPosition.x = Math.Max(xMin, resolvedPosition.x);
            resolvedPosition.y = Math.Min(yMax, resolvedPosition.y);
            resolvedPosition.y = Math.Max(yMin, resolvedPosition.y);
            transform.position = resolvedPosition;
        }

        shootedLastTime += Time.deltaTime;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;
        GameObject shootingPoint = GameObject.Find("ShootingPoint");
        Vector3 currentPosition = shootingPoint.transform.position;
        Vector3 direction = (mousePosition - currentPosition).normalized;
        if (Input.GetMouseButtonDown(0) && _canShoot)
        {
            if (shootedLastTime > _shotCooldown)
            {
                audioSource.PlayOneShot(shot);
                for (int i = 0; i < projectileCount; i++)
                {
                    GameObject projectile;
                    projectile = Instantiate(projectileObject);
                    projectile.transform.position = currentPosition;
                    float angle = Random.Range(-10f* multipleProjectileSpread, 10f * multipleProjectileSpread);
                    Quaternion quaternion = Quaternion.Euler(0, 0, angle);
                    Vector3 randomizedDirection = quaternion * direction;
                    randomizedDirection.z = 0.0f;
                    projectile.GetComponent<ProjectileBehaviour>().SetDirection(randomizedDirection);
                    shootedLastTime = 0.0f;
                    shootingRecoilDirections.Add(-randomizedDirection);
                    shootingRecoilTimers.Add(0.0f);
                }
            }
        }
    }
}
