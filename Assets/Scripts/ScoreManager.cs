// ScoreManager.cs
// Maintainer: Qingzhe-Song
// Summary: Exposes the scene's shared score through a singleton instance.

using UnityEngine;
using UnityEngine.Serialization;

public class ScoreManager : MonoBehaviour
{
    [Header("Score")]
    [InspectorName("Current Score")]
    [Tooltip("Running score increased by ScoreOnDeath components.")]
    [FormerlySerializedAs("amount")]
    public int currentScore;

    public static ScoreManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // Keep the first manager as the shared score source.
            Debug.LogError("Duplicated ScoreManager, ignoring this one", gameObject);
        }
    }
}
