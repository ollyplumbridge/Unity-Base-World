using UnityEngine;

public class BirdMover : MonoBehaviour
{
    public float speed = 5f;
    private bool shouldMove = false;

    public void StartMoving()
    {
        shouldMove = true;
    }

    void Update()
    {
        if (shouldMove)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}