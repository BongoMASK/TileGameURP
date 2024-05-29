using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public Color[] colors; 
    [SerializeField] public int colorIndex = 0;

    public Dictionary<Vector2, Tile> _tiles;

    private void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this);

        else 
            Instance = this;
    }

    public void PickColour(int i) {
        colorIndex = i;
    }
}
