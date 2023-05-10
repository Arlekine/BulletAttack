using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private AmmoCollector _ammoCollector;
    [SerializeField] private AmmoCarrier _ammoCarrier;
    [SerializeField] private AmmoInventory _ammoInventory;
    [SerializeField] private PlayerGrower _playerGrower;
    [SerializeField] private IKController _ikController;

    public PlayerController PlayerController => _playerController;
    public AmmoCollector AmmoCollector => _ammoCollector;
    public AmmoCarrier AmmoCarrier => _ammoCarrier;
    public AmmoInventory AmmoInventory => _ammoInventory;
    public PlayerGrower PlayerGrower => _playerGrower;
    public IKController IkController => _ikController;
}