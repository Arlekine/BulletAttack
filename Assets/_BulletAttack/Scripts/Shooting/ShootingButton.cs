using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShootingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action<AmmoData> OnButtonPressed;
    public Action<AmmoData> OnButtonUpressed;

    [SerializeField] private TextMeshProUGUI _ammoCount;
    [SerializeField] private Image _ammoIcon;
    
    private AmmoData _currentData;
    private bool _isInteractable;

    public void Init(AmmoData ammoData, int ammoCount)
    {
        _currentData = ammoData;
        UpdateAmmoCount(ammoCount);
        _ammoIcon.sprite = ammoData.Icon;
        Activate();
    }

    public void Activate()
    {
        _isInteractable = true;
    }

    public void Deactivate()
    {
        _isInteractable = false;
    }

    public void UpdateAmmoCount(int ammoCount)
    {
        _ammoCount.text = ammoCount.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isInteractable)
            OnButtonPressed?.Invoke(_currentData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnButtonUpressed?.Invoke(_currentData);
    }
}