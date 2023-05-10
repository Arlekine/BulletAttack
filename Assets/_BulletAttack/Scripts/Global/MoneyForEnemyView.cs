using DG.Tweening;
using UnityEngine;

public class MoneyForEnemyView : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;

    [Space] 
    [SerializeField] private float _showTime;
    [SerializeField] private float _moveDelay;
    [SerializeField] private float _moveTime;

    private Sequence _currentSequence;

    public RectTransform RectTransform => _rectTransform;

    public Sequence Show(RectTransform targetMove)
    {
        _currentSequence = DOTween.Sequence();

        var showingSequence = DOTween.Sequence();
        showingSequence.Append(_rectTransform.DOScale(1f, _showTime).SetEase(Ease.OutBack));
        showingSequence.Join(_canvasGroup.DOFade(1f, _showTime));

        _currentSequence.Append(showingSequence);
        _currentSequence.AppendInterval(_moveDelay);
        _currentSequence.Append(_rectTransform.DOMove(targetMove.position, _moveTime));
        _currentSequence.onComplete += () =>
        {
            Destroy(gameObject);
        };

        return _currentSequence;
    }

    private void OnDestroy()
    {
        _currentSequence?.Kill();
    }
}