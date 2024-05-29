using UnityEngine;

public enum MoveType {
    Move,
    Attack,
    Swap,
    Finish
}

public class PlayerMove
{
    //public int cardID;
    //public int cardPlacerID;
    //public MoveType moveType;

    //public PlayerMove(int cardID, int cardPlacerID, MoveType moveType) {
    //    this.cardID = cardID;
    //    this.cardPlacerID = cardPlacerID;
    //    this.moveType = moveType;
    //}

    //public object[] ToByteArray() {
    //    return new object[] { cardID, cardPlacerID, moveType };
    //}

    //public static PlayerMove ToPlayerMove(object[] objs) {
    //    return new PlayerMove((int)objs[0], (int)objs[1], (MoveType)objs[2]);
    //}

    //public void Print() {
    //    string msg = "CardID: " + cardID + ", " + "CardPlacerID: " + cardPlacerID + ", " + "Movetype: " + moveType;
    //    Debug.Log(msg);
    //}


    public int factionID;
    public Border direction;

    public PlayerMove(int factionID, Border moveType) {
        this.factionID = factionID;
        this.direction = moveType;
    }

    public object[] ToByteArray() {
        return new object[] { factionID, direction };
    }

    public static PlayerMove ToPlayerMove(object[] objs) {
        return new PlayerMove((int)objs[0], (Border)objs[1]);
    }

    public void Print() {
        string msg = "Faction: " + factionID + ", " + "Movetype: " + direction;
        Debug.Log(msg);
    }
}

public class PlayerPieceCreate {
    public int factionID;
    public int tileID;
    public FactionType factionType;

    public PlayerPieceCreate(int factionID, int tileID, FactionType factionType) {
        this.factionID = factionID;
        this.tileID = tileID;
        this.factionType = factionType;
    }

    public object[] ToByteArray() {
        return new object[] { factionID, tileID, factionType };
    }

    public static PlayerPieceCreate ToPlayerPieceCreate(object[] objs) {
        return new PlayerPieceCreate((int)objs[0], (int)objs[1], (FactionType)objs[2]);
    }

    public void Print() {
        string msg = "Faction: " + factionID + ", Tile: " + tileID +  ", Factiontype: " + factionType;
        Debug.Log(msg);
    }
}
