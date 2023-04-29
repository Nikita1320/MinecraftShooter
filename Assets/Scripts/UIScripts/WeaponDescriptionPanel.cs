using UnityEngine;
using UnityEngine.UI;

public class WeaponDescriptionPanel : MonoBehaviour
{
    [SerializeField] private Text damageText;
    [SerializeField] private Text clipText;
    [SerializeField] private Text rateOfFireText;

    public void RenderDescription(WeaponData weaponData, int level)
    {
        damageText.text = $"����: {weaponData.GetDamageValue(level)}";

        clipText.text = $"�������: {weaponData.GetClipValue(level)}";

        rateOfFireText.text = $"����������������: {weaponData.GetRateOfFireValue(level)}";
    }
}
