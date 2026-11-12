// PlayerBulletsUI.cs
// Maintainer: Qingzhe-Song
// Summary: Displays the selected player's remaining bullet count each frame.

using TMPro;
using UnityEngine;

public class PlayerBulletsUI : MonoBehaviour
{
    [Header("Ammunition Source")]
    [Tooltip("Player weapon whose remaining bullet count is displayed.")]
    public PlayerShooting targetShooting;

    private TMP_Text bulletCountText;

    private void Awake()
    {
        bulletCountText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        bulletCountText.text = $"Bullets: {targetShooting.remainingBullets}";
    }
}
