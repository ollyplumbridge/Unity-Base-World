using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTimer : MonoBehaviour
{
    [Header("Scene Configuration")]
    [Tooltip("The name of the scene to load")]
    public string sceneToLoad = "BaseWorld"; 
    
    [Tooltip("How many seconds to wait before switching")]
    public float timer = 5.0f;

    void Start()
    {
        // Safety check to ensure the scene exists in Build Settings
        if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
        {
            StartCoroutine(TimedTransition());
        }
        else
        {
            Debug.LogError($"Scene '{sceneToLoad}' cannot be loaded. Check if it's added to Build Settings (File > Build Settings)!");
        }
    }

    IEnumerator TimedTransition()
    {
        Debug.Log($"Transitioning to {sceneToLoad} in {timer} seconds...");
        
        yield return new WaitForSeconds(timer);

        SceneManager.LoadScene(sceneToLoad);
    }
}