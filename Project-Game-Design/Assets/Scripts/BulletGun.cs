using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletGun : MonoBehaviour
{
    public enum weaponType
    {
        continuous,
        diagonal,
        spiral,
    }
    public weaponType currentWeaponType;
    [SerializeField] private float damage;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float cooldown;
    private float timer;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Energie")]
    [SerializeField] private float maxEnergie = 100;
    [SerializeField] private float energieLostPerShots = 4;
    [SerializeField] private float energieRegen = 0.01f;
    [SerializeField] private float currentEnergieRegen;
    private float energieRegenFrom0;
    public bool canRegen = true;
    
    public float currentEnergie;
    private UIManager uIManager;
    private bool canShoot = true;


    private void Start() {
        timer = 0;
        currentEnergie = maxEnergie;
        currentEnergieRegen = energieRegen;
        energieRegenFrom0 = energieRegen * 0.75f;
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        uIManager.SetMaxEnergieSliderValue(maxEnergie);
    }
    private void Update() {
        timer -= Time.deltaTime;
        if(canRegen)
            GainEnergie(currentEnergieRegen);
        else
            uIManager.SetEnergieSliderValue(currentEnergie);
        if(currentEnergie >= maxEnergie)
            canShoot = true;
    }
    public void Fire()
    {
        if(!canShoot) return;

        canRegen = false;
        if (timer <= 0 && currentEnergie >= 0)
        {
            //Reset energie regen malus==
            currentEnergieRegen = energieRegen;


            timer = cooldown;
            currentEnergie -= energieLostPerShots;

            SpawnBullet();

            //If the energie goes down to 0, it regen slower
            if(currentEnergie <=0)
            {
                canShoot = false;
                currentEnergieRegen = energieRegenFrom0;
            }
        }
    }
    public void ChangeWeaponType()
    {
        int upgradeWeapon = PlayerPrefs.GetInt("weapon",0);
        int currentMaxWeapons = 1;
        if(upgradeWeapon < 3) return;
        if(upgradeWeapon >= 3)
            currentMaxWeapons = 2;
        if(upgradeWeapon >= 6)
            currentMaxWeapons = 3;

        int newIndex = ((int)currentWeaponType + 1) % currentMaxWeapons;
        currentWeaponType = (weaponType) newIndex;
        GameManager.instance.ChangeWeaponTypeUI(newIndex);

    }
    void SpawnBullet()
    {
        switch(currentWeaponType)
        {
            case weaponType.continuous:
                SpawnBulletContinuous();
                break;
            case weaponType.diagonal:
                SpawnBulletContinuous();
                SpawnBulletContinuous(false, new Vector3(0,0,45));
                SpawnBulletContinuous(false, new Vector3(0,0,-45));
                break;
            case weaponType.spiral:
                SpawnBulletContinuous(true, new Vector3(0,0,-45));
                break;
        }
    }
    void SpawnBulletContinuous(bool isSpriral = false, Vector3 rotation = default(Vector3))
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(speed, damage, isSpriral, rotation);
    }
    public bool UseEnergie(float energieUsed)
    {
        if(currentEnergie >= energieUsed)
        {
            currentEnergie -= energieUsed;
            return true;
        }
        return false;
    }
    public void GainEnergie(float energieGained)
    {
        currentEnergie = Mathf.Clamp(currentEnergie + energieGained, 0, maxEnergie);
        uIManager.SetEnergieSliderValue(Mathf.Clamp(currentEnergie,0,maxEnergie));
    }
    public void GainMaxEnergie(float maxEnergieGained)
    {
        maxEnergie += maxEnergieGained;
        uIManager.SetMaxEnergieSliderValue(maxEnergie);
    }
}
