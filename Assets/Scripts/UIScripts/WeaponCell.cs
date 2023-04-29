using UnityEngine;
using UnityEngine.UI;

public class WeaponCell : MonoBehaviour
{
    [SerializeField] private Image weaponImage;
    [SerializeField] private Image closeImage;
    public void Init(WeaponData weaponData, bool isOpen)
    {
        weaponImage.sprite = weaponData.WeaponSprite;
        closeImage.gameObject.SetActive(!isOpen);
    }
}
