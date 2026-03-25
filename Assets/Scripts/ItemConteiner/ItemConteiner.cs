using System.Collections.Generic;
using UnityEngine;

public class ItemConteiner : MonoBehaviour
{
    [SerializeField] ItemConfig _config;
    List<ItemSettings> _items;
    public void Initialize()
    {
        _items = new List<ItemSettings>(_config.ItemsSettings);
    }
    public List<ItemSettings> GetItems() => _items;
    public void RemoveItem(List<ItemSettings> items)
    {
        for(int i = 0; i < items.Count; i++) 
            _items.Remove(items[i]);
    }
}
