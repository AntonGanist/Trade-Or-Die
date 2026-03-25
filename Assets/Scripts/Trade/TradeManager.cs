using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeManager : MonoBehaviour
{
    [SerializeField] TradePanel _tradePanel;
    [SerializeField] List<TradeButton> _tradeButtons;
    [SerializeField] SalePanel _salePanel;
    [SerializeField] SellButton _sellButton;

    [SerializeField] UniversalAnimated _animated;
    [SerializeField] Transform _dialogCamera;
    [SerializeField] TradeCamera _tradeCamera;

    Func<List<ItemCost>> _takePreferences;
    Func<float> _takeWallet;
    Action _panelOff;
    Action _exit;
    Action<Vector3> _changeCameraPosition;
    Func<List<ItemSettings>> _takeItems;
    Action<List<ItemSettings>> _saleItems;
    Action _endDialog;
    Action<int> _sendTickets;
    public void Initialize(Func<List<ItemCost>> takePreferences, Func<float> takeWallet, Action panelOff, Action exit, 
        Action<Vector3> changeCameraPosition,Func<List<ItemSettings>> takeItems, Action<List<ItemSettings>> saleItems, 
        Action endDialog, Action<int> sendTickets)
    {
        _tradeCamera.Initialize(_dialogCamera);
        _tradePanel.Initialize(SaleItems, _salePanel, _sellButton);

        _takePreferences = takePreferences;
        _takeWallet = takeWallet;
        _panelOff = panelOff;
        _exit = exit;
        _changeCameraPosition = changeCameraPosition;
        _takeItems = takeItems;
        _saleItems = saleItems;
        _endDialog = endDialog;
        _sendTickets = sendTickets;
    }
    public void StartTrade()
    {
        ButtonInitialize();

        _salePanel.TakeTradingPreferences(_takePreferences.Invoke(), _takeWallet.Invoke());

        _dialogCamera.gameObject.SetActive(false);
        _tradePanel.enabled = true;
        _tradePanel.StartTrade();
        _animated.StartAnimation("открыть");
        _tradeCamera.StartTrade();
        StartCoroutine(PanelOff());
    }
    IEnumerator PanelOff()
    {
        yield return null;
        _panelOff.Invoke();
    }
    void ButtonInitialize()
    {
        List<ItemSettings> items = _takeItems.Invoke();
        for(int i = 0; i < items.Count; i++)
        {
            _tradeButtons[i].SetActive(true);
            _tradeButtons[i].TakeItem(items[i]);
        }
    }

    void SaleItems()
    {
        _animated.StartAnimation("продано", EndTrade);
    }
    void EndTrade()
    {
        _sendTickets.Invoke(_salePanel.GetRevenue());

        _tradePanel.ClearProducts();
        _tradePanel.enabled = false;
        _animated.StartAnimation("закрыть");

        for (int i = 0; i < _tradeButtons.Count; i++)
        {
            _tradeButtons[i].SetActive(false);
            _tradeButtons[i].MoveProductInSalePanel(false);
        }
        _sellButton.Sale(false);

        _changeCameraPosition.Invoke(_tradeCamera.CameraPosition());
        _tradeCamera.CameraOff();
        _exit.Invoke();
        _saleItems.Invoke(_salePanel.GetItems());
        _endDialog.Invoke();

    }
}
