using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
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

    public bool isFull => slotsFilled >= slotCount;

    public bool isEmpty => slotsFilled <= 0;

    public DeckFaction GetFactionFromDeck(FactionType factionType) {
        foreach (DeckFaction item in factions) {
            if(item.FactionType == factionType) {
                if(item.count > 0) {
                    item.count--;
                    return item;
                }
            }
        }

        return null;
    }
}

[System.Serializable]
public class DeckFaction {
    public FactionType FactionType;
    public int slotCount;
    public int count = 1;
}
