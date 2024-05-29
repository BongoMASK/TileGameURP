using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public Tile[] borders;

    public enum TileBorder {
        up = 0,
        down,
        left,
        right
    }

    enum TileState {
        red = 0,
        blue,
        white
    }

    // try to use events and scriptable objects

    [SerializeField] TileState tileState = TileState.white;

    // Checking how many consecutive same coloured tiles are there
    int GetPushPower(TileState firstTileState, TileBorder b) {
        if (firstTileState == TileState.white || firstTileState != tileState)
            return 0;

        if (borders[(int)b] == null)
            return 1;

        return 1 + borders[(int)b].GetPushPower(firstTileState, b);
    }

    // Checking the next tile that is coloured
    Tile TraverseTile(TileBorder b) {
        if (tileState != TileState.white)
            return this;

        if (borders[(int)b] == null)
            return null;

        Debug.Log(borders[(int)b].name);
        return borders[(int)b].TraverseTile(b);
    }

    private void OnMouseEnter() {
        _highlight.SetActive(true);
    }

    private void OnMouseOver() {
        // Don't play a move if its not player
        if (GameManager.Instance.colorIndex != (int)tileState)
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            // Find consecutive same coloured tiles for pushing power
            int p = GetPushPower(tileState, TileBorder.right);

            // Continue until pushpower is not finished
            while (p-- > 0)
                TraverseTile(TileBorder.right).Push_Right();

            SwitchTurn();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            // Find consecutive same coloured tiles for pushing power
            int p = GetPushPower(tileState, TileBorder.left);

            // Continue until pushpower is not finished
            while (p-- > 0)
                TraverseTile(TileBorder.left).Push_Left();
            SwitchTurn();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            // Find consecutive same coloured tiles for pushing power
            int p = GetPushPower(tileState, TileBorder.up);

            // Continue until pushpower is not finished
            while (p-- > 0)
                TraverseTile(TileBorder.up).Push_Up();
            SwitchTurn();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            // Find consecutive same coloured tiles for pushing power
            int p = GetPushPower(tileState, TileBorder.down);

            // Continue until pushpower is not finished
            while (p-- > 0)
                TraverseTile(TileBorder.down).Push_Down();
            SwitchTurn();
        }
    }

    private void OnMouseExit() {
        _highlight.SetActive(false);
    }

    // Applying colour to white tile
    private void OnMouseDown() {
        // Don't colour tile if its already coloured
        if (tileState != TileState.white)
            return;

        ChangeTileToColour((TileState)GameManager.Instance.colorIndex);
        SwitchTurn();
    }

    void ChangeTileToColour(TileState thisTileState) {
        tileState = thisTileState;
        _renderer.color = GameManager.Instance.colors[(int)thisTileState];
    }

    // Sets borders for the tile
    public void SetDirectionData() {
        Vector2 tilePos = transform.position;

        borders[(int)TileBorder.up] = GetTileAtPos(tilePos + new Vector2(0, 1));        // up
        borders[(int)TileBorder.down] = GetTileAtPos(tilePos + new Vector2(0, -1));     // down
        borders[(int)TileBorder.left] = GetTileAtPos(tilePos + new Vector2(-1, 0));     // left
        borders[(int)TileBorder.right] = GetTileAtPos(tilePos + new Vector2(1, 0));     // right
    }

    Tile GetTileAtPos(Vector2 pos) {
        if (GameManager.Instance._tiles.ContainsKey(pos))
            return GameManager.Instance._tiles[pos];
        return null;
    }

    void Push_Right() {
        Push(TileBorder.right, TileBorder.left);

        // Change tile that was originally pushed to white
        // Leaving this line out causes duplicates
        ChangeTileToColour(TileState.white);
    }

    void Push_Left() {
        Push(TileBorder.left, TileBorder.right);

        // Change tile that was originally pushed to white
        // Leaving this line out causes duplicates
        ChangeTileToColour(TileState.white);
    }

    void Push_Up() {
        Push(TileBorder.up, TileBorder.down);

        // Change tile that was originally pushed to white
        // Leaving this line out causes duplicates
        ChangeTileToColour(TileState.white);
    }

    void Push_Down() {
        Push(TileBorder.down, TileBorder.up);

        // Change tile that was originally pushed to white
        // Leaving this line out causes duplicates
        ChangeTileToColour(TileState.white);
    }

    void Push(TileBorder b1, TileBorder b2) {
        // if there are no tiles left
        if (borders[(int)b1] == null) {
            ChangeTileToColour(borders[(int)b2].tileState);
            return;
        }

        // if tile is not white, check next tile
        if (tileState != TileState.white)
            borders[(int)b1].Push(b1, b2);

        // If null, delete the current tile
        // special case for when a tile is at the edge of the board and wants to go inwards
        // Gives error coz it has nothing to copy a tilestate from
        if (borders[(int)b2] == null) {
            ChangeTileToColour(TileState.white);
            return;
        }
        // shift previous tile to current tile if null
        ChangeTileToColour(borders[(int)b2].tileState);
    }

    private static void SwitchTurn() {
        if (GameManager.Instance.colorIndex == 0) {
            GameManager.Instance.colorIndex = 1;
            return;
        }
        GameManager.Instance.colorIndex = 0;
    }
}