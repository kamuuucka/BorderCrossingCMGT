using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
public class DebateCommunicator : MonoBehaviourPun
{
    [SerializeField] UnityEvent onTimeOver;
    [SerializeField] UnityEvent<bool> onPlayerPickedSide;

    [PunRPC]
    public void RecieveTimeOver()
    {
        onTimeOver?.Invoke();
    }

    public void SendTimeOver()
    {
        photonView.RPC(nameof(RecieveTimeOver), RpcTarget.Others);
    }

    [PunRPC]
    public void RecievePlayerPickedSide(bool side)
    {
        onPlayerPickedSide?.Invoke(side);
    }

    public void SendPlayerPickedSide(bool side)
    {
        photonView.RPC(nameof(RecievePlayerPickedSide), RpcTarget.MasterClient, side);
    }
}
