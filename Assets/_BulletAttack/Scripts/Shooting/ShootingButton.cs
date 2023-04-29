using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action<AmmoData> OnButtonPressed;
    public Action<AmmoData> OnButtonUpressed;

    [SerializeField] private TextMeshProUGUI _ammoCount;
    
    private AmmoData _currentData;
    private bool _isInteractable;

    public void Init(AmmoData ammoData, int ammoCount)
    {
        _currentData = ammoData;
        UpdateAmmoCount(ammoCount);
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