using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Human : MonoBehaviour
{
    [Header("Menu Settings")]
    public Button pauseButton;
    public Button continueButton;
    public Button quitGameButton;
    public GameObject winPanel;
    public Button nextButton;
    public TextMeshProUGUI totalGemCount;
    public TextMeshProUGUI totalSnakeCount;

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
    public TextMeshProUGUI gemCountText;
    private int collectedGems = 0;
    private int totalGems = 0;
    public AudioClip gemSound;
    int snakeForGem = 2;
    public GameObject runEffect;
    public bool isRunning;
    public AudioClip jumpSound;

    [Header("Parallax Settings")]
    public Transform background;
    public float backgroundWidth = 20f;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupUI();
    }

    void SetupUI()
    {
        if (pauseButton == null)
            pauseButton = GameObject.Find("Pause")?.GetComponent<Button>();
        if (continueButton == null)
            continueButton = GameObject.Find("Continue")?.GetComponent<Button>();
        if (quitGameButton == null)
            quitGameButton = GameObject.Find("Exit Game")?.GetComponent<Button>();
        if (nextButton == null)
            nextButton = GameObject.Find("Next")?.GetComponent<Button>();

        if (pauseButton != null) { pauseButton.onClick.RemoveAllListeners(); pauseButton.onClick.AddListener(PauseGame); }
        if (continueButton != null) { continueButton.onClick.RemoveAllListeners(); continueButton.onClick.AddListener(ContinueGame); }
        if (quitGameButton != null) { quitGameButton.onClick.RemoveAllListeners(); quitGameButton.onClick.AddListener(QuitGame); }
    }


    void Start()
    {
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
        isRunning = true;
        Vector3 effectPos = transform.position + Vector3.down * 0.5f;
        if (isGround && isRunning)
        {
            GameObject effect = Instantiate(runEffect, effectPos, Quaternion.identity);
            Destroy(effect, 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
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
        if (background.position.x <= transform.position.x - backgroundWidth / 2f)
        {
            background.position += new Vector3(backgroundWidth, 0f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGround = true;

        else if (other.gameObject.CompareTag("Enemy"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            totalGems++;
            collectedGems++;
            gemCountText.text = "Gems:" + totalGems.ToString();
            AudioSource.PlayClipAtPoint(gemSound, transform.position, 1f);
            Destroy(other.gameObject);
            if (collectedGems == 3)
            {
                snakeCount += snakeForGem;
                snakeCountText.text = "Snake Count:" + snakeCount.ToString();
                collectedGems = 0;
                EnemyAI enemy = FindObjectOfType<EnemyAI>();
                if (enemy != null)
                {
                    enemy.ReduceSpeed(3f);
                }
            }
            if (totalGems == 20)
            {
                EnemyAI enemy = FindObjectOfType<EnemyAI>();
                if (enemy != null)
                {
                    enemy.BoostSpeed(20f);
                }

            }

        }
        else if (other.gameObject.CompareTag("Next Level"))
        {
            if (winPanel != null)
            {
                winPanel.SetActive(true);
                Time.timeScale = 0f;
                totalGemCount.text = "Collected Gems:" + totalGems.ToString();
                totalSnakeCount.text = "Snakes:" + snakeCount.ToString();
            }
        }
    }
}
