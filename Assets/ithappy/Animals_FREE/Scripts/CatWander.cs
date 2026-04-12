using UnityEngine;

public class CatWander : MonoBehaviour
{
    public float speed = 1.5f;
    public float wanderRadius = 5f;
    public float waitTime = 2f;

    private Vector3 targetPoint;
    private float waitTimer;
    private bool walking;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); // 👈 grab animator
        PickNewTarget();
    }

    void Update()
    {
        if (!walking)
        {
            waitTimer -= Time.deltaTime;

            if (anim != null)
                anim.SetFloat("Speed", 0f); // idle

            if (waitTimer <= 0)
                PickNewTarget();

            return;
        }

        Move();
    }

    void PickNewTarget()
    {
        Vector2 random = Random.insideUnitCircle * wanderRadius;

        targetPoint = new Vector3(
            transform.position.x + random.x,
            transform.position.y,
            transform.position.z + random.y
        );

        walking = true;
    }

    void Move()
    {
        Vector3 direction = targetPoint - transform.position;
        direction.y = 0;

        float distance = direction.magnitude;

        if (distance < 0.2f)
        {
            walking = false;
            waitTimer = waitTime;

            if (anim != null)
                anim.SetFloat("Speed", 0f); // idle

            return;
        }

        Vector3 moveDir = direction.normalized;

        transform.position += moveDir * speed * Time.deltaTime;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(moveDir),
            5f * Time.deltaTime
        );

        // 🐾 THIS is what makes legs move
        if (anim != null)
            anim.SetFloat("Speed", 1f);
    }
}