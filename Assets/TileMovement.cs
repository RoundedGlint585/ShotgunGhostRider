using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{

    GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("MainCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(-1.0f, 0.0f, 0.0f) * _player.GetComponent<PlayerScript>().GetMaxSpeed() * Time.deltaTime;
        if (!GetComponent<SpriteRenderer>().isVisible)
        {
            GameObject.Find("GameField").GetComponent<FieldSpawner>().RemoveFromTileCount();
            Destroy(gameObject);
        }
    }
}
