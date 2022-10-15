using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    public Slider slider;
    public GameObject _player;
    void Start()
    {
        _player = GameObject.Find("MainCharacter");
        slider.maxValue = _player.GetComponent<GameProgressScript>().GetLevelLength();
        slider.value = 0.0f;
    }

    void Update()
    {
        slider.value = _player.GetComponent<GameProgressScript>().GetCurrentTime();
    }

}