using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] private ShopWeaponCell cellPrefab;
    [SerializeField] private List<ShopWeaponCell> cells;
    [SerializeField] private GameObject contentCellsPanel;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Bank bank;

    private void Start()
    {
        InstantiateShopCell();
    }
    public void ImproveWeapon(ShopWeaponCell weaponCell)
    {
        Price price = weaponCell.Weapon.WeaponData.GetUpgradePrice(inventory.LevelWeapon[weaponCell.Weapon]);
        if (bank.GetResource(price.TypeResource).ChangeAmmount(-price.Cost))
        {
            inventory.Improve(weaponCell.Weapon);
            weaponCell.UpdateCellInformation(inventory.AccessableWeapon[weaponCell.Weapon], inventory.LevelWeapon[weaponCell.Weapon]);
        }
    }
    public void BuyWeapon(ShopWeaponCell weaponCell)
    {
        Price price = weaponCell.Weapon.WeaponData.BuyPrice;
        if (bank.GetResource(price.TypeResource).ChangeAmmount(-price.Cost))
        {
            inventory.OpenWeapon(weaponCell.Weapon);
            weaponCell.UpdateCellInformation(inventory.AccessableWeapon[weaponCell.Weapon], inventory.LevelWeapon[weaponCell.Weapon]);
        }
    }
    public void AdImproveWeapon(ShopWeaponCell weaponCell)
    {
        if (inventory.ImproveForAd(weaponCell.Weapon))
        {
            weaponCell.UpdateCellInformation(inventory.AccessableWeapon[weaponCell.Weapon], inventory.LevelWeapon[weaponCell.Weapon]);
        }
    }
    public void AdRentWeapon(ShopWeaponCell weaponCell)
    {
        if (inventory.RentWeapon(weaponCell.Weapon))
        {
            weaponCell.UpdateCellInformation(inventory.AccessableWeapon[weaponCell.Weapon], inventory.LevelWeapon[weaponCell.Weapon]);
        }
    }
    public void YandexImproveWeapon(ShopWeaponCell weaponCell)
    {
        if (inventory.YandexImproveWeapon(weaponCell.Weapon))
        {
            weaponCell.UpdateCellInformation(inventory.AccessableWeapon[weaponCell.Weapon], inventory.LevelWeapon[weaponCell.Weapon]);
        }
    }
    public void YandexBuyWeapon(ShopWeaponCell weaponCell)
    {
        if (inventory.YandexBuyWeapon(weaponCell.Weapon))
        {
            weaponCell.UpdateCellInformation(inventory.AccessableWeapon[weaponCell.Weapon], inventory.LevelWeapon[weaponCell.Weapon]);
        }
    }

    private void InstantiateShopCell()
    {
        foreach (var weapon in inventory.Weapons)
        {
            var cell = Instantiate(cellPrefab, contentCellsPanel.transform);
            cells.Add(cell);
            cell.InitCell(weapon, inventory.AccessableWeapon[weapon], inventory.LevelWeapon[weapon]);

            cell.BuyForGameResourceButton.CellButton.onClick.AddListener(() => BuyWeapon(cell));
            cell.BuyForYandexResourceButton.CellButton.onClick.AddListener(() => YandexBuyWeapon(cell));

            cell.ImprovementForGameResourceButton.CellButton.onClick.AddListener(() => ImproveWeapon(cell));
            cell.ImprovementForYandexResourceButton.CellButton.onClick.AddListener(() => AdImproveWeapon(cell));

            cell.RentForAdButton.onClick.AddListener(() => AdRentWeapon(cell));
            cell.ImproveForAdButton.onClick.AddListener(() => AdImproveWeapon(cell));
        }
    }
    private void OnEnable()
    {
        if (cells.Count > 0)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].UpdateCellInformation(inventory.AccessableWeapon[inventory.Weapons[i]], inventory.LevelWeapon[inventory.Weapons[i]]);
            }
        }
    }
}
