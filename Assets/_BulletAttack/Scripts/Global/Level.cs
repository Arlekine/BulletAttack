using System;
using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    public event Action Win;
    public event Action Lost;

    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Health _wall;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private WeaponScaler _weaponScaler;
    [SerializeField] private UpgradingZone _upgradingZone;
    [SerializeField] private float _hpBarHideOffset = 3f;

    [Space] 
    [SerializeField] private EnemyWave[] _waves;

    private Context _currentContext;

    public void PlacePlayer(Player player)
    {
        player.gameObject.SetActive(true);
        player.transform.position = _playerSpawnPosition.position;
        player.transform.forward = _playerSpawnPosition.forward;
        _weaponScaler.SetPlayer(player.PlayerGrower);
    }

    public void StartLevel(Context context)
    {
        _currentContext = context;
        
        _currentContext.Player.AmmoCollector.gameObject.SetActive(true);
        _currentContext.UI.WallHp.SetProgress(1f);
        _currentContext.Player.PlayerController.CharacterController.enabled = true;

        _currentContext.UI.ShootingMenu.SetWeapon(_weapon);
        context.Spawner.SetWaves(_waves, _attackPoint, _wall, _weapon);
        context.Spawner.WavesCleared += OnWavesCleared;

        _upgradingZone.SetUpgradesMenu(_currentContext.UI.UpgradesMenu);

        _wall.OnHit += OnWallHitted;
        _wall.OnDead += Lose;
    }

    private void OnDestroy()
    {
        if (_currentContext != null)
            _currentContext.Spawner.WavesCleared -= OnWavesCleared;
    }

    private void OnWallHitted(Health health)
    {
        _currentContext.UI.WallHp.Show();
        StopAllCoroutines();
        StartCoroutine(WallHPHideRoutine());

        _currentContext.UI.WallHp.SetProgress(health.HealthNormalized);
    }

    private void OnWavesCleared()
    {
        _currentContext.Player.AmmoInventory.Clear();
        _currentContext.Spawner.StopSpawing();
        _currentContext.Player.transform.parent = null;
        _currentContext.UI.WallHp.Hide();
        _currentContext.UI.ShootingMenu.CloseMenu();
        _currentContext.Player.IkController.Deactivate();
        _currentContext.Player.IkController.SetStandart();
        _currentContext.Player.PlayerController.ResetPlayer();
        _currentContext.Camera.OnOutOfWeapon();
        _currentContext.UI.WallHp.Hide();
        _currentContext.Player.AmmoCarrier.Clear();
        _currentContext.Player.AmmoCollector.gameObject.SetActive(false);

        Win?.Invoke();
    }

    private void Lose(Health health)
    {
        _currentContext.Player.AmmoInventory.Clear();
        _currentContext.Spawner.StopSpawing();
        _currentContext.Player.transform.parent = null;
        _currentContext.UI.WallHp.Hide();
        _currentContext.UI.ShootingMenu.CloseMenu();
        _currentContext.Camera.OnOutOfWeapon();
        _currentContext.Player.IkController.Deactivate();
        _currentContext.Player.IkController.SetStandart();
        _currentContext.Player.PlayerController.ResetPlayer();
        _currentContext.UI.WallHp.Hide();
        _currentContext.Player.AmmoCarrier.Clear();
        _currentContext.Player.AmmoCollector.gameObject.SetActive(false);

        Lost?.Invoke();
    }

    private IEnumerator WallHPHideRoutine()
    {
        yield return new WaitForSeconds(_hpBarHideOffset);
        _currentContext.UI.WallHp.Hide();
    }
}