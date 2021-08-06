using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame_Click()
    {
        SceneManager.LoadScene(1);
    }
}
