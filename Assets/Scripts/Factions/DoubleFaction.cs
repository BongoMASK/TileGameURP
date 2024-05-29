using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoubleFaction : Faction
{
    [SerializeField] SquareTile otherParentTile;

    public override void OnPlaced() {
        base.OnPlaced();

        float rotY = model.transform.rotation.eulerAngles.y;

        switch(rotY) {
            case 0:
                Debug.Log("0");
                break;
            case 90:
                Debug.Log("90");
                break;
            case 180:
                Debug.Log("180");
                break;
            case -90:
                Debug.Log("-90");
                break;
        }
    }
}
