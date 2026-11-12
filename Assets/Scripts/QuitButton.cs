// QuitButton.cs
// Maintainer: Qingzhe-Song
// Summary: Connects the attached UI button to quitting the application.

using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        // Application.Quit has no effect in the Editor; the log confirms the button was clicked.
        Debug.Log("Quitting");
        Application.Quit();
    }
}
