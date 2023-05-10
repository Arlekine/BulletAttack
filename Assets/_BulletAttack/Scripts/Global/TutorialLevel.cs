using UnityEngine;

public class TutorialLevel : Level
{
    [SerializeField] private GameObject _collectAmmoTutorial;
    [SerializeField] private GameObject _moveToTurretTutorial;

    public GameObject CollectAmmoTutorial => _collectAmmoTutorial;

    public GameObject MoveToTurretTutorial => _moveToTurretTutorial;
}