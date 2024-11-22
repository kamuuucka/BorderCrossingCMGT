using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public void SwitchToScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
