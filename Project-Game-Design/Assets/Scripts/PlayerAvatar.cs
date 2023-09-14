using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : BaseAvatar
{
    private UIManager uIManager;

    private void Start() {
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uIManager.InitHealthSlider(this.health);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        uIManager.SetHealthSliderValue(this.health);
    }
    protected override void Die()
    {
        GetComponent<DeathEvent>().Die();
    }
}
