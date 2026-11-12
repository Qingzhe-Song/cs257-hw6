// LifeBar.cs
// Maintainer: Qingzhe-Song
// Summary: Displays the target's current health as a filled UI image on a 100-point scale.

using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    private const float FullBarHealth = 100f;

    [Header("Health Source")]
    [Tooltip("Health component displayed by this image; 100 health fills the entire bar.")]
    public Life targetLife;

    private Image barImage;

    private void Awake()
    {
        barImage = GetComponent<Image>();
    }

    private void Update()
    {
        barImage.fillAmount = targetLife.currentHealth / FullBarHealth;
    }
}
