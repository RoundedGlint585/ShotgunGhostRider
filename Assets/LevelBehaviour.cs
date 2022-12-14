using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelBehaviour : MonoBehaviour
{
    public Button button;

    public GameObject player;

    void Start()
    {
        button = transform.GetChild(0).gameObject.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;
        GetComponent<AudioSource>().Play();
        player.GetComponent<PlayerScript>().needToMove = true;
        player.GetComponent<PlayerScript>().SetMaxSpeed(0.0f);
    }

}
