using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
using System;
using Unity.AI.Navigation;

[Serializable]
public struct CombinationEnemies
{
    [SerializeField] private EnemyBehaviour[] enemyes;

    public EnemyBehaviour[] Enemyes { get => enemyes;}
}
public class GameManager : MonoBehaviour
{
    [Header("MapSettings")]
    [SerializeField] private Map[] maps;
    [SerializeField] private Map selectedMap;

    [Header("RewardSettings")]
    [SerializeField] private Price baseRewardForWin;
    [SerializeField] private int increseRewardValue;
    [SerializeField] private RewardPanel rewardPanel;

    [Header("EnemySettings")]
    [SerializeField] private CombinationEnemies[] possibleCombinationEnemy;
    [SerializeField] private int baseAmmountEnemies;
    [SerializeField] private int increseAmmountEnemies;

    [Space(5)]
    [SerializeField] private Inventory inventory;
    [SerializeField] private Character character;
    [SerializeField] private InputConroller inputConroller;
    [SerializeField] private GameObject hand;
    [SerializeField] private PlayerCombatSystem playerCombatSystem;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovementController playerMovementController;

    [Space(5)]
    
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Image loadPanel;
    [SerializeField] private float timeFade;

    [SerializeField] private TMP_Text ammountEnemies;

    [SerializeField] private SaveLoadSystem saveLoadSystem;
    private List<GameObject> enemies = new();
    private int currentLevel = 0;
    private int startAmmountEnemies;
    private int remainingEnemies;
    public Action endedBattle;
    private void Start()
    {
        currentLevel = saveLoadSystem.LoadGameLevel();
        levelText.text = $"Level: {currentLevel}";
        SetMaps();
        inputConroller.gameObject.transform.position = selectedMap.PlayerPosition.position;
        inputConroller.gameObject.transform.rotation = selectedMap.PlayerPosition.rotation;
        Camera.main.transform.localRotation = Quaternion.identity;
    }
    public void LoadLevel()
    {
        StartCoroutine(FadeLoadPanel(false, StartGame,timeFade));
    }
    public void StartGame()
    {
        mainPanel.SetActive(false);

        InstantiateEnemies();

        foreach (var enemy in enemies)
        {
            enemy.SetActive(true);
        }
        startAmmountEnemies = enemies.Count;
        remainingEnemies = startAmmountEnemies;
        RenderEnemiesAmmount();

        hand.SetActive(true);
        inputConroller.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        InitializePlayer();

        playerHealth.diedEvent += OnDIePlayer;
        gamePanel.SetActive(true);
        StartCoroutine(FadeLoadPanel(true, null, 2));
    }

    private void SetMaps()
    {
        if (selectedMap != null)
        {
            selectedMap.gameObject.SetActive(false);
        }
        selectedMap = maps[UnityEngine.Random.Range(0, maps.Length)];
        selectedMap.gameObject.SetActive(true);
    }
    private EnemyBehaviour[] GetPossibleEnemyes()
    {
        return possibleCombinationEnemy[UnityEngine.Random.Range(0, possibleCombinationEnemy.Length)].Enemyes;
    }
    private void InitializePlayer()
    {
        bool[] access = new bool[inventory.AccessableWeapon.Count];
        inventory.AccessableWeapon.Values.CopyTo(access, 0);

        int[] improvement = new int[inventory.LevelWeapon.Count];
        inventory.LevelWeapon.Values.CopyTo(improvement, 0);

        playerCombatSystem.Initialize(access, improvement, character.AmmountGrenade);
        playerHealth.InitializeStat(character.HealthPoint);
        playerMovementController.Initialize(character.Speed);
        playerMovementController.enabled = true;
        inputConroller.GetComponent<Collider>().enabled = true;
    }
    private void InstantiateEnemies()
    {
        var ammountEnemies = baseAmmountEnemies + increseAmmountEnemies * currentLevel;
        EnemyBehaviour[] possibleEnemyes = GetPossibleEnemyes();
        for (int i = 0; i < ammountEnemies; i++)
        {
            EnemyBehaviour enemy = Instantiate(possibleEnemyes[UnityEngine.Random.Range(0, possibleEnemyes.Length)]);
            enemy.gameObject.SetActive(false);
            enemies.Add(enemy.gameObject);
            enemy.SetTarget(inputConroller.gameObject);
            enemy.GetComponent<EnemyHealth>().diedEvent += OnDIeEnemy;
            enemy.transform.position = GetRandomPointSpawn();
        }
    }
    private Vector3 GetRandomPointSpawn()
    {
        Transform area = selectedMap.SpawnEnemyPoints[UnityEngine.Random.Range(0, selectedMap.SpawnEnemyPoints.Count)];
        Vector3 spawnPoint = new Vector3(area.transform.position.x + UnityEngine.Random.Range(0, selectedMap.RangeArea), area.transform.position.y, area.transform.position.z + UnityEngine.Random.Range(0, selectedMap.RangeArea));
        return spawnPoint;
    }
    public void Win()
    {
        //smth action
        currentLevel++;
        saveLoadSystem.SaveGameLevel(currentLevel);
        levelText.text = $"Level: {currentLevel}";
        rewardPanel.OpenRewardPanel(new Price(baseRewardForWin.TypeResource, baseRewardForWin.Cost + increseRewardValue * currentLevel));
        endedBattle?.Invoke();
        StartCoroutine(FadeLoadPanel(false, Reset, timeFade));
    }
    public void Loose()
    {
        //smth action
        inputConroller.enabled = false;
        endedBattle?.Invoke();
        inputConroller.GetComponent<Collider>().enabled = false;
        StartCoroutine(FadeLoadPanel(false, Reset, timeFade));
    }
    private void OnDIePlayer()
    {
        Loose();
    }
    private void OnDIeEnemy()
    {
        remainingEnemies--;
        RenderEnemiesAmmount();
        if (remainingEnemies == 0)
        {
            Win();
        }
    }
    private void Reset()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
        enemies.Clear();

        SetMaps();

        inputConroller.enabled = false;
        playerMovementController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        hand.SetActive(false);
        gamePanel.SetActive(false);
        mainPanel.SetActive(true);

        inputConroller.transform.position = selectedMap.PlayerPosition.position;
        inputConroller.transform.rotation = selectedMap.PlayerPosition.rotation;
        Camera.main.transform.localRotation = Quaternion.identity;

        inputConroller.gameObject.GetComponent<PlayerHealth>().Reset();
        inputConroller.gameObject.GetComponent<PlayerCombatSystem>().Reset();

        StartCoroutine(FadeLoadPanel(true, null, 1, 2));
    }
    private void RenderEnemiesAmmount()
    {
        ammountEnemies.text = $"{startAmmountEnemies - remainingEnemies}/{startAmmountEnemies}";
    }
    private IEnumerator FadeLoadPanel(bool isOpened, Action faded, float time, float delayTime = 0)
    {
        var remainingTimeFade = time;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            remainingTimeFade -= Time.deltaTime;
            if (delayTime <= 0)
            {
                if (isOpened)
                {
                    loadPanel.color = new Color(loadPanel.color.r, loadPanel.color.g, loadPanel.color.b, remainingTimeFade / timeFade);
                }
                else
                {
                    loadPanel.color = new Color(loadPanel.color.r, loadPanel.color.g, loadPanel.color.b, 1 - remainingTimeFade / timeFade);
                }
                if (remainingTimeFade <= 0)
                {
                    faded?.Invoke();
                    break;
                }
            }
            else
            {
                delayTime -= Time.deltaTime;
            }
        }

    }
}
