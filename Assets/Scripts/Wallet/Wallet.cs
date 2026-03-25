using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] int _ticket;
    public void TakeTicket(int ticket) => _ticket += ticket;
}
