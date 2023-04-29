using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopWeaponCell : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Text nameWeapon;
    [SerializeField] private Image weaponImage;
    [SerializeField] private GameObject lockPanel;
    [SerializeField] private WeaponDescriptionPanel descriptionPanel;
    [SerializeField] private Image[] improvementImages;

    [SerializeField] private PurchaseButton improvementForGameResourceButton;
    [SerializeField] private PurchaseButton buyForGameResourceButton;
    [SerializeField] private Button improveForAdButton;
    [SerializeField] private Button rentForAdButton;
    [SerializeField] private YandexPurchaseButton improvementForYandexResourceButton;
    [SerializeField] private YandexPurchaseButton buyForYandexResourceButton;
    private Weapon weapon;
    private int levelWeapon;

    public Weapon Weapon { get => weapon; }
    public int LevelWeapon { get => levelWeapon; }
    public YandexPurchaseButton BuyForYandexResourceButton { get => buyForYandexResourceButton; }
    public YandexPurchaseButton ImprovementForYandexResourceButton { get => improvementForYandexResourceButton; }
    public Button RentForAdButton { get => rentForAdButton; }
    public PurchaseButton BuyForGameResourceButton { get => buyForGameResourceButton; }
    public PurchaseButton ImprovementForGameResourceButton { get => improvementForGameResourceButton; }
    public Button ImproveForAdButton { get => improveForAdButton; }

    public void InitCell(Weapon weapon, bool access, int level)
    {
        this.weapon = weapon;
        levelWeapon = level;
        nameWeapon.text = weapon.WeaponData.NameWeapon;

        weaponImage.sprite = weapon.WeaponData.WeaponSprite;
        lockPanel.SetActive(!access);

        descriptionPanel.RenderDescription(weapon.WeaponData, level);
        RenderImprovement(levelWeapon);
        RenderActionButtons(access, level);
    }

    public void UpdateCellInformation(bool access, int level = 0)
    {
        levelWeapon = level;

        lockPanel.SetActive(!access);

        descriptionPanel.RenderDescription(weapon.WeaponData, level);
        RenderImprovement(levelWeapon);
        RenderActionButtons(access, level);
    }

    private void RenderImprovement(int level)
    {
        for (int i = 0; i < improvementImages.Length; i++)
        {
            if (i < level)
            {
                improvementImages[i].gameObject.SetActive(true);
            }
            else
            {
                improvementImages[i].gameObject.SetActive(false);
            }
        }
    }

    private void RenderActionButtons(bool access, int level)
    {
        if (access)
        {
            if (level == weapon.MaxLevel)
            {
                improvementForGameResourceButton.gameObject.SetActive(false);
                improveForAdButton.gameObject.SetActive(false);
                improvementForYandexResourceButton.gameObject.SetActive(false);

                buyForGameResourceButton.gameObject.SetActive(false);
                rentForAdButton.gameObject.SetActive(false);
                buyForYandexResourceButton.gameObject.SetActive(false);
            }
            else
            {
                improvementForGameResourceButton.gameObject.SetActive(true);
                improvementForGameResourceButton.Init(weapon.WeaponData.GetUpgradePrice(level));
                improveForAdButton.gameObject.SetActive(true);
                improvementForYandexResourceButton.gameObject.SetActive(true);
                improvementForYandexResourceButton.Init(weapon.WeaponData.YandexUpgradePrice[level]);

                buyForGameResourceButton.gameObject.SetActive(false);
                rentForAdButton.gameObject.SetActive(false);
                buyForYandexResourceButton.gameObject.SetActive(false);
            }
        }
        else
        {
            improvementForGameResourceButton.gameObject.SetActive(false);
            improveForAdButton.gameObject.SetActive(false);
            improvementForYandexResourceButton.gameObject.SetActive(false);

            buyForGameResourceButton.gameObject.SetActive(true);
            buyForGameResourceButton.Init(weapon.WeaponData.BuyPrice);
            rentForAdButton.gameObject.SetActive(true);
            buyForYandexResourceButton.gameObject.SetActive(true);
            buyForYandexResourceButton.Init(weapon.WeaponData.YandexBuyPrice);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionPanel.RenderDescription(weapon.WeaponData, levelWeapon);
        descriptionPanel.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.gameObject.SetActive(false);
    }
}
