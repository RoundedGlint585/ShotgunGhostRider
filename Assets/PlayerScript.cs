using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ShootingScript;

public static  class  UpgradeStorage
{
    public static GunType gunType = GunType.Pistol;
    public static int healthPoints = 4; 

}
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

    GameObject fadeOutScreen;
    public float fadeOutTime = 1.0f;
    public float changeImageTimer = 0.0f;

    public Sprite deadPlayer;




    // end level variables

    public float endLevelTime = 1.0f;
    public float endLevelTimer = 0.0f;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }
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

    public void RestoreHealth()
    {
        UpgradeStorage.healthPoints = _maxLifeCount;
        SceneManager.LoadScene(1);
    }

    public void GetHeavyShotgun()
    {
        UpgradeStorage.gunType = GunType.HeavyShotgun;
        SceneManager.LoadScene(1);
    }

    public void GetShotgun()
    {
        UpgradeStorage.gunType = GunType.Shotgun;
        SceneManager.LoadScene(1);
    }
    public int Hit()
    {
        if (_lifeCount > 0)
        {
            _lifeCount -= 1;
        }
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
        _lifeCount = UpgradeStorage.healthPoints;
        fadeOutScreen = GameObject.Find("FadeOutScreen");

        GameObject EndLevelSprite = GameObject.Find("EndLevelSprite");
        if (!isMainMenu)
        {
            Button ShotgunBtn = EndLevelSprite.transform.GetChild(0).GetComponent<Button>();
            Button HeavyShotgunBtn = EndLevelSprite.transform.GetChild(1).GetComponent<Button>();
            Button Health = EndLevelSprite.transform.GetChild(2).GetComponent<Button>();
            ShotgunBtn.onClick.AddListener(GetShotgun);
            HeavyShotgunBtn.onClick.AddListener(GetHeavyShotgun);
            Health.onClick.AddListener(RestoreHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (needToMove)
        {
            //UpgradeStorage.gunType = GunType.Pistol;
            transform.position = transform.position + new Vector3(1, 0, 0) * _internalSpeed * Time.deltaTime;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
            if (!transform.GetChild(0).gameObject.GetComponent < SpriteRenderer>().isVisible){
                SceneManager.LoadScene(1);
            }
        }   

        if(_isDead)
        {
            fadeOutScreen.GetComponent<SpriteRenderer>().color = fadeOutScreen.GetComponent<SpriteRenderer>().color + new Color(0, 0, 0, 1.0f) * fadeOutTime * Time.deltaTime;
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = false;
            GetComponent<ShootingScript>()._canShoot = false;
            _maxSpeed = 0.0f;
            if(fadeOutScreen.GetComponent<SpriteRenderer>().color.a > 1.0)
            {
                changeImageTimer += Time.deltaTime;
                if(changeImageTimer > 1.0f)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = deadPlayer;
                    transform.GetChild(2).transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                }
                if(changeImageTimer > 1.2f)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        SceneManager.LoadScene(0);
                    }
                }
            }
        }


        if (GetComponent<GameProgressScript>().IsFinished())
        {

            GameObject EndLevelSprite = GameObject.Find("EndLevelSprite");
            Button ShotgunBtn = EndLevelSprite.transform.GetChild(0).GetComponent<Button>();
            Button HeavyShotgunBtn = EndLevelSprite.transform.GetChild(1).GetComponent<Button>();
            Button Health = EndLevelSprite.transform.GetChild(2).GetComponent<Button>();
            Color currentColor = EndLevelSprite.GetComponent<Image>().color;
            EndLevelSprite.GetComponent<Image>().color = currentColor + new Color(0, 0, 0, 1.0f) * fadeOutTime * Time.deltaTime;
            if (EndLevelSprite.GetComponent<Image>().color.a > 1.0)
            {
                if (!EndLevelSprite.transform.GetChild(0).gameObject.activeSelf)
                {
                    EndLevelSprite.transform.GetChild(0).gameObject.SetActive(true);
                    EndLevelSprite.transform.GetChild(1).gameObject.SetActive(true);
                    EndLevelSprite.transform.GetChild(2).gameObject.SetActive(true);
                    GetComponent<ShootingScript>()._canShoot = false;
                    
                }
               
            }
        }
    }
}
