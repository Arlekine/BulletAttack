using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressView : MonoBehaviour
{
    [SerializeField] private Slider _progressBar;
    [SerializeField] private CanvasGroup _canvasGroup;

    public void SetProgress(float progress)
    {
        var progressNormalized = Mathf.Clamp01(progress);
        _progressBar.normalizedValue = progressNormalized;
    }

    public void Show()
    {
        _canvasGroup.DOFade(1f, 0.3f);
    }

    public void Hide()
    {
        _canvasGroup.DOFade(0f, 0.3f);
    }
}