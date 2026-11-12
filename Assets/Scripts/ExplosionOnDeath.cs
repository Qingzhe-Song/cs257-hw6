// ExplosionOnDeath.cs
// Maintainer: Qingzhe-Song
// Summary: Listens to the attached Life component and spawns a particle prefab on death.

using UnityEngine;

public class ExplosionOnDeath : MonoBehaviour
{
    [Header("Death Effect")]
    [Tooltip("Effect prefab spawned at this object's position and rotation when Life signals death.")]
    public GameObject particlePrefab;

    private void Awake()
    {
        // Life publishes death before destruction, so the effect can use the object's pose.
        var life = GetComponent<Life>();
        life.onDeath.AddListener(SpawnExplosion);
    }

    private void SpawnExplosion()
    {
        Instantiate(
            particlePrefab,
            transform.position,
            transform.rotation
        );
    }
}
