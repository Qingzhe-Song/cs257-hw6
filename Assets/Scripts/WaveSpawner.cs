// WaveSpawner.cs
// Maintainer: Qingzhe-Song
// Summary: Registers a timed enemy wave, spawns enemies repeatedly, and unregisters when the wave ends.

using UnityEngine;
using UnityEngine.Serialization;

public class WaveSpawner : MonoBehaviour
{
    [Header("Spawned Enemy")]
    [InspectorName("Enemy Prefab")]
    [Tooltip("Enemy prefab spawned at this spawner's position and rotation.")]
    [FormerlySerializedAs("prefab")]
    public GameObject enemyPrefab;

    [Header("Wave Timing")]
    [InspectorName("Start Delay (Seconds)")]
    [Tooltip("Delay from Start until the first spawn.")]
    [FormerlySerializedAs("startTime")]
    public float startDelay;
    [InspectorName("Wave End Delay (Seconds)")]
    [Tooltip("Delay from Start until this wave ends, measured independently of the first spawn.")]
    [FormerlySerializedAs("endTime")]
    public float endDelay;
    [InspectorName("Spawn Interval (Seconds)")]
    [Tooltip("Seconds between spawns. Use a positive value for repeated spawning.")]
    [FormerlySerializedAs("spawnRate")]
    public float spawnInterval;

    private void Start()
    {
        // Register before the first spawn so a delayed wave still counts toward the win condition.
        WaveManager.instance.AddWave(this);
        InvokeRepeating(nameof(SpawnEnemy), startDelay, spawnInterval);
        Invoke(nameof(EndWave), endDelay);
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }

    private void EndWave()
    {
        // Only future spawns end here; existing enemies remain tracked by EnemiesManager.
        WaveManager.instance.RemoveWave(this);
        CancelInvoke();
    }
}
