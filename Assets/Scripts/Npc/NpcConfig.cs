using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/NpcConfig", fileName = "NpcConfig")]
public class NpcConfig : ScriptableObject
{
    public List<NpcSettings> NpcSettings;
}

[Serializable]
public class NpcSettings
{
    public List<ItemCost> ItemsWithModificator;
    public DialogData DialogData;
    public float Wallet;
    public GameObject Model;
}
[Serializable]
public class ItemCost
{
    public int Id;
    public float Multiplier;
}
[Serializable]
public class DialogData
{
    public string Name;
    public List<string> StartKnots;
}