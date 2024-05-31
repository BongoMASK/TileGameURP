using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    public int slotCount = 12;

    public List<DeckFaction> factions = new List<DeckFaction>();

    public int slotsFilled {
        get {
            int count = 0;

            foreach (DeckFaction item in factions)
                count += item.slotCount;

            return count;
        }
    }

    public int factionCount {
        get {
            int count = 0;

            foreach (DeckFaction item in factions) {
                count += item.count;
            }

            if(count <= 0)
                PhotonNetwork.LocalPlayer.SetIsEmptyDeck(true);

            return count;
        }
    }

    public bool isFull => slotsFilled >= slotCount;

    public bool isEmpty => factionCount <= 0;

    public bool CanTakeFactionFromDeck(FactionType factionType) {
        foreach (DeckFaction item in factions) {
            if (item.FactionType == factionType) {
                if (item.count > 0) {
                    //item.count--;
                    return true;
                }
            }
        }

        return false;
    }

    public void ReduceFactionCount(FactionType factionType) {
        foreach (DeckFaction item in factions) {
            if (item.FactionType == factionType) {
                if (item.count > 0) {
                    item.count--;
                    Debug.Log(factionCount + " slots left");
                }
                else {
                    Debug.Log("fjkdfl");
                }
            }
        }
    }
}

[System.Serializable]
public class DeckFaction {
    public FactionType FactionType;
    public int slotCount;
    public int count = 1;
}