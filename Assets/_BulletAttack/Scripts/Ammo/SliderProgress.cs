using UnityEngine;
using UnityEngine.UI;

public class SliderProgress : ProgressView
{
    [SerializeField] private Slider _progressBar;

    public override void SetProgress(float progress)
    {
        var progressNormalized = Mathf.Clamp01(progress);
        _progressBar.normalizedValue = progressNormalized;
    }
}