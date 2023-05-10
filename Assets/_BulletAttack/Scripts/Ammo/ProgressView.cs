using DG.Tweening;
using UnityEngine;

public abstract class ProgressView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public abstract void SetProgress(float progress);

    public void Show()
    {
        _canvasGroup.DOFade(1f, 0.3f);
    }

    public void Hide()
    {
        _canvasGroup.DOFade(0f, 0.3f);
    }
}