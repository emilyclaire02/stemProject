
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject startPanel;
    public TMP_Text timerText;

    [Header("Scenes")]
    public string experienceSceneName = "IntroStage";
    public string endSceneName = "EndScene";

    public float duration = 60f;

    private float timeRemaining;
    private bool hasStarted = false;

    public static bool ExperienceStarted { get; private set; }

    private static Timer instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        timeRemaining = duration;
        ExperienceStarted = false;

        if (startPanel != null)
            startPanel.SetActive(true);

    }

    void Update()
    {
        if (!hasStarted)
        {
            bool tapped = Input.GetMouseButtonDown(0);

            if (Input.touchCount > 0 &&
                Input.GetTouch(0).phase == TouchPhase.Began)
            {
                tapped = true;
            }

            if (tapped)
                StartExperience();

            return;
        }

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0f, timeRemaining);

        if (timeRemaining <= 0f)
        {
            hasStarted = false;
            ExperienceStarted = false;

            SceneManager.LoadScene(endSceneName);
        }
    }

    void StartExperience()
    {
        hasStarted = true;
        ExperienceStarted = true;

        SceneManager.LoadScene(experienceSceneName);
    }
}
