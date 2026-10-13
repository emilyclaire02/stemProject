using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

using UnityEngine.InputSystem;

public class VRGazeClicker : MonoBehaviour
{
    [Header("Gaze Settings")]
    public float maxDistance = 20f;
    public float gazeDuration = 2.0f;

    [Header("UI Reference")]
    public Image gazeLoaderImage;

    private float timer = 0f;
    private GameObject currentGazeObject;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            GameObject lookedAtObject = hit.collider.gameObject;

            // Only progress the loader if the object actually has our scene target script
            if (lookedAtObject.GetComponent<GazeSceneTarget>() != null)
            {
                if (lookedAtObject == currentGazeObject)
                {
                    timer += Time.deltaTime;

                    // Update the loading circle fill progress (0.0 to 1.0)
                    if (gazeLoaderImage != null)
                    {
                        gazeLoaderImage.fillAmount = timer / gazeDuration;
                    }

                    if (timer >= gazeDuration)
                    {
                        ExecuteClick(lookedAtObject);
                        ResetGaze();
                    }
                }
                else
                {
                    currentGazeObject = lookedAtObject;
                    timer = 0f;
                }
            }
            else
            {
                // Looked at something that isn't a scene target text
                ResetGaze();
            }
        }
        else
        {
            ResetGaze();
        }
    }

    void ResetGaze()
    {
        currentGazeObject = null;
        timer = 0f;
        
        // Reset the circle back to empty when looking away
        if (gazeLoaderImage != null)
        {
            gazeLoaderImage.fillAmount = 0f;
        }
    }

    void ExecuteClick(GameObject obj)
    {
        GazeSceneTarget sceneTarget = obj.GetComponent<GazeSceneTarget>();
        
        if (sceneTarget != null && !string.IsNullOrEmpty(sceneTarget.sceneToLoad))
        {
            Debug.Log("Gaze loading scene: " + sceneTarget.sceneToLoad);
            SceneManager.LoadScene(sceneTarget.sceneToLoad);
        }
    }
}