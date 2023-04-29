using UnityEngine;
using UnityEngine.UI;

public class WeaponDescriptionPanel : MonoBehaviour
{
    [SerializeField] private Text damageText;
    [SerializeField] private Text clipText;
    [SerializeField] private Text rateOfFireText;

    public void RenderDescription(WeaponData weaponData, int level)
    {
        damageText.text = $"Урон: {weaponData.GetDamageValue(level)}";

        clipText.text = $"Магазин: {weaponData.GetClipValue(level)}";

        rateOfFireText.text = $"Скорострельность: {weaponData.GetRateOfFireValue(level)}";
    }
}
