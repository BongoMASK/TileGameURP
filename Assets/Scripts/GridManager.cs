using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] Transform tileParent;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        GameManager.Instance._tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.transform.parent = tileParent;
                spawnedTile.name = $"Tile {x} {y}";
                GameManager.Instance._tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
        foreach(var key in GameManager.Instance._tiles.Keys)
        {
            GameManager.Instance._tiles[key].SetDirectionData();
        }
    }
}
