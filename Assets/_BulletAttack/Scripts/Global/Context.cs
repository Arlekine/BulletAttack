using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Context : MonoBehaviour
{
    [SerializeField] private CameraController _camera;
    [SerializeField] private Player _player;
    [SerializeField] private EnemieSpawner _spawner;
    [SerializeField] private Game _game;
    [SerializeField] private UI _ui;

    public CameraController Camera => _camera;
    public Player Player => _player;
    public EnemieSpawner Spawner => _spawner;
    public Money Money => _game.GameData.Money;
    public UI UI => _ui;
}