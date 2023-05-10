using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private UpgradesMenu _upgradesMenu;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private LevelEndPanel _winPanel;
    [SerializeField] private LevelEndPanel _losePanel;
    [SerializeField] private ProgressView _wallHP;
    [SerializeField] private ShootingMenu _shootingMenu;
    [SerializeField] private MoneyCollector _moneyCollector;
    [SerializeField] private SwitchButton _soundButton;
    [SerializeField] private SwitchButton _hapticButton;
    [SerializeField] private GameObject _settingsMenu;

    public UpgradesMenu UpgradesMenu => _upgradesMenu;
    public Joystick Joystick => _joystick;
    public LevelEndPanel WinPanel => _winPanel;
    public LevelEndPanel LosePanel => _losePanel;
    public ProgressView WallHp => _wallHP;
    public ShootingMenu ShootingMenu => _shootingMenu;

    public SwitchButton SoundButton => _soundButton;

    public SwitchButton HapticButton => _hapticButton;

    public MoneyCollector MoneyCollector => _moneyCollector;

    public GameObject SettingsMenu => _settingsMenu;
}