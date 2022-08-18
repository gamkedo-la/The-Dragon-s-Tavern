using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void LoadSceneByName(string sceneName)
    {
       // gameManager.SaveGame();

        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        int currentIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIdx + 1);
    }
}
