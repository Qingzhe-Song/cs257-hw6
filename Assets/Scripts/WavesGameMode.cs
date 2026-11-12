// WavesGameMode.cs
// Maintainer: Qingzhe-Song
// Summary: Loads the win scene after all waves and enemies clear, or the lose scene on player/base death.

using UnityEngine;
using UnityEngine.SceneManagement;

public class WavesGameMode : MonoBehaviour
{
    [Header("Defended Objects")]
    [Tooltip("Player health component whose death triggers the LoseScreen scene.")]
    [SerializeField] private Life playerLife;
    [Tooltip("Base health component whose death also triggers the LoseScreen scene.")]
    [SerializeField] private Life playerBaseLife;

    private void Start()
    {
        playerLife.onDeath.AddListener(OnPlayerOrBaseDied);
        playerBaseLife.onDeath.AddListener(OnPlayerOrBaseDied);
        EnemiesManager.instance.onChanged.AddListener(CheckWinCondition);
        WaveManager.instance.onChanged.AddListener(CheckWinCondition);
    }

    private void CheckWinCondition()
    {
        // Both lists must be empty: a quiet gap between scheduled spawns is not a win.
        if (EnemiesManager.instance.enemies.Count <= 0 && WaveManager.instance.waves.Count <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }

    private void OnPlayerOrBaseDied()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    private void OnDestroy()
    {
        // Release runtime event subscriptions when the game mode leaves the scene.
        playerLife.onDeath.RemoveListener(OnPlayerOrBaseDied);
        playerBaseLife.onDeath.RemoveListener(OnPlayerOrBaseDied);

        EnemiesManager.instance.onChanged.RemoveListener(CheckWinCondition);
        WaveManager.instance.onChanged.RemoveListener(CheckWinCondition);
    }
}
