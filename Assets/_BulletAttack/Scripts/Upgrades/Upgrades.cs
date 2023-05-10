using System;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    private const string CollectionSpeedUpgradeName = "Collection Speed";
    private const string CollectionRangeUpgradeName = "Collection Range";
    private const string EnemySlowdownUpgradeName = "Slow Enemy";

    public Action Upgraded;

    [SerializeField] private UpgradesMenu _menu;

    [Space]
    [SerializeField] private int _defaultUpgradePrice;
    [SerializeField] private int _additionalPriceForLevel;

    [Space] 
    [SerializeField] private float _collectionSpeedForLevel = 0.1f;
    [SerializeField] private float _enemySlowdownForLevel = 0.5f;
    [SerializeField] private float _collectionRangeForLevel = 0.25f;

    [Space] 
    [SerializeField] private Sprite _collectionSpeedIcon;
    [SerializeField] private Sprite _collectionRangeIcon;
    [SerializeField] private Sprite _enemySpeedIcon;

    private List<Upgrade> _upgrades = new List<Upgrade>();

    public void Init(UpgradesSaveData saves, Context context)
    {
        var speedUpgrade = new Upgrade(_defaultUpgradePrice, _additionalPriceForLevel, CollectionSpeedUpgradeName,
            saves[CollectionSpeedUpgradeName],
            () =>
            {
                context.Player.AmmoCollector.AddSpeed(_collectionSpeedForLevel);
                context.Player.PlayerGrower.IncreaseGrowth();
                Upgraded?.Invoke();
            });

        var rangeUpgrade = new Upgrade(_defaultUpgradePrice, _additionalPriceForLevel, CollectionRangeUpgradeName,
            saves[CollectionRangeUpgradeName],
            () =>
            {
                context.Player.AmmoCollector.AddRange(_collectionRangeForLevel);
                context.Player.PlayerGrower.IncreaseGrowth();
                Upgraded?.Invoke();
            });

        var enemySlowUpgrade = new Upgrade(_defaultUpgradePrice, _additionalPriceForLevel, EnemySlowdownUpgradeName, saves[EnemySlowdownUpgradeName],
            () =>
            {
                context.Spawner.SetSlowdown(_enemySlowdownForLevel);
                Upgraded?.Invoke();
            });

        speedUpgrade.Upgraded += (lvl) => saves[CollectionSpeedUpgradeName] = lvl;
        rangeUpgrade.Upgraded += (lvl) => saves[CollectionRangeUpgradeName] = lvl;
        enemySlowUpgrade.Upgraded += (lvl) => saves[EnemySlowdownUpgradeName] = lvl;

        _upgrades.Add(speedUpgrade);
        _upgrades.Add(rangeUpgrade);
        _upgrades.Add(enemySlowUpgrade);

        _menu.AddUpgrade(speedUpgrade, _collectionSpeedIcon, context.Money);
        _menu.AddUpgrade(rangeUpgrade, _collectionRangeIcon, context.Money);
        _menu.AddUpgrade(enemySlowUpgrade, _enemySpeedIcon, context.Money);
    }
}