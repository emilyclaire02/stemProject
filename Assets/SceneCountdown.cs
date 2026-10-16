
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class SceneCountdown : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text countdownText;

    [Header("Timer Settings")]
    public float countdownDuration = 5f;
    public float duration = 60f;

    [Header("Scenes")]
    public string endSceneName = "EndScene";

    private void Start()
    {
        if (countdownText == null)
        {
            Debug.LogError("Assign CountdownText in the Inspector!");
            return;
        }

        StartCoroutine(RunTimers());
    }

    private IEnumerator RunTimers()
    {
        countdownText.gameObject.SetActive(true);

        for (int seconds = Mathf.CeilToInt(countdownDuration);
             seconds > 0;
             seconds--)
        {
            countdownText.text = seconds.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = " ";

        float timeRemaining = duration;

        while (timeRemaining > 0f)
        {

            yield return null;
            timeRemaining -= Time.deltaTime;
        }

        SceneManager.LoadScene(endSceneName);
    }
}
