using UnityEngine;

public class MoveAfterTime : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float startTime = 10f; // time in seconds before movement starts

    private float timer = 0f;

    void Update()
    {
        // Increase timer every frame
        timer += Time.deltaTime;

        // Start moving after the set time
        if (timer >= startTime)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}