using System;
using TMPro;
using UnityEngine;

public class WavesView : MonoBehaviour
{
    private const string WavesTextSample = "Wave {0}/{1}";
    private const string TimerSample = "{0:0.0}s";

    [SerializeField] private TMP_Text _wavesText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _finalWaveText;
    [SerializeField] private ProgressView _progressView;

    public void SetWave(int currentWave, int wavesCount)
    {
        _wavesText.text = String.Format(WavesTextSample, currentWave, wavesCount);
    }

    public void UpdateTimer(float currentTime, float timeNormalized)
    {
        _timerText.text = String.Format(TimerSample, currentTime);
        _progressView.SetProgress(timeNormalized);
    }

    public void SetStandart()
    {
        _finalWaveText.gameObject.SetActive(false);
        _timerText.gameObject.SetActive(true);
    }

    public void SetFinalWave()
    {
        _finalWaveText.gameObject.SetActive(true);
        _timerText.gameObject.SetActive(false);
    }
}