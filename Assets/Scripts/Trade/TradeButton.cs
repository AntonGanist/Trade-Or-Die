using TMPro;
using UnityEngine;

public class TradeButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textName;
    [SerializeField] TextMeshProUGUI _textPrice;
    [SerializeField] GameObject _signature;
    ItemSettings _item;

    public void TakeItem(ItemSettings item)
    {
        _item = item;
        _textName.text = _item.Name;
        _textPrice.text = _item.Price.ToString();
    }
    public ItemSettings GetItem() => _item;

    public void MoveProductInSalePanel(bool sale) => _signature.SetActive(sale);
    public bool CheckProductPosition() => _signature.activeSelf;

    public void SetActive(bool active) => gameObject.SetActive(active);
}
