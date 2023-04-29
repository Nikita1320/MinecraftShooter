using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerCombatSystem playerCombatSystem;
    [Header("SelectedWeaponPanel")]
    [SerializeField] private TMP_Text nameWeaponText;
    [SerializeField] private Image weaponImage;
    [SerializeField] private TMP_Text clipWeaponText;
    [SerializeField] private TMP_Text granadeAmmountText;
    [SerializeField] private GameObject selectedWeaponPanel;

    [Header("InventoryPanel")]
    [SerializeField] private GameObject cellsConteiner;
    [SerializeField] private PlayerInventoryCell prefab;
    private List<PlayerInventoryCell> playerInventoryCells = new();

    private void Start()
    {
        playerCombatSystem.shootedEvent += RenderBulletInfo;
        playerCombatSystem.threwGrenadeEvent += RenderGrenadeInfo;
        playerCombatSystem.switchedWeaponEvent += RenderSelectedWeapon;
        playerCombatSystem.reloadedWeaponEvent += RenderBulletInfo;
    }
    private void InstantiateCell()
    {
        for (int i = 0; i < inventory.Weapons.Length; i++)
        {
            var cell = Instantiate(prefab, cellsConteiner.transform);
            cell.Init(inventory.Weapons[i].WeaponData, inventory.AccessableWeapon[inventory.Weapons[i]], inventory.LevelWeapon[inventory.Weapons[i]], i + 1);
            playerInventoryCells.Add(cell);
        }
    }
    public void DestroyCell()
    {
        for (int i = 0; i < playerInventoryCells.Count; i++)
        {
            Destroy(playerInventoryCells[i].gameObject);
        }
        playerInventoryCells.Clear();
    }
    private void OnEnable()
    {
        InstantiateCell();
    }
    private void OnDisable()
    {
        DestroyCell();
        selectedWeaponPanel.SetActive(false);
    }
    private void RenderSelectedWeapon()
    {
        selectedWeaponPanel.SetActive(true);
        nameWeaponText.text = playerCombatSystem.SelectedWeapon.WeaponData.NameWeapon;
        weaponImage.sprite = playerCombatSystem.SelectedWeapon.WeaponData.WeaponSprite;
        RenderBulletInfo();
        RenderGrenadeInfo();
    }
    private void RenderBulletInfo()
    {
        clipWeaponText.text = $"{playerCombatSystem.SelectedWeapon.RemainingBulletInClip}/{playerCombatSystem.SelectedWeapon.SizeClip}";
    }
    private void RenderGrenadeInfo()
    {
        granadeAmmountText.text = $"{playerCombatSystem.RemainingAmmountGranade}/{playerCombatSystem.MaxAmmountGranade}";
    }
}
