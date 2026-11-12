// WavesUI.cs
// Maintainer: Qingzhe-Song
// Summary: Displays the remaining wave count whenever the wave manager's list changes.

using TMPro;
using UnityEngine;

public class WavesUI : MonoBehaviour
{
    private TMP_Text waveCountText;

    private void Start()
    {
        // Start runs after the manager establishes its shared instance in Awake.
        waveCountText = GetComponent<TMP_Text>();
        WaveManager.instance.onChanged.AddListener(RefreshWaveCount);
        RefreshWaveCount();
    }

    private void RefreshWaveCount()
    {
        waveCountText.text = $"Remaining Waves: {WaveManager.instance.waves.Count}";
    }
}
