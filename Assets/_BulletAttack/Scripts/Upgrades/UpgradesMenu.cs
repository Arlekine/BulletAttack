using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UpgradesMenu : MonoBehaviour
{
    [SerializeField] private UpgradeView _viewPrefab;
    [SerializeField] private Transform _viewsParent;
    [SerializeField] private CanvasGroup _canvasGroup;

    private List<UpgradeView> _currentViews = new List<UpgradeView>();

    public void AddUpgrade(Upgrade upgrade, Sprite upgradeIcon, Money money)
    {
        var newView = Instantiate(_viewPrefab, _viewsParent);
        newView.Init(upgrade, money, upgradeIcon);
        _currentViews.Add(newView);
    }

    [EditorButton]
    public void Open()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.DOFade(1f, 0.3f);
    }

    [EditorButton]
    public void Close()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOFade(0f, 0.3f);
    }
}