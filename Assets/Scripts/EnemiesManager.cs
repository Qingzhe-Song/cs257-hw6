// EnemiesManager.cs
// Maintainer: Qingzhe-Song
// Summary: Tracks registered enemies and notifies listeners when the enemy list changes.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager instance;

    [Header("Enemy Tracking")]
    [Tooltip("Registered enemies used by the game mode to check whether the wave is cleared.")]
    public List<Enemy> enemies;

    [Header("Events")]
    [Tooltip("Invoked after AddEnemy or RemoveEnemy updates the enemy list.")]
    public UnityEvent onChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // Keep the first manager as the shared instance when a duplicate is present.
            Debug.LogError("Dupe detected, ignoring this", gameObject);
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        onChanged.Invoke();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        onChanged.Invoke();
    }
}
