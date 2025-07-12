using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel != null)
            {
                if (!pausePanel.activeSelf)
                {
                    pausePanel.SetActive(true);
                }
                else
                {
                    pausePanel.SetActive(false);
                }
            }
        } 
    }



    public void ExitGame()
    {
        Application.Quit();
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
