// Pause.cs
// Maintainer: Qingzhe-Song
// Summary: Opens the pause menu on Escape and restores gameplay when Resume is pressed.

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [Header("Pause Menu")]
    [Tooltip("Menu shown while gameplay is paused; hidden during Awake and on resume.")]
    public GameObject pauseMenu;
    [Tooltip("Button that resumes gameplay and locks the cursor again.")]
    public Button resumeButton;

    private void Awake()
    {
        pauseMenu.SetActive(false);
        resumeButton.onClick.AddListener(OnResumePressed);
    }

    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        // The menu needs an unlocked, visible cursor so its buttons can be clicked.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnResumePressed()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDestroy()
    {
        // Time scale is global, so leaving this scene must not leave the next scene paused.
        Time.timeScale = 1f;
    }
}
