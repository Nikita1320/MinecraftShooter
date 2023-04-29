using UnityEngine;
using UnityEngine.UI;

public class PriceCell : MonoBehaviour
{
    [SerializeField] private Image resourcesImage;
    [SerializeField] private Text costText;
    public void InitCell(Price price)
    {
        resourcesImage.sprite = Bank.Instance.GetResource(price.TypeResource).IconResource;
        costText.text = price.Cost.ToString();
    }
}
