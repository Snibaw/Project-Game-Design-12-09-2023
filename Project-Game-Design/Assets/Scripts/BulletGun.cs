using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletGun : MonoBehaviour
{
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

        uIManager.InitEnergieSlider(maxEnergie);
    }
    private void Update() {
        timer -= Time.deltaTime;
        if(canRegen) currentEnergie += currentEnergieRegen;
        if(currentEnergie >= maxEnergie)
        {
            canShoot = true;
            currentEnergie = maxEnergie;
        }
        uIManager.SetEnergieSliderValue(Mathf.Clamp(currentEnergie,0,maxEnergie));
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

            GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0.5f,0,0), Quaternion.identity);
            bullet.GetComponent<Bullet>().Init(speed, damage);

            //If the energie goes down to 0, it regen slower
            if(currentEnergie <=0)
            {
                canShoot = false;
                currentEnergieRegen = energieRegenFrom0;
            }
        }
    }
}
