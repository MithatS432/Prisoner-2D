using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button startButton;
    public Button howtoPlayButton;
    public Button quitButton;

    public GameObject howToPlayPanel;
    public Button backMainMenu;
    public AudioClip clickSound;

    public void StartGame()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position, 1f);
        SceneManager.LoadScene("Level1");
    }

    public void HowToPlay()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position, 1f);
        howToPlayPanel.SetActive(true);
        startButton.gameObject.SetActive(false);
        howtoPlayButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }

#if UNITY_EDITOR
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
#else
    public void QuitGame()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position, 1f);
        Application.Quit();
    }
#endif

    public void BackMenu()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position, 1f);
        howToPlayPanel.SetActive(false);
        startButton.gameObject.SetActive(true);
        howtoPlayButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
}
