using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update()
    {
        //if(Input.GetKeyDown.KeyCode.Escape)  //why
        //{
        //    if(gameIsPaused)
        //    {
        //        Resume();
        //    }
        //    else
        //    {
        //        Paused();
        //    }
        //}
    }

    void Paused()
    {
        //PlayerMovement.instance.enable = false;
        //pauseMenuUI.SetActive(true);
        //Time.timeScale = 0;
        //gameIsPaused = true;
    }

    public void Resume()
    {
        //PlayerMovement.instance.enable = true;
        //PauseMenu.SetActive(false);
        //Time.timeScale = 1;
        //gameIsPaused = false;
    }

    public void LoadMainMenu()
    {
        //DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        //SceneManager.LoadScene("MainMenu");
    }

}
