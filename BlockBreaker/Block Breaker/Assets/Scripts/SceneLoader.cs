using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // cache
    GameStatus gameStatus;

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (SceneManager.GetActiveScene().name == "Win")
        {
            gameStatus = FindObjectOfType<GameStatus>();
            gameStatus.Destroy();
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
