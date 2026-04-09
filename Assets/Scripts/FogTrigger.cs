using UnityEngine;

public class FogTrigger : MonoBehaviour
{
    public ParticleSystem fogParticles;
    private ParticleSystem.EmissionModule emission;

    public float targetRate = 50f;
    public float transitionSpeed = 2f;

    private float currentRate = 0f;
    private float desiredRate = 0f;

    void Start()
    {
        emission = fogParticles.emission;
        emission.rateOverTime = 0f;
    }

    void Update()
    {
        currentRate = Mathf.Lerp(currentRate, desiredRate, Time.deltaTime * transitionSpeed);
        emission.rateOverTime = currentRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            desiredRate = targetRate;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            desiredRate = 0f;
        }
    }
}