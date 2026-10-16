
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class VRGazeClicker : MonoBehaviour
{
    [System.Serializable]
    public class SceneTextOption
    {
        public TMP_Text targetText;
        public string targetSceneName;
    }

    [Header("Text and Scene Options")]
    public SceneTextOption[] options;

    [Header("Gaze Settings")]
    public float maxDistance = 20f;

    private bool isLoading = false;

    void Update()
    {

        Debug.DrawRay(
            transform.position,
            transform.forward * maxDistance,
            Color.red
        );

        if (isLoading || !WasScreenTapped())
            return;

        Ray ray = new Ray(transform.position, transform.forward);

        if (!Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            Debug.Log("Tap detected, but nothing was hit.");
            return;
        }

        foreach (SceneTextOption option in options)
        {
            if (option == null || option.targetText == null)
                continue;

            Transform hitTransform = hit.collider.transform;
            Transform textTransform = option.targetText.transform;

            if (hitTransform == textTransform ||
                hitTransform.IsChildOf(textTransform))
            {
                if (string.IsNullOrWhiteSpace(option.targetSceneName))
                {
                    Debug.LogError(
                        "No destination scene assigned for " +
                        option.targetText.name
                    );
                    return;
                }

                Debug.Log("Loading scene: " + option.targetSceneName);

                isLoading = true;
                SceneManager.LoadScene(option.targetSceneName);
                return;
            }
        }

        Debug.Log("Tap detected, but the hit object was not a target text.");
    }

    bool WasScreenTapped()
    {
        bool tapped = false;

        #if ENABLE_INPUT_SYSTEM
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            tapped = true;
        }

        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            tapped = true;
        }
        #endif

        #if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.touchCount > 0 &&
            Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            tapped = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            tapped = true;
        }
        #endif

        return tapped;
    }
}
