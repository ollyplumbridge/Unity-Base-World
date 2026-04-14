using UnityEngine;

public class MoveChicken : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float startTime = 10f;

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