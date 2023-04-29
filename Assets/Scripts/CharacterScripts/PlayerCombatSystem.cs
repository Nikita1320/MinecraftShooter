using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCombatSystem : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private Weapon selectedWeapon;

    [SerializeField] private Grenade grenadePref;
    [SerializeField] private float powerThrow;
    [SerializeField] private int maxAmmountGranade = 3;
    [SerializeField] private int remainingAmmountGranade = 3;

    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform grenadeSpawnPoint;

    private Weapon nextWeapon;
    private Dictionary<Weapon, bool> accesWeapon = new();
    [SerializeField] private bool isSwitching = false;
    [SerializeField] private bool isReloading = false;
    [SerializeField] private bool isThrowingGrenade = false;

    public Action shootedEvent;
    public Action threwGrenadeEvent;
    public Action initializedEvent;
    public Action switchedWeaponEvent;
    public Action reloadedWeaponEvent;
    public Weapon SelectedWeapon { get => selectedWeapon;}
    public int MaxAmmountGranade { get => maxAmmountGranade;}
    public int RemainingAmmountGranade { get => remainingAmmountGranade;}

    public void Shoot()
    {
        if (selectedWeapon != null && isThrowingGrenade == false && isSwitching == false && isReloading ==false)
        {
            if (selectedWeapon.Shoot())
            {
                Debug.Log("MakeShoot");
                animator.SetTrigger("Shoot");
                shootedEvent?.Invoke();
            }
            else
            {
                animator.ResetTrigger("Shoot");
            }
            if (selectedWeapon.RemainingBulletInClip == 0 && isReloading == false)
            {
                Debug.Log("RELOAD");
                StartReload();
            }
        }
    }
    public void StartReload()
    {
        animator.SetTrigger("Reload");
        isReloading = true;
    }
    public void Reload()
    {
        if (selectedWeapon != null && isThrowingGrenade == false && isSwitching == false && selectedWeapon.RemainingBulletInClip < selectedWeapon.MaxBulletInClip)
        {
            selectedWeapon.Reload();
            isReloading = false;
            animator.ResetTrigger("Reload");
            animator.ResetTrigger("Shoot");
            reloadedWeaponEvent?.Invoke();
            Debug.Log("RELOAD");
        }
    }
    public void StartThrowGrenade()
    {
        if (remainingAmmountGranade > 0 && isSwitching == false && isReloading == false && isThrowingGrenade == false)
        {
            isThrowingGrenade = true;
            animator.SetTrigger("TrowGrenade");
        }
    }
    public void ThrowGrenade()
    {
        var grenade = Instantiate(grenadePref, grenadeSpawnPoint);

        grenade.transform.localPosition = Vector3.zero;
        grenade.transform.rotation = Quaternion.identity;
        grenade.transform.parent = null;

        grenade.Rigidbody.AddForce(Camera.main.transform.forward * powerThrow, ForceMode.Impulse);
        grenade.Activate();
        remainingAmmountGranade--;
        threwGrenadeEvent?.Invoke();
        animator.ResetTrigger("TrowGrenade");
        isThrowingGrenade = false;
    }
    public void ActivateSelectedWeap0n()
    {
        selectedWeapon.gameObject.SetActive(true);
    }
    public void DeaActivateSelectedWeap0n()
    {
        selectedWeapon.gameObject.SetActive(false);
    }
    public void SelectWeapon(int numberWeapon)
    {
        Debug.Log(numberWeapon);
        if (selectedWeapon != weapons[numberWeapon] && isThrowingGrenade == false && isReloading == false)
        {
            if (accesWeapon.TryGetValue(weapons[numberWeapon], out bool value) && isSwitching == false)
            {
                if (value)
                {
                    isSwitching = true;
                    if (selectedWeapon != null)
                    {
                        selectedWeapon.gameObject.SetActive(false);
                    }
                    selectedWeapon = null;
                    nextWeapon = weapons[numberWeapon];
                    animator.SetTrigger("ChangeWeapon");
                    Debug.Log("ChangeWEAPON");
                }
            }
        }
    }
    public void SwitchWeapn()
    {
        if (selectedWeapon != null)
        {
            selectedWeapon.gameObject.SetActive(false);
        }
        selectedWeapon = nextWeapon;
        nextWeapon = null;
        animator.runtimeAnimatorController = selectedWeapon.WeaponData.OverrideController;
        selectedWeapon.gameObject.SetActive(true);
        Debug.Log("ACTIVATE WEAPON");
        isSwitching = false;
        switchedWeaponEvent?.Invoke();
    }
    public void Initialize(bool[] access, int[] improvement, int ammountGrenade)
    {
        maxAmmountGranade = ammountGrenade;
        remainingAmmountGranade = maxAmmountGranade;
        for (int i = 0; i < access.Length; i++)
        {
            accesWeapon.Add(weapons[i], access[i]);

            weapons[i].Initialize(improvement[i], enemyLayer);
        }
        initializedEvent?.Invoke();

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].Reload();
        }
        Debug.Log("SelectDeafaultWeapon");
        SelectWeapon(0);
        //animator.ResetTrigger("Reload");
        isReloading = false;
    }
    public void Reset()
    {
        accesWeapon.Clear();
        if (selectedWeapon)
        {
            selectedWeapon.gameObject.SetActive(false);
        }
        isReloading = false;
        isSwitching = false;
        isThrowingGrenade = false;
        selectedWeapon = null;
        remainingAmmountGranade = maxAmmountGranade;
        nextWeapon = null;
    }
}
