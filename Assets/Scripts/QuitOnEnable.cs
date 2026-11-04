using UnityEngine;

public class QuitOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        QuitGame();
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        // Stops play mode in the Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quits the built game
        Application.Quit();
#endif
    }
}
