using UnityEngine;

public class AmbienceZone : MonoBehaviour
{
    public AudioSource ambience;

    private Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
        ambience.volume = 0f;
        ambience.Play();
    }

    private void Update()
    {
        // Check if camera is inside collider bounds
        Collider col = GetComponent<Collider>();

        if (col.bounds.Contains(cam.position))
        {
            StopAllCoroutines();
            StartCoroutine(FadeAudio(1f, 1f));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FadeAudio(0f, 1f));
        }
    }

    System.Collections.IEnumerator FadeAudio(float targetVolume, float duration)
    {
        float startVolume = ambience.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            ambience.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }

        ambience.volume = targetVolume;
    }
}