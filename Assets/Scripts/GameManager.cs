using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] InitializerDialogComponents _initializerDialogComponents;
    [SerializeField] PlayerClass _player;
    [SerializeField] FixItemManager _fixArea;
    [SerializeField] TradeManager _trade;
    [SerializeField] ItemConteiner _itemConteiner;
    [SerializeField] NpcManager _npcManager;
    [SerializeField] Wallet _wallet;

    private void Awake() => Initialize();
    public void Initialize()
    {
        _player.Initialize(_trade.StartTrade);
        _initializerDialogComponents.Initialize(_player.GetMouseLooks(), _player.GetMove(), _player.GetDialogPlayback());
        _fixArea.Initialize(_player.GetMouseLooks(), _player.GetMove());

        _trade.Initialize(_npcManager.GetPreferences, _npcManager.GetWallet, _initializerDialogComponents.PanelOff, 
_initializerDialogComponents.StopDialog, _initializerDialogComponents.ChangeCameraPosition, _itemConteiner.GetItems, 
_itemConteiner.RemoveItem, _npcManager.SaleCompleted, _wallet.TakeTicket);

        _itemConteiner.Initialize();
        _npcManager.Initialize();
    }
}
