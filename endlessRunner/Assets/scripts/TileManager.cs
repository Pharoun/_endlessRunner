using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<GameObject> activeTiles;
    public GameObject[] tilePrefabs;
    public int numberOfTiles = 2;
    public float zSpawn = 652.6f;
    public Transform playerTransform;
    private int previousIndex = 0;

    void Start()
    {
        SpawnTile(previousIndex);
    }

    void Update()
    {
        if (playerTransform.position.z + 900 >= zSpawn)
        {
            if (previousIndex == 0)
                previousIndex = 1;
            else
                previousIndex = 0;

            SpawnTile(previousIndex);
        }
    }

    public void SpawnTile(int index)
    {
        GameObject tile = tilePrefabs[index];

        if (tile.activeInHierarchy)
            tile = tilePrefabs[index];

        tile.transform.position = new Vector3(0, 0, zSpawn);
        //tile.transform.Rotate(0, 0, 0);

        zSpawn += 652.6f;
    }
}