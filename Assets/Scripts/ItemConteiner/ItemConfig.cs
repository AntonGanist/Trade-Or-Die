using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ItemConfig", fileName = "ItemConfig")]
public class ItemConfig : ScriptableObject
{
    public List<ItemSettings> ItemsSettings;
}

[Serializable]
public class ItemSettings
{
    public int Id;
    public string Name;
    public Product Prefab;
    public int Price;
}