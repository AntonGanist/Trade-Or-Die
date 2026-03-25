using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [SerializeField] NpcConfig _config;
    [SerializeField] NpcDialogPlayback _npcDialogPlayback;
    [SerializeField] ComingNpc _comingNpc;
    [SerializeField] int _numberOfGuests;

    Transform _spawn;
    GameObject _currentNpc;
    List<ItemCost> _itemsWithModificator = new();
    float _wallet;

    List<NpcSettings> _npcSettings;
    public void Initialize()
    {
        _comingNpc.Initialize(NpcIsHere);
        _spawn = _npcDialogPlayback.transform;
        _npcSettings = new List<NpcSettings>(_config.NpcSettings);

    }
    void NpcIsHere()
    {
        if(_numberOfGuests == 0) return;
        //_numberOfGuests--;

        int i = Random.Range(0, _npcSettings.Count);
        NpcSettings npcSettings = _npcSettings[i];

        _currentNpc = Instantiate(npcSettings.Model, _spawn.position, _spawn.rotation);
        _npcDialogPlayback.TakeDialogData(npcSettings.DialogData);
        //_npcSettings.Remove(npcSettings);
        _itemsWithModificator = npcSettings.ItemsWithModificator;
        _wallet = npcSettings.Wallet;
    }
    public void SaleCompleted()
    {
        Destroy(_currentNpc);
        _npcDialogPlayback.ClearData();
        _comingNpc.StartExpectation();
    }

    public List<ItemCost> GetPreferences() => _itemsWithModificator;
    public float GetWallet() => _wallet;
}
