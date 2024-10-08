using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(PhotonView))]
public class PhotonBondaryDataCommunicator : MonoBehaviourPun
{
    [SerializeField] UnityEvent<BoundaryData> onRecieveBoundaryData;
    [SerializeField] UnityEvent<StringData> onRecieveQuestionData;

    [PunRPC]
    public void RecieveResponse(string serializedBoundaryData) 
    {
        BoundaryData boundaryData = BoundaryData.DeSerialize(serializedBoundaryData);
        onRecieveBoundaryData.Invoke(boundaryData);
        Debug.Log(boundaryData.ToString());
    }

    public void SendBoundaryData(BoundaryData boundaryData)
    {
        photonView.RPC(nameof(RecieveResponse), RpcTarget.MasterClient,BoundaryData.Serialize(boundaryData));
    }

    [PunRPC]
    public void RecieveQuestions(string serializedQuestionData)
    {
        StringData questionData = StringData.DeSerialize(serializedQuestionData);
        onRecieveQuestionData.Invoke(questionData);
        Debug.Log(questionData.ToString());
    }

    public void SendQuestions(StringData questionData)
    {
        photonView.RPC(nameof(RecieveQuestions), RpcTarget.Others, StringData.Serialize(questionData));
    }
}
