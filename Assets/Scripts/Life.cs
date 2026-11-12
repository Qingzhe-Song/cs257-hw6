// Life.cs
// Maintainer: Qingzhe-Song
// Summary: Holds current health, invokes death listeners at zero health, and destroys its GameObject.

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Life : MonoBehaviour
{
    [Header("Health")]
    [InspectorName("Current Health")]
    [Tooltip("Remaining health. Zero or a negative value triggers death in Update.")]
    [FormerlySerializedAs("amount")]
    public float currentHealth;

    [Header("Events")]
    [Tooltip("Invoked before destruction so listeners can award points or spawn effects.")]
    public UnityEvent onDeath;

    private void Update()
    {
        if (currentHealth <= 0)
        {
            // Notify listeners while this object's components and transform still exist.
            onDeath.Invoke();
            Destroy(gameObject);
        }
    }
}
