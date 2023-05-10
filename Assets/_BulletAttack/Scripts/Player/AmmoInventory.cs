using System.Collections.Generic;
using UnityEngine;

public class AmmoInventory : MonoBehaviour
{
    private Dictionary<AmmoData, int> _ammos = new Dictionary<AmmoData, int>();

    public int this[AmmoData data] => _ammos[data];

    public int AmmoTotalCount
    {
        get
        {
            int totalCount = 0;
            foreach (var ammoData in GetAllAmmos())
            {
                totalCount += _ammos[ammoData];
            }

            return totalCount;
        }
    }

    public List<AmmoData> GetAllAmmos()
    {
        var ammos = new List<AmmoData>();

        foreach (var ammosKey in _ammos.Keys)
        {
            if (_ammos[ammosKey] > 0)
                ammos.Add(ammosKey);
        }

        return ammos;
    }

    public bool UseAmmo(AmmoData ammo)
    {
        if (_ammos[ammo] > 0)
        {
            _ammos[ammo]--;
            return true;
        }

        return false;
    }

    public void AddAmmo(AmmoData ammo)
    {
        if (_ammos.ContainsKey(ammo))
            _ammos[ammo]++;
        else
            _ammos[ammo] = 1;
    }

    public void Clear()
    {
        _ammos.Clear();
    }
}