using UnityEngine;
using Photon.Pun;

using UnityEngine.Events;

public class DisconnectHandeler : MonoBehaviourPunCallbacks
{
    [SerializeField] int minplayerAmount;
    [SerializeField] UnityEvent onLeftRoom = null;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!PhotonNetwork.IsConnected) return;
        if (!PhotonNetwork.InRoom) return;
        if (PhotonNetwork.PlayerList.Length < minplayerAmount)
        {
            PhotonNetwork.LeaveRoom();
        }    
    }

    public override void OnLeftRoom()
    {
        onLeftRoom?.Invoke();
        base.OnLeftRoom();
    }
}
