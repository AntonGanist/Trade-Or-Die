using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TradePanel : MonoBehaviour
{
    [SerializeField] Pencil _pencil;

    [SerializeField] Camera _camera;
    [SerializeField] LayerMask _layerMask;

    SalePanel _salePanel;
    SellButton _sellButton;

    bool _saleBool;
    Action _sale;
    public void Initialize(Action sale, SalePanel salePanel, SellButton sellButton)
    {
        _sale = sale;
        _salePanel = salePanel;
        _sellButton = sellButton;
    }
    void Update()
    {
        if (Mouse.current == null || _saleBool) return;

        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 999, _layerMask))
        {
            TradeButton trade = hit.collider.GetComponent<TradeButton>();
            if(trade != null && !_salePanel.GetCooldown())
            {
                _pencil.TakePosition(trade.transform.localPosition.y);

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (!trade.CheckProductPosition())
                    {
                        if(_salePanel.GetProductInShowcase() != 3)
                        {
                            bool canSale = _salePanel.PostProductInShowcase(trade.GetItem());
                            if(canSale)
                            {
                                trade.MoveProductInSalePanel(true);
                                _pencil.Subscribe("подписать");
                            }
                        }
                    }
                    else
                    {
                        _salePanel.RemoveProductFromShowcase(trade.GetItem());
                        trade.MoveProductInSalePanel(false);
                        _pencil.Subscribe("стереть");
                    }
                }
            }

            if (_sellButton.gameObject == hit.collider.gameObject && _salePanel.GetProductInShowcase() != 0)
            {
                _pencil.TakePosition(_sellButton.transform.localPosition.y);

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    _saleBool = true;
                    _pencil.Subscribe("подписать", SaleAnimation);
                    _sellButton.Sale(true);
                }
            }
        }
    }
    void SaleAnimation() => _sale.Invoke();

    public void ClearProducts() => _salePanel.ClearProducts();
    public void StartTrade() => _saleBool = false;
}
