using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimedSceneLoaderAdvanced : MonoBehaviour
{
    [Header("Timing")]
    public float delay = 5f;

    [Header("Scenes")]
    public string sceneToLoad;
    public string sceneToUnload;

    void Start()
    {
        StartCoroutine(LoadAfterDelay());
    }

    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        // Load new scene additively
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        yield return loadOp;

        // Unload chosen scene
        if (!string.IsNullOrEmpty(sceneToUnload))
        {
            SceneManager.UnloadSceneAsync(sceneToUnload);
        }
    }
}