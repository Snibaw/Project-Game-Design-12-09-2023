using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasicBulletGun : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float cooldown;
    private float timer;
    [SerializeField] private GameObject bulletPrefab;
    private void Start() {
        timer = 0;
    }
    private void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Fire();
        }

    }
    private void Fire()
    {
        timer = cooldown;
        GameObject bullet = Instantiate(bulletPrefab, transform.position+new Vector3(-0.8f,0,0), Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(speed, damage);
    }
}
