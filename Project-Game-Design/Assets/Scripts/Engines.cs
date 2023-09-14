using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engines : MonoBehaviour
{
    private BaseAvatar avatar;
    private BulletGun bulletGun;
    public Vector2 speed;
    [SerializeField] private float dashMultiplier = 2f;
    [SerializeField] private float dashEnergyCost = 10f;

    [Header("Bounds")]
    [SerializeField] private bool isPlayer;
    [SerializeField] private float[] xbounds = new float[2] { -7, 3.5f };
    [SerializeField] private float[] ybounds = new float[2] { -3.5f, 3.5f };

    private void Start() 
    {
        avatar = GetComponent<BaseAvatar>();
        bulletGun = GetComponent<BulletGun>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = StayInBounds(transform.position + new Vector3(speed.x, speed.y, 0).normalized * avatar.maxSpeed * Time.deltaTime);
    }

    public void Dash(float direction, int axis)
    {
        if(!bulletGun.UseEnergie(dashEnergyCost)) return;
        
        StartCoroutine(avatar.BecomeInvincible());
        if(axis == 0)
        {
            transform.position = StayInBounds(transform.position + new Vector3(direction * dashMultiplier, 0, 0));
        }
        else if(axis == 1)
        {
            transform.position = StayInBounds(transform.position + new Vector3(0, direction * dashMultiplier, 0));
        }
    }

    Vector3 StayInBounds(Vector3 nextPosition)
    {
        nextPosition.x = Mathf.Clamp(nextPosition.x, xbounds[0], xbounds[1]);
        nextPosition.y = Mathf.Clamp(nextPosition.y, ybounds[0], ybounds[1]);
        return nextPosition;
    }
}
