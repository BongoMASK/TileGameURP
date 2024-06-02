using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class SquareTile : MonoBehaviour
{
    public Borders borders;

    public Faction occupiedFaction;

    public bool isEmpty => occupiedFaction == null;

    public int tileID { get; private set; }

    private void Awake() {
        tileID = (int)(transform.position.z * 10 + transform.position.x);
        //SetUpAllBorders();
    }


    #region Pushing

    public void Push(int count, Border pushBorder) {
        while (count-- > 0) {
            // Done when it cannot find any other border at all
            // Usually gives a null exception when push count is over a certain value
            if (TraverseTiles(pushBorder) == null)
                return;

            SquareTile squareTile = TraverseTiles(pushBorder).borders.GetBorder(pushBorder);

            if (squareTile != null) {
                if (squareTile.isEmpty)
                    squareTile.Push(pushBorder, Borders.GetOppDirection(pushBorder));
                else
                    squareTile.occupiedFaction.OnPushed(pushBorder);
            }
            else
                TraverseTiles(pushBorder).occupiedFaction.Die(pushBorder);
        }
    }

    public bool Push(Border pushBorder, Border oppositeBorder) {

        // If cannot find a border after this
        if (borders.GetBorder(pushBorder) == null) {
            // If it isnt empty, kill the faction occupying the tile
            if(!isEmpty)
                occupiedFaction.Die(pushBorder);

            // Make previous faction come here
            borders.GetBorder(oppositeBorder).occupiedFaction?.MoveFaction(this);
            return true;
        }

        //if(borders.GetBorder(pushBorder).isOuterMostRing && !isEmpty) {
        //    occupiedFaction.MoveFaction(borders.GetBorder(pushBorder));
        //    return;
        //}

        bool pushSuccess = true;

        // if a faction is occupying it, then push the next tile as well
        if (!isEmpty)
            pushSuccess = borders.GetBorder(pushBorder).OnPushed(pushBorder);

        // If there is no opposite border, then dont move that guy here
        if (borders.GetBorder(oppositeBorder) == null) {
            // May have some bug here
            return pushSuccess;
        }

        // Move previous faction to this place
        if (pushSuccess)
            borders.GetBorder(oppositeBorder).occupiedFaction?.MoveFaction(this);

        return pushSuccess;
    }

    public bool OnPushed(Border pushBorder) {
        if (isEmpty)
            return Push(pushBorder, Borders.GetOppDirection(pushBorder)); 

        return occupiedFaction.OnPushed(pushBorder);
    }

    #endregion

    public SquareTile TraverseTiles(Border border) {
        // If it is not empty, then this is the tile you want
        if (!isEmpty)
            return this;

        // If the border is the edge, dont do anything
        if (borders.GetBorder(border) == null)
            return null;

        // Return the next Border
        return borders.GetBorder(border).TraverseTiles(border);
    }

    /// <summary>
    /// Used once to just set up the borders in the inspector
    /// Copy the thing in play mode and paste it to make it work
    /// </summary>
    private void SetUpAllBorders() {
        //if (isOuterMostRing)
        //    Destroy(gameObject);

        SquareTile[] squareTiles = FindObjectsOfType<SquareTile>();

        foreach (var item in squareTiles) {
            if(item.transform.position == transform.position + Vector3.right) {
                borders.rightBorder = item;
            }

            if (item.transform.position == transform.position + Vector3.left) {
                borders.leftBorder = item;
            }

            if (item.transform.position == transform.position + Vector3.forward) {
                borders.topBorder = item;
            }

            if (item.transform.position == transform.position + Vector3.back) {
                borders.downBorder = item;
            }
        }
    }

    public void Die() {
        transform.DOMoveY(-200, 2).SetEase(Ease.InOutBack);
        Destroy(gameObject, 2);
    }


    #region Placement

    private void OnMouseOver() {
        PlacementManager.instance.ChangeFactionPos(this);
    }

    private void OnMouseDown() {
        PlacementManager.instance.PlaceFaction(this);
    }

    #endregion

    #region Tile ID

    public static SquareTile FindTile(int id) {
        if (id < 0)
            return null;

        foreach (var item in FindObjectsOfType<SquareTile>()) {
            if (item.tileID == id) return item;
        }

        Debug.LogError("Tile with id: " + id + " does not exist or has been destroyed");
        return null;
    }

    #endregion
}

[System.Serializable]
public class Borders {

    // 0 - top  1 - down    2 - left    3 - right
    public List<SquareTile> borders = new List<SquareTile>(4);

    public SquareTile topBorder {
        get => borders[0];
        set { borders[0] = value; }
    }
    public SquareTile downBorder {
        get => borders[1];
        set { borders[1] = value; }
    }
    public SquareTile leftBorder {
        get => borders[2];
        set { borders[2] = value; }
    }
    public SquareTile rightBorder {
        get => borders[3];
        set { borders[3] = value; }
    }

    public SquareTile GetBorder(Border border) {
        switch (border) {
            case Border.Left:
                return leftBorder;

            case Border.Right:
                return rightBorder;

            case Border.Top:
                return topBorder;

            case Border.Down:
                return downBorder;

            default:
                return null;
        }
    }

    public static Border GetOppDirection(Border border) {
        switch (border) {
            case Border.Left:
                return Border.Right;

            case Border.Right:
                return Border.Left;

            case Border.Top:
                return Border.Down;

            case Border.Down:
                return Border.Top;

            default:
                return Border.Top;
        }
    }
}

public enum Border {
    Top,
    Down,
    Left,
    Right
}