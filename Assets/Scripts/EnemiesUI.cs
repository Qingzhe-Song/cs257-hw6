// EnemiesUI.cs
// Maintainer: Qingzhe-Song
// Summary: Displays the remaining enemy count whenever the enemy manager's list changes.

using TMPro;
using UnityEngine;

public class EnemiesUI : MonoBehaviour
{
    private TMP_Text enemyCountText;

    private void Start()
    {
        // Start runs after the manager establishes its shared instance in Awake.
        enemyCountText = GetComponent<TMP_Text>();
        EnemiesManager.instance.onChanged.AddListener(RefreshEnemyCount);
        RefreshEnemyCount();
    }

    private void RefreshEnemyCount()
    {
        enemyCountText.text = $"Remaining Enemies: {EnemiesManager.instance.enemies.Count}";
    }
}
