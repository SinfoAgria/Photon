using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;
    public GameObject player;
    public Transform[] spawnPoints;
    public GameObject roomCam;

    private string nickcame = "Unnamed";

    public GameObject nameUI;
    public GameObject connectingUI;

    void Awake()
    {
        instance = this;
    }

    public void ChangeNickname(string _name)
    {
        nickcame = _name;
    }

    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();

        nameUI.SetActive(false);
        connectingUI.SetActive(true);
    }

    void Start()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("We're in the lobby");
        PhotonNetwork.JoinOrCreateRoom("test", null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We're connected and in a room!");

        roomCam.SetActive(false);

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;
        _player.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickcame);
    }
}
