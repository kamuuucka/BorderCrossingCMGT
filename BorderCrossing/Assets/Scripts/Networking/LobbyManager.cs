using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

public class LobbyManager : MonoBehaviour
{
    string roomCode;

    [SerializeField] int minStudentCount;
    [SerializeField] UnityEvent onEnoughStudents;
    [SerializeField] UnityEvent<string> onSetRoomCode;
    [SerializeField] UnityEvent<string> onSetStudentCount;

    private void Update()
    {
        if (roomCode == null)
        {
            roomCode = getRoomCode();
        }

        int studentCount = PhotonNetwork.PlayerList.Length - 1;
        int maxStudentCount = PhotonNetwork.CurrentRoom.MaxPlayers - 1;
        onSetStudentCount.Invoke(studentCount + "/" + maxStudentCount);
        if (studentCount >= minStudentCount) onEnoughStudents.Invoke();
    }

    private string getRoomCode()
    {
        if (!PhotonNetwork.IsConnected) return null;
        if (PhotonNetwork.CurrentRoom == null) return null;

        string outp = PhotonNetwork.CurrentRoom.Name;
        onSetRoomCode.Invoke(outp);
        return outp;
    }
}
