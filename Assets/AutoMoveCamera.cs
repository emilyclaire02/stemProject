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
        StartCoroutine(RunCameraSequence());
    }

    IEnumerator RunCameraSequence()
    {

        yield return new WaitForSeconds(5f);

        if (stop1 != null)
        {
            yield return StartCoroutine(MoveToPosition(stop1.position, 10));
            yield return new WaitForSeconds(3f); 
        }

        if (stop2 != null)
        {
            yield return StartCoroutine(MoveToPosition(stop2.position, 10));
            yield return new WaitForSeconds(5f); 
        }

        if (stop3 != null)
        {
            yield return StartCoroutine(MoveToPosition(stop3.position, 10));
        }

        if (stop4 != null)
        {
            yield return StartCoroutine(MoveToPosition(stop4.position, 5));
        }
    }


    IEnumerator MoveToPosition(Vector3 targetPosition, float durationInSeconds)
        {
            
            float elapsedTime = 0f;
            Vector3 startPosition = transform.position;

            while (elapsedTime < durationInSeconds)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / durationInSeconds;

                transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
                
                yield return null; 
            }

            transform.position = targetPosition;
        }
}
