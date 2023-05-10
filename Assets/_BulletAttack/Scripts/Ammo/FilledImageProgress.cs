using UnityEngine;
using UnityEngine.UI;

public class FilledImageProgress : ProgressView
{
    [SerializeField] private Image _progressBar;

    public override void SetProgress(float progress)
    {
        var progressNormalized = Mathf.Clamp01(progress);
        _progressBar.fillAmount = progressNormalized;
    }
}