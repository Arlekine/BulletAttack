using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WavesView : MonoBehaviour
{
    private const string WavesTextSample = "Wave {0}/{1}";
    private const string TimerSample = "{0:0.0}s";

    [SerializeField] private TMP_Text _wavesText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _finalWaveText;
    [SerializeField] private ProgressView _progressView;

    [Space] 
    [SerializeField] private Image _fillImage;
    [SerializeField] private Color _beforeWaveColor;
    [SerializeField] private Color _waveColor;

    private bool _isBooping;
    private Sequence _boopingRoutine;

    public void SetWave(int currentWave, int wavesCount)
    {
        _wavesText.text = String.Format(WavesTextSample, currentWave, wavesCount);
        _fillImage.color = currentWave == 0 ? _beforeWaveColor : _waveColor;
    }

    public void UpdateTimer(float currentTime, float timeNormalized)
    {
        //print(currentTime);
        _timerText.text = String.Format(TimerSample, currentTime);
        _progressView.SetProgress(timeNormalized);

        if (currentTime is > 0.1f and <= 5)
        {
            if (_isBooping == false)
                StartBooping();
        }
        else
        {
            if (_isBooping)
                EndBooping();
        }
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

    private void StartBooping()
    {
        _isBooping = true;
        _boopingRoutine?.Kill();
        _boopingRoutine = DOTween.Sequence();
        _boopingRoutine.Append(transform.DOScale(1.1f, 0.5f).SetEase(Ease.Linear));
        _boopingRoutine.Append(transform.DOScale(1f, 0.5f).SetEase(Ease.Linear));
        _boopingRoutine.SetLoops(-1);
    }

    private void EndBooping()
    {
        _isBooping = false;
        _boopingRoutine?.Kill();
        transform.DOScale(1f, 0.3f);
    }
}