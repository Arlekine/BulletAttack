using System;

[Serializable]
public class Upgrade
{
    private int _defaultPrice;
    private int _priceForLevel;
    private string _name;

    private int _level;
    private Action OnUpgrade;

    public string Name => _name;
    public int Level => _level;
    public int NextPrice => _defaultPrice + _priceForLevel * _level;

    public event Action<int> Upgraded;

    public Upgrade(int defaultPrice, int priceForLevel, string name, int level, Action onUpgrade)
    {
        _defaultPrice = defaultPrice;
        _priceForLevel = priceForLevel;
        _name = name;
        _level = level;
        OnUpgrade = onUpgrade;

        for (int i = 0; i < level; i++)
        {
            onUpgrade.Invoke();
        }
    }

    public void InvokeUpgrade()
    {
        _level++;
        OnUpgrade.Invoke();

        Upgraded?.Invoke(_level);
    }
}