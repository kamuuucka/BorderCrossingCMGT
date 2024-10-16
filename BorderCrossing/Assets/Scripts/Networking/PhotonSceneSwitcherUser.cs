using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonSceneSwitcherUser : MonoBehaviour
{
    public void SwitchToSceneString(string scene)
    {
        PhotonSceneSwitcher.instance.SwitchToSceneString(scene);
    }

    public void SwtichToSceneStringOtherNonMasterClients(string scene)
    {
        PhotonSceneSwitcher.instance.SwtichToSceneStringOtherNonMasterClients(scene);
    }

    public void SwitchToSceneInt(int sceneID)
    {
        PhotonSceneSwitcher.instance.SwitchToSceneInt(sceneID);
    }

    public void SwtichToSceneIntOtherNonMasterClients(int sceneID)
    {
        PhotonSceneSwitcher.instance.SwtichToSceneIntOtherNonMasterClients(sceneID);
    }
}
