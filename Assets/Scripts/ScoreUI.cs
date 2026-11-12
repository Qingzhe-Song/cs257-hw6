// ScoreUI.cs
// Maintainer: Qingzhe-Song
// Summary: Displays the shared score each frame.

using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        scoreText.text = $"Score: {ScoreManager.instance.currentScore}";
    }
}
