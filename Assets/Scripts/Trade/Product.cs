using TMPro;
using UnityEngine;

public class Product : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    int _price;
    int _id;
    public int GetId() => _id;
    public void TakeId(int id) => _id = id;
    public void TakePrice(float price)
    {
        _price = (int)price;
        _text.text = _price.ToString();
    }
    public int GetPrice() => _price;
}
