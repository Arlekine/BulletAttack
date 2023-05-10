using UnityEngine;

public class MoneyCollector : MonoBehaviour
{
    [SerializeField] private MoneyForEnemyView _collectedMoneyPrefab;
    [SerializeField] private RectTransform _moneySpawnZone;
    [SerializeField] private RectTransform _moneyMovePoint;
    [SerializeField] private float _moneyMoveTime = 0.35f;

    [Space]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private EnemieSpawner _enemySpawner;

    private Money _currentMoney;

    private void Start()
    {
        _enemySpawner.EnemyDead += OnEnemyDead;
    }

    public void Init(Money money)
    {
        _currentMoney = money;
    }

    private void OnEnemyDead(Enemy enemy)
    {
        var newMoney = Instantiate(_collectedMoneyPrefab, _moneySpawnZone);
        newMoney.RectTransform.anchoredPosition = _mainCamera.WorldToScreenPoint(enemy.transform.position);
        newMoney.Show(_moneyMovePoint).onComplete += () =>
        {
            _currentMoney.AddMoney(enemy.Currency);
        };
    }
}