using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    [SerializeField] private Price reward;
    [SerializeField] private Image resourceImage;
    [SerializeField] private Text resourceAmmountText;
    [SerializeField] private int coefficientRewardForWatchingAd;
    [SerializeField] private Text coefficientRewardForWatchingAdText;
    [SerializeField] private Bank bank;
    private void Start()
    {
        coefficientRewardForWatchingAdText.text = $"x {coefficientRewardForWatchingAd}";
    }
    public void OpenRewardPanel(Price price)
    {
        reward = price;
        gameObject.SetActive(true);
        resourceImage.sprite = bank.GetResource(price.TypeResource).IconResource;
        resourceAmmountText.text = price.Cost.ToString();
    }
    public void TakeReward(bool isWatchingAd)
    {
        bank.GetResource(reward.TypeResource).ChangeAmmount(reward.Cost);
        gameObject.SetActive(false);
    }
    public void WatchingAd()
    {
        if (true)
        {
            TakeReward(true);
        }
        else
        {
            TakeReward(false);
        }
    }
}
