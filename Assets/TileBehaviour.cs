using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{

    GameObject _player;
    // Start is called before the first frame update
    private bool _wasRenderedAtLeastOnce = false;

    private float _movementStrength = 1.0f;
    void Start()
    {
        _player = GameObject.Find("MainCharacter");
        _movementStrength = GetComponentInParent<FieldSpawner>().GetMovementStrength();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(-1.0f, 0.0f, 0.0f) * _player.GetComponent<PlayerScript>().GetMaxSpeed() * Time.deltaTime * _movementStrength;
        if (GetComponent<SpriteRenderer>().isVisible)
        {
            _wasRenderedAtLeastOnce = true;
        }

        if (!GetComponent<SpriteRenderer>().isVisible && _wasRenderedAtLeastOnce)
        {
            transform.parent.gameObject.GetComponent<FieldSpawner>().RemoveFromTileCount();
            Destroy(gameObject);
        }
    }
}
