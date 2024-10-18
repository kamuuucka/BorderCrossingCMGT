using UnityEngine;
using UnityEngine.Events;

using Photon.Pun;
using Photon.Realtime;


public class RoomMaker : MonoBehaviourPunCallbacks
{
    [SerializeField] int maxPlayers = 7;
    [SerializeField] UnityEvent onConnectedToPhoton;
    [SerializeField] UnityEvent onCreatedRoom;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Established conntection to Photon: " + PhotonNetwork.CloudRegion + "Server");
        onConnectedToPhoton.Invoke();
    }

    public void CreateRoom()
    {
        string roomName = Random.Range(0, 9).ToString() + Random.Range(0, 9).ToString() + Random.Range(0, 9).ToString() + Random.Range(0, 9).ToString();
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = maxPlayers };
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        Debug.Log("Created room: " + roomName + ". Waiting for another player.");
    }

    public override void OnCreatedRoom()
    {
        onCreatedRoom.Invoke();
    }
}
