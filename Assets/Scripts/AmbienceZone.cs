using UnityEngine;

public class AmbienceZone : MonoBehaviour
{
    public AudioSource ambience;

    private void Start()
    {
        ambience.volume = 0f;
        ambience.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeAudio(ambience, 1f, 1f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeAudio(ambience, 0f, 1f));
        }
    }

    System.Collections.IEnumerator FadeAudio(AudioSource audio, float targetVolume, float duration)
    {
        float startVolume = audio.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            audio.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }

        audio.volume = targetVolume;
    }
}