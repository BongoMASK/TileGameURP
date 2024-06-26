using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text messageText;
    [SerializeField] TMP_Text playerListText;

    [SerializeField] Color32[] teamColors;


    public override void OnEnable() {
        base.OnEnable();

        messageText.text = "Finding Game...";
        playerListText.text = "";

        if (PhotonNetwork.IsConnected) {
            Disconnect();
            return;
        }

        Invoke(nameof(ConnectToMaster), 2);
    }

    private void Start() {
        PhotonNetwork.AutomaticallySyncScene = true; 
    }

    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift))
            if (Input.GetKeyDown(KeyCode.Space))
                StartGame();
    }

    public void ConnectToMaster() {
        messageText.text = "Connecting...";
        PhotonNetwork.ConnectUsingSettings();

        //PhotonNetwork.NickName = inputFieldText.text;
        string randPlayerName = "Player #" + Random.Range(0, 1000).ToString();
        PhotonNetwork.NickName = PlayerPrefs.GetString("playerName", randPlayerName);
        PlayerPrefs.SetString("playerName", PhotonNetwork.NickName);
    }

    public override void OnConnectedToMaster() {
        base.OnConnectedToMaster();

        messageText.text = "Connected To Master";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("test", null, null);
        messageText.text = "Lobby Created";
    }

    public override void OnJoinedRoom() {
        base.OnJoinedRoom();

        messageText.text = PhotonNetwork.MasterClient.NickName + "'s Lobby\nWaiting For Players...";

        UpdatePlayerList();
        //GameObject player = PhotonNetwork.Instantiate(playerPath, Vector3.zero, Quaternion.identity);
    }

    public override void OnDisconnected(DisconnectCause cause) {
        base.OnDisconnected(cause);

        Debug.Log(cause);
        messageText.text = cause.ToString();

        Invoke(nameof(BackToMainMenu), 2);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {

        for (int i = 0; i < roomList.Count; i++) {
            Debug.Log(roomList[i].Name);
        }
    }



    public void StartGame() {
        PhotonNetwork.LoadLevel(1);
        Debug.Log("Game Started");
    }

    public void UpdatePlayerList() {
        string playerList = "<size=100%>Player List<size=80%>\n\n";

        int i = 0;
        foreach (Player item in PhotonNetwork.PlayerList) {
            string col = "<color=#" + ColorUtility.ToHtmlStringRGBA(teamColors[i]) + ">";

            playerList += item.NickName;
            playerList += "\n";

            SetPlayerCustomProps(item, i);

            i++;
        }

        playerListText.text = playerList;
    }

    public void Disconnect() {
        PhotonNetwork.Disconnect();
    }

    private void SetPlayerCustomProps(Player player, int i) {
        player.SetTeam(i);
        player.SetIsEmptyDeck(false);
    }

    public void DoTransitionAnim() {
        MenuManager.instance.DoTransitionAnimIn();
    }

    public void BackToMainMenu() {
        MenuManager.instance.OpenMenu("main");
    }


    #region Monobehaviour Pun Callbacks

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        base.OnPlayerEnteredRoom(newPlayer);

        UpdatePlayerList();

        if (!PhotonNetwork.IsMasterClient)
            return;

        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2) {
            messageText.text = "Starting Game...";
            Invoke(nameof(DoTransitionAnim), 2);
            Invoke(nameof(StartGame), 5);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        base.OnPlayerLeftRoom(otherPlayer);

        UpdatePlayerList();
    }

    #endregion
}
