using UnityEngine;
using System.Collections;

public class FogTimer2 : MonoBehaviour
{
    [Header("Timing")]
    public float startTriggerDelay = 20f; // Time until anything happens
    public float skyFadeDuration = 5f;    // How long the sky takes to turn gray
    public float fogDelay = 1.5f;         // How many seconds to wait AFTER sky starts before fog appears
    public float fogFadeDuration = 4f;    // How fast the actual fog thickens

    [Header("Fog & Background")]
    public Color targetFogColor = new Color(0.5f, 0.5f, 0.5f); 
    public float targetDensity = 0.05f;

    private Camera mainCam;
    private CameraClearFlags originalFlags;
    private Color originalCamColor;

    void Start()
    {
        mainCam = Camera.main;
        originalFlags = mainCam.clearFlags;
        originalCamColor = mainCam.backgroundColor;

        RenderSettings.fog = false;
        RenderSettings.fogDensity = 0;
        RenderSettings.fogColor = targetFogColor;

        StartCoroutine(StartTheFogEvent());
    }

    IEnumerator StartTheFogEvent()
    {
        yield return new WaitForSeconds(startTriggerDelay);
        
        // Switch to Solid Color immediately
        mainCam.clearFlags = CameraClearFlags.SolidColor;
        Color startingBlue = new Color(0.3f, 0.5f, 0.8f); 

        float elapsed = 0;
        bool fogStarted = false;

        // Run the master loop based on the sky duration
        while (elapsed < skyFadeDuration)
        {
            elapsed += Time.deltaTime;
            float skyT = elapsed / skyFadeDuration;

            // 1. Fade the Sky Background First
            mainCam.backgroundColor = Color.Lerp(startingBlue, targetFogColor, skyT);

            // 2. Start the Fog after the specified 'fogDelay'
            if (elapsed >= fogDelay)
            {
                if (!fogStarted) { RenderSettings.fog = true; fogStarted = true; }
                
                float fogElapsed = elapsed - fogDelay;
                float fogT = fogElapsed / fogFadeDuration;
                RenderSettings.fogDensity = Mathf.Lerp(0, targetDensity, fogT);
            }

            yield return null;
        }

        // Ensure final values are set
        RenderSettings.fogDensity = targetDensity;
        mainCam.backgroundColor = targetFogColor;
    }

    void OnDisable()
    {
        if (mainCam != null)
        {
            mainCam.clearFlags = originalFlags;
            mainCam.backgroundColor = originalCamColor;
        }
    }
}