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
    GameObject tile;

    private int currentTileCount = 0;

    public void RemoveFromTileCount() {
        currentTileCount--;
    }


    GameObject lastTile;

    void Start()
    {

        for (int i = -(maxTileCount / 2)-1; i <= (maxTileCount / 2)+1; i++)
        {
            lastTile = Instantiate(tile, transform);
            lastTile.transform.position = new Vector3(i * tileSize, 0, 0);
            lastTile.GetComponentInChildren<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
            lastTile.transform.localScale = new Vector3(tileSize, tileSize, 1);
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
            NewTile = Instantiate(tile, transform);
            NewTile.transform.position = new Vector3(lastTile.transform.position.x + tileSize, 0.0f, 0.0f);
            lastTile.GetComponentInChildren<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
            lastTile.transform.localScale = new Vector3(tileSize, tileSize, 1);
            currentTileCount++;
            lastTile = NewTile;
        }
    }
}
