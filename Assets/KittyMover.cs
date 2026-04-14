using UnityEngine;

public class MoveKitty : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float startTime = 0f; // starts immediately (change if you want delay)

    private float timer = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (timer >= startTime)
        {
            Vector3 move = transform.forward * moveSpeed * Time.fixedDeltaTime;

            if (rb != null)
            {
                rb.MovePosition(rb.position + move);
            }
            else
            {
                transform.Translate(move, Space.World);
            }
        }
    }
}