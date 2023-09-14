using System;
using UnityEngine;

public class DeathEvent : MonoBehaviour
{
    public delegate void OnDeathHandler(GameObject deadObject);

    public static event OnDeathHandler OnDeath;

    public void Die()
    {
        if(OnDeath != null) OnDeath?.Invoke(gameObject);
    }
}