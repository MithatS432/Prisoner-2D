using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [Header("Character Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    public GameObject snakeObject;
    public AudioClip snakeSound;
    public bool isGround;
    public TextMeshProUGUI snakeCountText;
    private int snakeCount = 3;
    public GameObject parallaxBackGround;

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

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            prb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGround = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && snakeCount > 0)
        {
            Instantiate(snakeObject, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(snakeSound, transform.position, 1f);
            snakeCount--;
            snakeCountText.text = "Snake Count:" + snakeCount.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGround = true;
    }
}
