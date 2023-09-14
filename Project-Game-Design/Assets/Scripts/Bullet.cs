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
    [SerializeField] private float spriralRotationSpeed;
    protected string targetTag;
    private bool isSpriral = false;
    
    public virtual void Init(Vector2 speed, float damage, bool isSpriral = false, Vector3 rotation = default(Vector3))
    {
        this.speed = speed;
        this.damage = damage;
        this.isSpriral = isSpriral;
        this.transform.rotation = Quaternion.Euler(rotation);
    }
    protected virtual void UpdatePosition()
    {
        if(isSpriral)
        {
            transform.Rotate(0,0,spriralRotationSpeed);
            transform.position += new Vector3(speed.x, speed.y, 0) * Time.deltaTime/3;
        }
        transform.position += transform.right * speed.x * Time.deltaTime;
        transform.position += transform.up * speed.y * Time.deltaTime;
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
