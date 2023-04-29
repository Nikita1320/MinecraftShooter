using UnityEngine;
using UnityEngine.UI;

public class SelectedWeaponCell : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private Image weaponImage;

    public bool IsEmpty => weapon == null ? true : false;
    public Weapon Weapon { get => weapon; }

    public void InitCell(Weapon weapon)
    {
        this.weapon = weapon;
        weaponImage.sprite = weapon.WeaponData.WeaponSprite;
        weaponImage.gameObject.SetActive(true);
    }
    public void ClearCell()
    {
        weaponImage.gameObject.SetActive(false);
        weapon = null;
    }
}
