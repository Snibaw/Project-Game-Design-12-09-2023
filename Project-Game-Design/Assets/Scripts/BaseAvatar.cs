using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAvatar : MonoBehaviour
{
    public float maxSpeed;
    [SerializeField] protected float health;
    [SerializeField] private GameObject explosionPrefab;

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        
        if(health <=0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion,0.6f);
        Destroy(gameObject);
    }
}
