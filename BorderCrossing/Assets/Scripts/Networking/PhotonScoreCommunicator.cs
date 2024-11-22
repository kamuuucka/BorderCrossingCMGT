using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(PhotonView))]
public class PhotonBondaryDataCommunicator : MonoBehaviourPun
{
    [SerializeField] UnityEvent<BoundaryData> onRecieveBoundaryData;
    [SerializeField] UnityEvent<StringData> onRecieveQuestionData;

    private StringData _questionData;

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
        _questionData = questionData;
        onRecieveQuestionData.Invoke(questionData);
    }

    public void ToTheText(TMP_Text text)
    {
        text.text = _questionData.data.Count.ToString();
    }

    public void SendQuestions(StringData questionData)
    {
        Debug.Log($"Sending: {questionData.data.Count}");
        photonView.RPC(nameof(RecieveQuestions), RpcTarget.Others, StringData.Serialize(questionData));
    }
}
