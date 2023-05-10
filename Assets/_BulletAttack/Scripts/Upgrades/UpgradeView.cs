using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private const string LevelTextSample = "Level {0}";

    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private ColorButtonAnimation _buttonAnimation;

    private Upgrade _currentUpgrade;
    private Money _currentMoney;

    private bool _isPressed;
    private bool IsInteractable => _currentMoney.CurrentMoney >= _currentUpgrade.NextPrice;

    public void Init(Upgrade upgrade, Money money, Sprite icon)
    {
        _currentUpgrade = upgrade;
        _currentMoney = money;

        _level.text = String.Format(LevelTextSample, upgrade.Level + 1);
        _name.text = upgrade.Name;
        _price.text = upgrade.NextPrice.ToString();

        _icon.sprite = icon;
        money.Updated += OnMoneyUpdated;

        _buttonAnimation.Init();

        if (IsInteractable)
            _buttonAnimation.SetStandart();
        else
            _buttonAnimation.SetDisabled();
    }

    private void OnDestroy()
    {
        _currentMoney.Updated -= OnMoneyUpdated;
    }

    private void OnMoneyUpdated()
    {
        if (IsInteractable == false)
        {
            _buttonAnimation.SetDisabled();
        }
        else if (_isPressed == false)
        {
            _buttonAnimation.SetStandart();
        }
    }

    private void OnClick()
    {
        SoundManager.Instance.Upgrade.Play();
        var currentPrice = _currentUpgrade.NextPrice;
        _currentUpgrade.InvokeUpgrade();
        _currentMoney.Subtract(currentPrice);
        _price.text = _currentUpgrade.NextPrice.ToString();
        _level.text = String.Format(LevelTextSample, _currentUpgrade.Level + 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsInteractable)
        {
            _isPressed = true;
            _buttonAnimation.SetPressed();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsInteractable)
        {
            _buttonAnimation.SetStandart();
            OnClick();
            _isPressed = false;
        }
    }
}