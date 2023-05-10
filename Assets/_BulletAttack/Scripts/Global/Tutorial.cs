using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject _moveTutorial;
    [SerializeField] private GameObject _shootTutorial;

    private Context _currentContext;
    private TutorialLevel _level;

    public void StartTutorial(Context context, TutorialLevel level)
    {
        _currentContext = context;
        _level = level;
        _moveTutorial.SetActive(true);
        context.UI.Joystick.Pressed += StartAmmoPart;
    }

    private void StartAmmoPart()
    {
        _currentContext.UI.Joystick.Pressed -= StartAmmoPart;

        _moveTutorial.SetActive(false);
        _level.CollectAmmoTutorial.SetActive(true);

        _currentContext.Spawner.WaveSpawned += TryGoToWeaponStep;
    }

    private void TryGoToWeaponStep()
    {
        _level.CollectAmmoTutorial.SetActive(false);
        _level.MoveToTurretTutorial.SetActive(true);
        _currentContext.Spawner.WaveSpawned -= TryGoToWeaponStep;

        _currentContext.Player.PlayerController.GotToWeapon += GoToShootState;
    }

    private void GoToShootState()
    {
        _level.MoveToTurretTutorial.SetActive(false);
        _shootTutorial.SetActive(true);

        _currentContext.Player.PlayerController.GotToWeapon -= GoToShootState;
        _currentContext.UI.ShootingMenu.Shot += CloseShootTutorial;
    }

    private void CloseShootTutorial()
    {
        _currentContext.UI.ShootingMenu.Shot -= CloseShootTutorial;
        _shootTutorial.SetActive(false);
    }
}