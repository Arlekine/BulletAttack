using UnityEngine;

public class Game : MonoBehaviour
{
    private const string GameDataPlayerPrefs = "GameData";

    [SerializeField] private TutorialLevel _tutorialLevel;
    [SerializeField] private Level[] _levels;
    [SerializeField] private Upgrades _upgrades;
    [SerializeField] private Context _context;
    [SerializeField] private MoneyView _moneyView;
    [SerializeField] private Tutorial _tutorial;
    [SerializeField] private GameObject _upgradesTutorial;

    private GameData _gameData;
    private Level _currentLevel;

    public GameData GameData => _gameData;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(GameDataPlayerPrefs))
        {
            _gameData = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(GameDataPlayerPrefs));
        }
        else
        {
            _gameData = new GameData();
            _gameData.UpgradesSaveData = new UpgradesSaveData();
            _gameData.Money = new Money();
            SaveData();
        }

        if (_levels.Length < _gameData.CurrentLevel)
        {
            _gameData.CurrentLevel = 0;
            SaveData();
        }

        if (_gameData.IsTutorial == false)
            LoadLevel(_levels[_gameData.CurrentLevel]);
        else
            LoadLevel(_tutorialLevel);

        _gameData.UpgradesSaveData.Updated += SaveData;
        _gameData.Money.Updated += SaveData;

        _upgrades.Init(_gameData.UpgradesSaveData, _context);
        _moneyView.Init(_gameData.Money);

        _context.UI.LosePanel.NextClicked += () =>
        {
            if (_gameData.IsTutorial)
                LoadLevel(_tutorialLevel);
            else
                LoadLevel(_levels[_gameData.CurrentLevel]);
        };

        _context.UI.WinPanel.NextClicked += () =>
        {
            if (_gameData.IsTutorial)
            {
                _gameData.IsTutorial = false;
                SaveData();
            }

            LoadLevel(_levels[_gameData.CurrentLevel]);
        };

        _context.UI.MoneyCollector.Init(_gameData.Money);
        
        AudioListener.volume = _gameData.SoundOn ? 1f : 0f;
    }

    public void LoadLevel(Level level)
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel.gameObject);
            _currentLevel.Win -= ShowWinMenu;
            _currentLevel.Lost -= ShowLoseMenu;
        }

        _currentLevel = Instantiate(level);
        _context.UI.Joystick.Pressed += StartCurrentLevel;

        if (_gameData.IsTutorial)
        {
            _tutorial.StartTutorial(_context, (TutorialLevel)_currentLevel);
        }

        if (_gameData.IsTutorial == false)
        {
            if (_gameData.IsUpgradesTutorial)
            {
                _upgradesTutorial.SetActive(true);
                _upgrades.Upgraded += UpgradesTutorialComplete;
            }

            _context.UI.UpgradesMenu.Open();
        }

        _currentLevel.PlacePlayer(_context.Player);
    }

    private void UpgradesTutorialComplete()
    {
        _gameData.IsUpgradesTutorial = false;
        _upgrades.Upgraded -= UpgradesTutorialComplete;
        _upgradesTutorial.SetActive(false);
        SaveData();
    }

    private void StartCurrentLevel()
    {
        _context.UI.Joystick.Pressed -= StartCurrentLevel;
        _context.UI.UpgradesMenu.Close();

        _currentLevel.StartLevel(_context);
        _currentLevel.Win += ShowWinMenu;
        _currentLevel.Lost += ShowLoseMenu;
    }

    private void ShowWinMenu()
    {
        _gameData.CurrentLevel++;

        if (_gameData.CurrentLevel >= _levels.Length)
            _gameData.CurrentLevel = 0;

        SaveData();

        _context.UI.WinPanel.Open();
    }

    private void ShowLoseMenu()
    {
        _context.UI.LosePanel.Open();
    }

    public void HapticSwitch(bool isActive)
    {
        _gameData.HapticOn = isActive;
        SaveData();
    }

    public void SoundSwitch(bool isActive)
    {
        _gameData.SoundOn = isActive;
        AudioListener.volume = _gameData.SoundOn ? 1f : 0f;
        SaveData();
    }

    [EditorButton]
    public void AddMoney()
    {
        _gameData.Money.AddMoney(1000);
    }

    private void SaveData()
    {
        PlayerPrefs.SetString(GameDataPlayerPrefs, JsonUtility.ToJson(_gameData));
    }
}