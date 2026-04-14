using UnityEngine;
using System.Collections;

public class MoveBoat : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float startDelay = 0f;

    private Rigidbody rb;
    private bool canMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(StartAfterDelay());
    }

    IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        canMove = true;
    }

    void FixedUpdate()
    {
        if (!canMove) return;

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