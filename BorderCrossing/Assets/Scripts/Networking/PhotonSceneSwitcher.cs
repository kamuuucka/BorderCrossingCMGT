using UnityEngine;
using Photon.Pun;

class PhotonSceneSwitcher : MonoBehaviourPun
{
    public static PhotonSceneSwitcher instance = null;

    private void Awake()
    {
        if (instance != null) 
        { 
            Debug.LogError("Multiple instances of PhotonSceneSwitcher should not exist");
            Destroy(this);
            return;
        }

        instance = this;
    }

    [PunRPC]
    public void SwitchToSceneString(string scene) 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    public void SwtichToSceneStringOtherNonMasterClients(string scene) 
    {
        if (!PhotonNetwork.IsConnected) return;
        if (!PhotonNetwork.IsMasterClient) return;
        photonView.RPC(nameof(SwitchToSceneString), RpcTarget.Others, scene);
    }

    [PunRPC]
    public void SwitchToSceneInt(int sceneID)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneID);
    }

    public void SwtichToSceneIntOtherNonMasterClients(int sceneID)
    {
        if (!PhotonNetwork.IsConnected) return;
        if (!PhotonNetwork.IsMasterClient) return;
        photonView.RPC(nameof(SwitchToSceneInt), RpcTarget.Others, sceneID);
    }
}
