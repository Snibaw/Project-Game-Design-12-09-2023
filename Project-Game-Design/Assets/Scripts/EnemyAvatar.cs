using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAvatar : BaseAvatar
{
    public float minXValueBeforeDestroy;
    private void Update()
    {
        if (transform.position.x < minXValueBeforeDestroy)
        {
            Destroy(gameObject);
        }
    }
    protected override void Die()
    {
        GetComponent<DeathEvent>().Die();
        base.Die();
    }
}
