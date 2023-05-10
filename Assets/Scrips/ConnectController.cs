using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ConnectController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Button ButtonConnect;

    private void Reset()
    {
        ButtonConnect = GameObject.Find("ButtonConnect").GetComponent<Button>();
    }

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        ButtonConnect = GameObject.Find("ButtonConnect").GetComponent<Button>();
        ButtonConnect.onClick.AddListener(Connect);
    }

    void SetButton(bool state, string msg)
    {
        ButtonConnect.GetComponentInChildren<TMP_Text>().text = msg;
        ButtonConnect.GetComponent<Button>().enabled = state;
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
            SetButton(false, "FINDING MATCH...");

        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            SetButton(false, "CONNECTING...");

        }
    }

    #region MonoBehaviourPunCallbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        SetButton(true, "LETS BATTLE");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(":OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Se creo un cuarto");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
        SetButton(false, "WAITING PLAYERS");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Se Ha unido al cuarto, Players: " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Match");
        }
    }

    #endregion

}
