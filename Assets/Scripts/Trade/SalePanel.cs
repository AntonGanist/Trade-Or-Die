using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalePanel : ChangeObjectPosition
{
    [SerializeField] Transform[] _points;
    List<Product> _products = new();
    List<ItemSettings> _items = new();

    List<ItemCost> _itemsWithModificator = new();
    float _wallet;
    float _endPrice;

    public void TakeTradingPreferences(List<ItemCost> tradingPreferences, float wallet)
    {
        _itemsWithModificator = tradingPreferences;
        _wallet = wallet;
    }

    public int GetProductInShowcase() => _products.Count;

    public bool PostProductInShowcase(ItemSettings itemSettings)
    {
        Product product = Instantiate(itemSettings.Prefab, transform.position, transform.rotation);
        product.TakeId(itemSettings.Id);

        float finalPrice = FindPriceModifiers(product, itemSettings.Price);
        _endPrice += finalPrice;

        if (_endPrice > _wallet)
        {
            Debug.Log("Недостаточно денег!");
            Destroy(product.gameObject);
            _endPrice -= finalPrice;
            return false;
        }

        product.TakePrice(finalPrice);

        _products.Add(product);
        _items.Add(itemSettings);

        for (int i = 0; i < _points.Length; i++)
        {
            if (_points[i].childCount == 0)
            {
                product.transform.parent = _points[i];
                _object = product.transform;
                StartCoroutine(MoveToTarget(_points[i].position, _points[i].rotation));
                break;
            }
        }
        StartCoroutine(Cooldown());
        return true;
    }

    float FindPriceModifiers(Product product, float price)
    {
        float multiplikator = 1;
        for (int i = 0; i < _itemsWithModificator.Count; i++)
        {
            if (product.GetId() == _itemsWithModificator[i].Id)
            {
                multiplikator = _itemsWithModificator[i].Multiplier;
                break;
            }
        }

        return price * multiplikator;
    }

    public void RemoveProductFromShowcase(ItemSettings itemSettings)
    {
        _items.Remove(itemSettings);

        int I = 0;
        for(int i = 0; i < _products.Count;i++)
        {
            if (_products[i].GetId() == itemSettings.Id)
            {
                I = i;
                break;
            }
        }
        StartCoroutine(RemoveProduct(I));
        StartCoroutine(Cooldown());
    }
    IEnumerator RemoveProduct(int i)
    {
        float finalPrice = FindPriceModifiers(_products[i], _products[i].GetPrice());
        _endPrice -= finalPrice;

        _object = _products[i].transform;
        yield return StartCoroutine(MoveToTarget(transform.position, transform.rotation));
        Destroy(_products[i].gameObject);
        _products.RemoveAt(i);
    }
    
    public void ClearProducts()
    {
        for(int i = 0; i< _products.Count; i++)
            Destroy(_products[i].gameObject);
        _products = new();
    }

    public List<ItemSettings> GetItems() => _items;

    public int GetRevenue()
    {
        int revenue = 0;
        for (int i = 0; i < _products.Count; i++)
            revenue += _products[i].GetPrice();
        return revenue;
    }

}
