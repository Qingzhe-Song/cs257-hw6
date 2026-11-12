// Enemy.cs
// Maintainer: Qingzhe-Song
// Summary: Registers with EnemiesManager on Start and unregisters when destroyed.

using UnityEngine;

public class Enemy : MonoBehaviour
{

    private void Start()
    {
        // Start runs after scene Awake calls, when the manager establishes its instance.
        EnemiesManager.instance.AddEnemy(this);
    }

    private void OnDestroy()
    {
        EnemiesManager.instance.RemoveEnemy(this);
    }
}
