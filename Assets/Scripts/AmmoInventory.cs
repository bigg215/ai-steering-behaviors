using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType : int
{
    Basic,
    Shotgun,
    Rockets,
    Cells
}

public class AmmoInventory : MonoBehaviour {

	[System.Serializable]
    
    public struct AmmoEntry
    {
#if UNITY_EDITOR
        [HideInInspector]
        public string name;
#endif
        public int maxCapacity;
        public int stock;
    }

    [SerializeField]
    List<AmmoEntry> _inventory = new List<AmmoEntry>();

    public int GetStock(AmmoType type)
    {
        return _inventory[(int)type].stock;
    }

    public int GetMax(AmmoType type)
    {
        return _inventory[(int)type].maxCapacity;
    }

    public int Collect(AmmoType type, int amount)
    {
        AmmoEntry held = _inventory[(int)type];
        int collect = Mathf.Min(amount, held.maxCapacity - held.stock);
        held.stock += collect;
        _inventory[(int)type] = held;
        return collect;
    }

    public int Spend(AmmoType type, int amount)
    {
        AmmoEntry held = _inventory[(int)type];
        int spend = Mathf.Min(amount, held.stock);
        held.stock -= spend;
        _inventory[(int)type] = held;
        return spend;
    }

#if UNITY_EDITOR
    void Reset()
    {
        OnValidate();    
    }
    void OnValidate()
    {
        var ammoNames = System.Enum.GetNames(typeof(AmmoType));
        var inventory = new List<AmmoEntry>(ammoNames.Length);
        for(int i = 0; i < ammoNames.Length; i++)
        {
            var existing = _inventory.Find(
                (entry) => { return entry.name == ammoNames[i]; });
            existing.name = ammoNames[i];
            existing.stock = Mathf.Min(existing.stock, existing.maxCapacity);
            inventory.Add(existing);
        }
        _inventory = inventory;
    }
#endif
}