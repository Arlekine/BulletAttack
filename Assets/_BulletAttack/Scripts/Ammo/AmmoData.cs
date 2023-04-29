using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Ammo", fileName = "Ammo")]
public class AmmoData : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private CarriedAmmo _carriedAmmo;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private CollectableAmmo _collectable;

    public Sprite Icon => _icon;
    public CarriedAmmo CarriedAmmo => _carriedAmmo;
    public Projectile Projectile => _projectile;
    public CollectableAmmo Collectable => _collectable;
}