using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int maxTileCount = 3;

    [SerializeField]
    private int tileSize = 10;

    [SerializeField]
    GameObject[] tiles;

    [SerializeField]
    private float _movementStrength = 1.0f;

    private int currentTileCount = 0;

    public void RemoveFromTileCount() {
        currentTileCount--;
    }

    public float GetMovementStrength()
    {
        return _movementStrength;
    }
    GameObject lastTile;

    void Start()
    {

        for (int i = -(maxTileCount / 2)-1; i <= (maxTileCount / 2)+1; i++)
        {
            
            lastTile = Instantiate(tiles[Random.Range(0, tiles.Length)], transform);
            lastTile.transform.position = new Vector3(i * tileSize, transform.position.y, 0);
            lastTile.transform.localScale = new Vector3(tileSize, tileSize, 1);
            lastTile.layer = LayerMask.NameToLayer("Level");
        }
        currentTileCount = maxTileCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (currentTileCount < maxTileCount)
        {
            GameObject NewTile;
            NewTile = Instantiate(tiles[Random.Range(0, tiles.Length)], transform);
            NewTile.transform.position = new Vector3(lastTile.transform.position.x + tileSize, lastTile.transform.position.y, 0.0f);
            NewTile.transform.localScale = new Vector3(tileSize, tileSize, 1);
            NewTile.layer = LayerMask.NameToLayer("Level");
            currentTileCount++;
            lastTile = NewTile;
            
        }
    }
}
