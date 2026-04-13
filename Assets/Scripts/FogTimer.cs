using UnityEngine;
using System.Collections;

public class FogTimer : MonoBehaviour
{
    [Header("Timing")]
    public float startTriggerDelay = 20f; 
    public float skyFadeDuration = 5f;    
    public float fogDelay = 1.5f;         
    public float fogFadeDuration = 4f;    

    [Header("Normal World Settings (Before Event)")]
    public Color normalSkyColor = new Color(0.3f, 0.5f, 0.8f);
    public Color normalFogColor = new Color(0.5f, 0.6f, 0.7f);
    public float normalFogDensity = 0.01f;

    [Header("Heavy Event Settings")]
    public Color targetFogColor = new Color(0.2f, 0.2f, 0.2f); // Darker gray
    public float targetDensity = 0.05f;

    private Camera mainCam;
    private CameraClearFlags originalFlags;

    void Start()
    {
        mainCam = Camera.main;
        originalFlags = mainCam.clearFlags;

        // Apply "Normal" settings immediately on Start
        mainCam.clearFlags = CameraClearFlags.SolidColor;
        mainCam.backgroundColor = normalSkyColor;
        
        RenderSettings.fog = true; // Fog is now ON from the start
        RenderSettings.fogMode = FogMode.ExponentialSquared; // Good for "thick" feel
        RenderSettings.fogColor = normalFogColor;
        RenderSettings.fogDensity = normalFogDensity;

        StartCoroutine(StartTheFogEvent());
    }

    IEnumerator StartTheFogEvent()
    {
        yield return new WaitForSeconds(startTriggerDelay);
        
        float elapsed = 0;
        bool transitionStarted = false;

        // Use the longest duration to ensure the loop runs long enough
        float totalDuration = Mathf.Max(skyFadeDuration, fogDelay + fogFadeDuration);

        while (elapsed < totalDuration)
        {
            elapsed += Time.deltaTime;

            // 1. Fade the Sky Background
            float skyT = Mathf.Clamp01(elapsed / skyFadeDuration);
            mainCam.backgroundColor = Color.Lerp(normalSkyColor, targetFogColor, skyT);

            // 2. Start the Heavy Fog transition after 'fogDelay'
            if (elapsed >= fogDelay)
            {
                float fogElapsed = elapsed - fogDelay;
                float fogT = Mathf.Clamp01(fogElapsed / fogFadeDuration);
                
                // Fade both Density and Color for a smoother "creeping in" effect
                RenderSettings.fogDensity = Mathf.Lerp(normalFogDensity, targetDensity, fogT);
                RenderSettings.fogColor = Color.Lerp(normalFogColor, targetFogColor, fogT);
            }

            yield return null;
        }

        // Ensure final values are locked in
        RenderSettings.fogDensity = targetDensity;
        RenderSettings.fogColor = targetFogColor;
        mainCam.backgroundColor = targetFogColor;
    }

    void OnDisable()
    {
        // Resets to Unity defaults if script is turned off
        if (mainCam != null)
        {
            mainCam.clearFlags = originalFlags;
        }
    }
}