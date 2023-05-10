using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _uiClick;
    [SerializeField] private AudioSource _win;
    [SerializeField] private AudioSource _loose;
    [SerializeField] private AudioSource _collect;
    [SerializeField] private AudioSource _upgrade;
    [SerializeField] private AudioSource _weaponAmmo;

    public AudioSource UiClick => _uiClick;
    public AudioSource Win => _win;
    public AudioSource Loose => _loose;
    public AudioSource Collect => _collect;
    public AudioSource Upgrade => _upgrade;
    public AudioSource WeaponAmmo => _weaponAmmo;
}