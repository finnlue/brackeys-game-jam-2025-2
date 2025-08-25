using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public int startScene = 1;

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }

    public void Options()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
