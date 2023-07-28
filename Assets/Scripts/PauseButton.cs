using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject pausedBox;
    public Button resumeButton;

    public void PauseGame()
    {
        pausedBox.SetActive(true);
        resumeButton.gameObject.SetActive(true);
        resumeButton.interactable = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausedBox.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        resumeButton.interactable = false;
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}