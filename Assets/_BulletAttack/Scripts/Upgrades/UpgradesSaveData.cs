using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradesSaveData
{
    [Serializable]
    private class UpgradeSave
    {
        [SerializeField] private string _name;
        [SerializeField] private int _level;

        public UpgradeSave(string name, int level)
        {
            _name = name;
            _level = level;
        }

        public string Name => _name;
        public int Level
        {
            get => _level;
            set => _level = value;
        }
    }

    [SerializeField] private List<UpgradeSave> _saves = new List<UpgradeSave>();

    public event Action Updated;

    public int this[string name]
    {
        get
        {
            var save = _saves.Find(x => x.Name == name);

            if (save == null)
            {
                _saves.Add(new UpgradeSave(name, 0));
                Updated?.Invoke();
                return 0;
            }

            return save.Level;
        }

        set
        {
            var save = _saves.Find(x => x.Name == name);

            if (save == null)
            {
                _saves.Add(new UpgradeSave(name, value));
                Updated?.Invoke();
                return;
            }

            save.Level = value;
            Updated?.Invoke();
        }
    }
}