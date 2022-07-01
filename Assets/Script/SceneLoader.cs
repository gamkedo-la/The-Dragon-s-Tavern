using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        int currentIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIdx + 1);
    }
}
