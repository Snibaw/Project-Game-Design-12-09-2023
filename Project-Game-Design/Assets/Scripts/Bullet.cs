using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [HideInInspector]
    [SerializeField] protected float damage;
    
    [HideInInspector]
    [SerializeField] protected Vector2 speed;

    [SerializeField] protected float minXValueBeforeDestroy;
    protected string targetTag;
    
    public virtual void Init(Vector2 speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }
    protected virtual void UpdatePosition()
    {
        transform.position += new Vector3(speed.x, speed.y, 0) * Time.deltaTime;
    }
    protected virtual void CheckIfNeedToDestroy()
    {
        if (Mathf.Abs(transform.position.x) > minXValueBeforeDestroy)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == targetTag)
        {
            other.gameObject.GetComponent<BaseAvatar>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    //Die when out of bounds
    protected void Update()
    {   
        UpdatePosition();
        CheckIfNeedToDestroy();
    }
}
