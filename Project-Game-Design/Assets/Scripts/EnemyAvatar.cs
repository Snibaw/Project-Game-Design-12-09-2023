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
            GameManager.instance.ChangeScoreText(-10);
            Destroy(gameObject);
        }
    }
    protected override void Die()
    {
        GetComponent<DeathEvent>().Die();
        base.Die();
    }
}
