using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EasySceneLoader : MonoBehaviour
{
    [Header("Configuration")]
    public string sceneToLoad;
    public float delay = 3.0f;
    public bool loadAutomatically = true;

    void Start()
    {
        // If you want this specific object to just be a timer
        if (loadAutomatically)
        {
            StartSceneTransition();
        }
    }

    // You can also call this from a Button Click or another script!
    public void StartSceneTransition()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            StartCoroutine(ExecuteTransition());
        }
        else
        {
            Debug.LogError("EasySceneLoader: You forgot to type the Scene Name!");
        }
    }

    IEnumerator ExecuteTransition()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}