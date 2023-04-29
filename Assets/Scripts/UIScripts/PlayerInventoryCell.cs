using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryCell : MonoBehaviour
{
    [SerializeField] private WeaponData weapon;
    [SerializeField] private bool isOpen;
    [SerializeField] private Image weaponImage;
    [SerializeField] private Text keyText;
    [SerializeField] private Image closeImage;
    [SerializeField] private Image[] improvementImages;
    [SerializeField] private Button button;

    public Button Button { get => button; }

    public void Init(WeaponData weapon, bool isOpen, int level, int key)
    {
        this.weapon = weapon;
        this.isOpen = isOpen;
        weaponImage.sprite = weapon.WeaponSprite;
        keyText.text = key.ToString();
        closeImage.gameObject.SetActive(!isOpen);
        RenderImprovement(level);
    }
    private void RenderImprovement(int level)
    {
        for (int i = 0; i < improvementImages.Length; i++)
        {
            if (i < level)
            {
                improvementImages[i].gameObject.SetActive(true);
            }
        }
    }
}
