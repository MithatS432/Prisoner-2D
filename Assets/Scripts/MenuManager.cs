using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button startButton;
    public Button howtoPlayButton;
    public Button quitButton;

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }


#if UNITY_EDITOR
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
#else
    public void QuitGame()
    {
        Application.Quit();
    }
#endif

}
