// Autodestroy.cs
// Maintainer: Qingzhe-Song
// Summary: Destroys its GameObject after a configured lifetime.

using UnityEngine;
using UnityEngine.Serialization;

public class AutoDestroy : MonoBehaviour
{
    [Header("Lifetime")]
    [InspectorName("Lifetime (Seconds)")]
    [Tooltip("Time after Start before this GameObject is destroyed.")]
    [FormerlySerializedAs("delay")]
    public float lifetime;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
