using System.Collections; 
using UnityEngine;

public class AutoMoveCamera : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 1f;

    [Header("Target Positions")]
    public Transform stop1;
    public Transform stop2;
    public Transform stop3;
    public Transform stop4;

    void Start()
    {
        // Start the movement sequence as soon as the game begins
        StartCoroutine(RunCameraSequence());
    }

    IEnumerator RunCameraSequence()
    {
        // 1. Move to Stop 1, then pause
        if (stop1 != null)
        {
            yield return StartCoroutine(MoveToPosition(stop1.position, 10));
            yield return new WaitForSeconds(3f); // Pause for 3 seconds
        }

        // 2. Move to Stop 2, then pause
        if (stop2 != null)
        {
            yield return StartCoroutine(MoveToPosition(stop2.position, 10));
            yield return new WaitForSeconds(5f); // Pause for 3 seconds
        }

        // 3. Move to Stop 3
        if (stop3 != null)
        {
            yield return StartCoroutine(MoveToPosition(stop3.position, 10));
        }

        // 4. Move to Stop 4
        if (stop4 != null)
        {
            yield return StartCoroutine(MoveToPosition(stop4.position, 5));
        }
    }

    // IEnumerator MoveToPosition(Vector3 targetPosition)
    //     {
    //         // Lower numbers = slower, smoother movement (try 1.0f or 2.0f)
    //         float lerpSpeed = 1f; 

    //         while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
    //         {
    //             // Lerp smoothly slows down the camera as it gets closer to the target
    //             transform.position = Vector3.Lerp(
    //                 transform.position, 
    //                 targetPosition, 
    //                 lerpSpeed * Time.deltaTime
    //             );
                
    //             yield return null; 
    //         }

    //         transform.position = targetPosition;
    //     }

    IEnumerator MoveToPosition(Vector3 targetPosition, float durationInSeconds)
        {
            // 1. Change this number to exactly how many SECONDS the camera should take to travel
            //float durationInSeconds = 10f; 
            
            float elapsedTime = 0f;
            Vector3 startPosition = transform.position;

            while (elapsedTime < durationInSeconds)
            {
                // Calculate progress between 0 (start) and 1 (finish)
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / durationInSeconds;

                // Move smoothly over time
                transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
                
                yield return null; 
            }

            transform.position = targetPosition;
        }
}
