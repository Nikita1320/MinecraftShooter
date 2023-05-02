using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterImprovementPanel : MonoBehaviour
{
    [SerializeField] private int ammountImprovementForAdImprove;
    [SerializeField] private int offsetForOpenAdButtonHealth;
    [SerializeField] private int offsetForOpenAdButtonSpeed;
    [SerializeField] private int offsetForOpenAdButtonGrenade;

    [SerializeField] private Image progressPrefab;
    [SerializeField] private Sprite improvePointSprite;
    [SerializeField] private Sprite notImprovePointSprite;

    [SerializeField] private Text currentHealth;
    [SerializeField] private Text currentSpeed;
    [SerializeField] private Text currentGrenade;

    [SerializeField] private GameObject progressHealthImprovementPanel;
    [SerializeField] private GameObject progressSpeedImprovementPanel;
    [SerializeField] private GameObject progressGrenadeImprovementPanel;

    [SerializeField] private PurchaseButton purchaseHealthButton;
    [SerializeField] private Button adHealthButton;
    [SerializeField] private PurchaseButton purchaseSpeedButton;
    [SerializeField] private Button adSpeedButton;
    [SerializeField] private PurchaseButton purchaseGranateButton;
    [SerializeField] private Button adGrenadeButton;
    [SerializeField] private Character character;
    [SerializeField] private Bank bank;

    private List<Image> progressHealthImprovement = new();
    private List<Image> progressSpeedImprovement = new();
    private List<Image> progressGrenadeImprovement = new();

    private void Start()
    {
        InstanceIproveProgressImage();
        UpdateHealthProgressPanel();
        UpdateSpeedProgressPanel();
        UpdateGrenadeProgressPanel();

        purchaseHealthButton.CellButton.onClick.AddListener(ImproveHealth);
        adHealthButton.onClick.AddListener(AdImproveHealth);

        purchaseSpeedButton.CellButton.onClick.AddListener(ImproveSpeed);
        adSpeedButton.onClick.AddListener(AdImproveSpeed);

        purchaseGranateButton.CellButton.onClick.AddListener(ImproveGranate);
        adGrenadeButton.onClick.AddListener(AdImproveGranate);
    }
    private void InstanceIproveProgressImage()
    {
        for (int i = 0; i < character.MaxLevelHealth; i++)
        {
            var image = Instantiate(progressPrefab, progressHealthImprovementPanel.transform);
            progressHealthImprovement.Add(image);
            if (i < character.CurrentLevelHealth)
            {
                image.sprite = improvePointSprite;
            }
            else
            {
                image.sprite = notImprovePointSprite;
            }
        }

        for (int i = 0; i < character.MaxLevelSpeed; i++)
        {
            var image = Instantiate(progressPrefab, progressSpeedImprovementPanel.transform);
            progressSpeedImprovement.Add(image);
            if (i < character.CurrentLevelSpeed)
            {
                image.sprite = improvePointSprite;
            }
            else
            {
                image.sprite = notImprovePointSprite;
            }
        }

        for (int i = 0; i < character.MaxLevelGrenade; i++)
        {
            var image = Instantiate(progressPrefab, progressGrenadeImprovementPanel.transform);
            progressGrenadeImprovement.Add(image);
            if (i < character.CurrentLevelGrenade)
            {
                image.sprite = improvePointSprite;
            }
            else
            {
                image.sprite = notImprovePointSprite;
            }
        }
    }
    public void ImproveHealth()
    {
        Price price = character.CurrentUpgradeHealthPrice;
        if (bank.GetResource(price.TypeResource).ChangeAmmount(-price.Cost) && character.CurrentLevelHealth < character.MaxLevelHealth)
        {
            character.ImproveHealth();
            UpdateHealthProgressPanel();
        }
    }
    public void ImproveSpeed()
    {
        Price price = character.CurrentUpgradeSpeedPrice;
        if (bank.GetResource(price.TypeResource).ChangeAmmount(-price.Cost) && character.CurrentLevelSpeed < character.MaxLevelSpeed)
        {
            character.ImproveSpeed();
            UpdateSpeedProgressPanel();
        }
    }
    public void ImproveGranate()
    {
        Price price = character.CurrentUpgradeGrenadePrice;
        if (bank.GetResource(price.TypeResource).ChangeAmmount(-price.Cost) && character.CurrentLevelGrenade < character.MaxLevelGrenade)
        {
            character.ImproveGrenade();
            UpdateGrenadeProgressPanel();
        }
    }
    public void AdImproveHealth()
    {
        if (character.AdImproveHealth() && character.CurrentLevelHealth < character.MaxLevelHealth)
        {
            UpdateHealthProgressPanel();
        }
    }
    public void AdImproveSpeed()
    {
        if (character.AdImproveSpeed() && character.CurrentLevelSpeed < character.MaxLevelSpeed)
        {
            UpdateSpeedProgressPanel();
        }
    }
    public void AdImproveGranate()
    {
        if (character.AdImproveGrenade() && character.CurrentLevelGrenade < character.MaxLevelGrenade)
        {
            UpdateGrenadeProgressPanel();
        }
    }
    private void UpdateHealthProgressPanel()
    {
        if (character.CurrentLevelHealth < character.MaxLevelHealth)
        {
            purchaseHealthButton.Init(character.CurrentUpgradeHealthPrice);
            if (character.CurrentLevelHealth % (ammountImprovementForAdImprove + offsetForOpenAdButtonHealth) == 0)
            {
                purchaseHealthButton.gameObject.SetActive(false);
                adHealthButton.gameObject.SetActive(true);
            }
            else
            {
                purchaseHealthButton.gameObject.SetActive(true);
                adHealthButton.gameObject.SetActive(false);
            }
        }
        else
        {
            purchaseHealthButton.gameObject.SetActive(false);
            adHealthButton.gameObject.SetActive(false);
        }
        currentHealth.text = $"Здоровье: {character.HealthPoint}";

        if (character.CurrentLevelHealth > 0)
            progressHealthImprovement[character.CurrentLevelHealth - 1].sprite = improvePointSprite;
    }
    private void UpdateSpeedProgressPanel()
    {
        if (character.CurrentLevelSpeed < character.MaxLevelSpeed)
        {
            purchaseSpeedButton.Init(character.CurrentUpgradeSpeedPrice);
            if (character.CurrentLevelSpeed % (ammountImprovementForAdImprove + offsetForOpenAdButtonSpeed) == 0 && character.CurrentLevelSpeed != 0)
            {
                purchaseSpeedButton.gameObject.SetActive(false);
                adSpeedButton.gameObject.SetActive(true);
            }
            else
            {
                purchaseSpeedButton.gameObject.SetActive(true);
                adSpeedButton.gameObject.SetActive(false);
            }
        }
        else
        {
            purchaseSpeedButton.gameObject.SetActive(false);
            adSpeedButton.gameObject.SetActive(false);
        }
        currentSpeed.text = $"Скорость: {character.Speed}";

        if (character.CurrentLevelSpeed > 0)
            progressSpeedImprovement[character.CurrentLevelSpeed - 1].sprite = improvePointSprite;
    }
    private void UpdateGrenadeProgressPanel()
    {
        if (character.CurrentLevelGrenade < character.MaxLevelGrenade)
        {
            purchaseGranateButton.Init(character.CurrentUpgradeGrenadePrice);
            if (character.CurrentLevelGrenade % (ammountImprovementForAdImprove + offsetForOpenAdButtonGrenade) == 0 && character.CurrentLevelGrenade != 0)
            {
                purchaseGranateButton.gameObject.SetActive(false);
                adGrenadeButton.gameObject.SetActive(true);
            }
            else
            {
                purchaseGranateButton.gameObject.SetActive(true);
                adGrenadeButton.gameObject.SetActive(false);
            }
        }
        else
        {
            purchaseGranateButton.gameObject.SetActive(false);
            adGrenadeButton.gameObject.SetActive(false);
        }
        currentGrenade.text = $"Гранаты: {character.AmmountGrenade}";

        if (character.CurrentLevelGrenade > 0)
            progressGrenadeImprovement[character.CurrentLevelGrenade - 1].sprite = improvePointSprite;
    }
}
