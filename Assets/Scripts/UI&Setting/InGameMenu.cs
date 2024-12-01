using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject OptionMenu;
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private GameObject Player;
    private bool isPaused = false;

    private void Start()
    {
        PauseMenu.SetActive(false);
        OptionMenu.SetActive(false);
        GameOverMenu.SetActive(false);
    }
    void Update()
    {
        // Check for the pause button (Escape key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Pause the game
            PauseMenu.SetActive(true);
            OptionMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 1; // Resume the game
            PauseMenu.SetActive(false);
            OptionMenu.SetActive(false);
        }
    }

    public void BackToMenu()
    {
        Player.GetComponent<PlayerMovement>().enabled = false;// disable the control
        SceneManager.LoadScene("MainMenu");//scene change
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
