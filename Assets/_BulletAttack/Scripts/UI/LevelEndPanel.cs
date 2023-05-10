using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelEndPanel : MonoBehaviour
{
    public Action NextClicked;

    [SerializeField] private CanvasGroup _canvasGroup;

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

    public void Next()
    {
        Close();
        NextClicked?.Invoke();
    }
}
