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
        Debug.Log("Recieved: " + serializedBoundaryData);
        BoundaryData boundaryData = BoundaryData.DeSerialize(serializedBoundaryData);
        onRecieveBoundaryData.Invoke(boundaryData);
        Debug.Log(serializedBoundaryData);
    }

    public void SendBoundaryData(BoundaryData boundaryData)
    {
        photonView.RPC(nameof(RecieveResponse), RpcTarget.MasterClient,BoundaryData.Serialize(boundaryData));
    }

    [PunRPC]
    public void RecieveQuestions(string serializedQuestionData)
    {
        Debug.Log("Recieved: " + serializedQuestionData);
        StringData questionData = StringData.DeSerialize(serializedQuestionData);
        onRecieveQuestionData.Invoke(questionData);
    }

    public void SendQuestions(StringData questionData)
    {
        photonView.RPC(nameof(RecieveQuestions), RpcTarget.Others, StringData.Serialize(questionData));
    }
}
