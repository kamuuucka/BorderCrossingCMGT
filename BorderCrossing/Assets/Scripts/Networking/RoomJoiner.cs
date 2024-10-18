using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RoomJoiner : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField inputField;

    [SerializeField] UnityEvent onConnectedToPhoton;
    [SerializeField] UnityEvent onStartSearching;
    [SerializeField] UnityEvent onConnectedToRoom;
    [SerializeField] UnityEvent onFailedToConnectToRoom;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Established conntection to Photon: " + PhotonNetwork.CloudRegion + "Server");
        onConnectedToPhoton.Invoke();
    }

    public void TryToJoinRoom() 
    {
        PhotonNetwork.JoinRoom(inputField.text);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Could not find Room: " + inputField.text);
        onFailedToConnectToRoom.Invoke();
    }

    public override void OnJoinedRoom()
    {
        onConnectedToRoom.Invoke();
    }
}
