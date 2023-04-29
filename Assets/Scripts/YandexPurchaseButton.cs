using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YandexPurchaseButton : MonoBehaviour
{
    [SerializeField] private Text priceText;
    [SerializeField] private Button cellButton;
    public Button CellButton => cellButton;
    public void Init(int price)
    {
        priceText.text = price.ToString();
    }
}
