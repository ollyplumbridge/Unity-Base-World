using UnityEngine;
using System.Collections;

public class CameraAmbienceTrigger : MonoBehaviour
{
    public AudioSource ambience;
    public float fadeDuration = 1.0f;
    
    private Collider zoneCollider;
    private Transform cameraTransform;
    private Coroutine fadeCoroutine;

    private void Start()
    {
        zoneCollider = GetComponent<Collider>();
        // Automatically finds the Main Camera that Cinemachine is controlling
        cameraTransform = Camera.main.transform; 
        
        if (ambience != null) ambience.volume = 0;
    }

    private void Update()
    {
        // Check if the camera's position is inside this box's bounds
        bool isInside = zoneCollider.bounds.Contains(cameraTransform.position);

        if (isInside && ambience.volume < 0.99f && fadeCoroutine == null)
        {
            TriggerFade(1f);
        }
        else if (!isInside && ambience.volume > 0.01f && fadeCoroutine == null)
        {
            TriggerFade(0f);
        }
    }

    private void TriggerFade(float target)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeAudio(target));
    }

    IEnumerator FadeAudio(float targetVolume)
    {
        float startVolume = ambience.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            ambience.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeDuration);
            yield return null;
        }

        ambience.volume = targetVolume;
        fadeCoroutine = null;
    }
}