using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : BaseAvatar
{
    [SerializeField] private float invincibleTime;
    private UIManager uIManager;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uIManager.InitHealthSlider(this.health);
    }
    public override void TakeDamage(float damage)
    {
        if(isInvincible) return;
        base.TakeDamage(damage);
        uIManager.SetHealthSliderValue(this.health);
    }
    public override IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        for(int i = 0; i < 2; i++)
        {
            spriteRenderer.color = new Color(1,1,1,0.5f);
            yield return new WaitForSeconds(invincibleTime/4);
            spriteRenderer.color = new Color(1,1,1,1);
            yield return new WaitForSeconds(invincibleTime/4);
        }
        isInvincible = false;
    }
    protected override void Die()
    {
        GetComponent<DeathEvent>().Die();
    }
    public override void GainHealth(float _health)
    {
        base.GainHealth(_health);
        uIManager.SetHealthSliderValue(health);
    }
}
