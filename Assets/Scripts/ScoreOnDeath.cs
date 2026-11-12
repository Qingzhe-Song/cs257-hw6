// ScoreOnDeath.cs
// Maintainer: Qingzhe-Song
// Summary: Awards points through ScoreManager when the attached Life component signals death.

using UnityEngine;
using UnityEngine.Serialization;

public class ScoreOnDeath : MonoBehaviour
{
    [Header("Score Reward")]
    [InspectorName("Points On Death")]
    [Tooltip("Points added to the shared score when this object's Life component signals death.")]
    [FormerlySerializedAs("amount")]
    public int pointsOnDeath;

    private void Awake()
    {
        var life = GetComponent<Life>();
        life.onDeath.AddListener(AwardPointsOnDeath);
    }

    private void AwardPointsOnDeath()
    {
        ScoreManager.instance.currentScore += pointsOnDeath;
    }

    private void OnDestroy()
    {
        // Pair the runtime subscription with cleanup when this component is destroyed.
        var life = GetComponent<Life>();
        life.onDeath.RemoveListener(AwardPointsOnDeath);
    }
}
