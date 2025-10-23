using UnityEngine;
using UnityEngine.UI;

public class Human : MonoBehaviour
{
    [Header("Menu Settings")]
    public Button pauseButton;
    public Button continueButton;
    public Button quitGameButton;

    [Header("Components")]
    private Rigidbody2D prb;
    private Animator pa;
    private AudioSource pas;

    [Header("Charachter Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    public GameObject snakeObject;




    void Start()
    {
        if (pauseButton != null)
            pauseButton.onClick.AddListener(PauseGame);
        if (continueButton != null)
            continueButton.onClick.AddListener(ContinueGame);
        if (quitGameButton != null)
            quitGameButton.onClick.AddListener(QuitGame);
        prb = GetComponent<Rigidbody2D>();
        pa = GetComponent<Animator>();
        pas = GetComponent<AudioSource>();

    }

    void PauseGame()
    {
        pauseButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
        quitGameButton.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    void ContinueGame()
    {
        pauseButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(false);
        quitGameButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
