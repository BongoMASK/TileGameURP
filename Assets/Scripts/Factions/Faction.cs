using DG.Tweening;
using UnityEngine;

public class Faction : MonoBehaviour
{

    [Header("Data")]

    [SerializeField] SquareTile _parentTile;
    public SquareTile parentTile {
        get { return _parentTile; }
        private set { 
            _parentTile = value;

            if (_parentTile != null)
                DoPushAnim();
        }
    }

    [SerializeField] int pushCount = 1;

    [SerializeField] private Team _team;
    public Team team {
        get { return _team; }
        set {
            _team = value;
            ChangeTeamMaterial();
        }
    }

    public Vector3 offset { get; private set; } = new Vector3(0, 0f, 0);

    [Header("Assignables")]

    [SerializeField] private Outline outline;
    [SerializeField] protected Transform model;


    public static int id = 0;
    public int factionID;


    #region Gameplay

    /// <summary>
    /// Called when this faction has initiated a push
    /// </summary>
    /// <param name="pushCount"></param>
    /// <param name="direction"></param>
    public virtual void Push(int pushCount, Border direction) {
        parentTile.Push(pushCount, direction);
    }

    /// <summary>
    /// Called when this faction has been pushed
    /// Can be used if you want some different functionality for when a faction has been pushed
    /// </summary>
    /// <param name="direction"></param>
    public virtual bool OnPushed(Border direction) {
        return parentTile.Push(direction, Borders.GetOppDirection(direction));
    }

    public virtual void InitiatePush(Border direction) {
        // Turn Animation
        int rot = 0;
        switch (direction) {
            case Border.Top:
                rot = 0;
                break;

            case Border.Down:
                rot = 180;
                break;

            case Border.Right:
                rot = 90;
                break;

            case Border.Left:
                rot = -90;
                break;
        }

        Vector3 rotation = new Vector3(0, rot, 0);
        model.DORotate(rotation, 0.5f).SetEase(Ease.OutSine).OnComplete(() => {
            model.DORotate(rotation, 0.1f).OnComplete(() => Push(pushCount, direction));
        });

        
    }

    public virtual void OnPlaced() {
        // play particle effect
    }

    public void MoveFaction(SquareTile newParent) {
        if (parentTile != null)
            parentTile.occupiedFaction = null;

        parentTile = newParent;
        newParent.occupiedFaction = this;
    }

    public void Die(Border border) {
        parentTile.occupiedFaction = null;
        DoPushAnim(border);
        Invoke(nameof(DoFallAnim), 0.4f);
        Destroy(gameObject, 2.4f);
    }

    #endregion


    #region Animations

    public virtual void DoPushAnim() {
        transform.DOMove(parentTile.transform.position + offset, 0.5f).SetEase(Ease.OutSine);
    }

    public virtual void DoPushAnim(Border border) {
        Vector3 dir = Vector3.zero;
        switch (border) {
            case Border.Left:
                dir = Vector3.left;
                break;
            case Border.Right:
                dir = Vector3.right;
                break;
            case Border.Top:
                dir = Vector3.forward;
                break;
            case Border.Down:
                dir = Vector3.down;
                break;
        }

        transform.DOMove(transform.position + dir, 0.5f).SetEase(Ease.OutSine);
    }

    public virtual void DoFallAnim() {
        transform.DOMoveY(-20, 1.5f).SetEase(Ease.InSine);
    }

    #endregion


    #region Faction Appearance

    public void OnMouseDown() {
        FactionSelectionManger.instance.selectedFaction = this;
    }

    public void HighlightFaction(bool enabled) {
        outline.enabled = enabled;
    }

    private void ChangeTeamMaterial() {
        switch (team) {
            case Team.Red:
                break;

            case Team.Blue:
                break;
        }
    }

    #endregion


    #region Faction ID

    public static Faction FindFaction(int id) {
        if (id < 0)
            return null;

        foreach (var item in FindObjectsOfType<Faction>()) {
            if (item.factionID == id) return item;
        }

        Debug.LogError("Faction with id: " + id + " does not exist or has been destroyed");
        return null;
    }

    #endregion
}

public enum Team {
    Red,
    Blue
}

public enum FactionType {
    King,
    Troop,
    Double,
    Hole,
    Wall
}
