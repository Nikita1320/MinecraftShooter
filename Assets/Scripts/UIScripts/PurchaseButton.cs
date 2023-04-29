using UnityEngine;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour
{
    [SerializeField] private PriceCell priceCell;
    [SerializeField] private Button cellButton;
    public Button CellButton => cellButton;
    public void Init(Price price)
    {
        priceCell.InitCell(price);
    }
}
