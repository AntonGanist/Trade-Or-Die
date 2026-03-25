using UnityEngine;

public class SellButton : MonoBehaviour
{
    [SerializeField] GameObject _signature;

    public void Sale(bool sale) => _signature.SetActive(sale);
}
