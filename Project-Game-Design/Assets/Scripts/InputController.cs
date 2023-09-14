using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Engines engines;
    private float x, y;
    private BulletGun bulletGun;

    [Header("Dash")]
    [SerializeField] private float reloadDashTime;
    private float timeSinceLastDash;
    [SerializeField] private float timeBtwInputForDash;
    private float[] timeSinceVarInput;
    private float[] lastVarInput;
    private bool[] canDash;
    private void Start() 
    {
        engines = GetComponent<Engines>();
        bulletGun = GetComponent<BulletGun>();
        x = 0;
        y = 0;

        InitDashVariables();
    }
    void InitDashVariables()
    {
        timeSinceLastDash = 0;

        lastVarInput = new float[2];
        lastVarInput[0] = 0;
        lastVarInput[1] = 0;

        timeSinceVarInput = new float[2];
        timeSinceVarInput[0] = 0;
        timeSinceVarInput[1] = 0;

        canDash = new bool[2];
        canDash[0] = false;
        canDash[1] = false;
    }
    private void Update() 
    {
        ControlMovement();

        ControlShoot();

        ControlWeapon();
    }
    void ControlMovement()
    {
        timeSinceLastDash -= Time.deltaTime;
        timeSinceVarInput[0] -= Time.deltaTime;
        timeSinceVarInput[1] -= Time.deltaTime;


        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        CheckIfDash(x, 0);
        CheckIfDash(y, 1);

        engines.speed = new Vector2(x, y);
    }
    void ControlShoot()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            bulletGun.Fire();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            bulletGun.canRegen = true;
        }
    }
    void ControlWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            bulletGun.ChangeWeaponType();
        }
    }
    private void CheckIfDash(float var, int index)
    {
        if(timeSinceLastDash > 0) return;
        if(var!=0)
        {
            //If the input is the same as the last one (!=0) and the time since the last input is less than the time between input for dash
            if(timeSinceVarInput[index] > 0 && timeSinceVarInput[1] < timeBtwInputForDash && lastVarInput[index]*var > 0 && canDash[index])
            {
                Debug.Log("engines.Dash()");  
                timeSinceVarInput[index] = 0;
                canDash[index] = false;
                engines.Dash(var, index);
                timeSinceLastDash = reloadDashTime;
            }
            else
            {
                timeSinceVarInput[index] = timeBtwInputForDash;
                lastVarInput[index] = var;
                canDash[index] = false;
            }
        }
        else
        {
            if(timeSinceVarInput[index] > 0 && timeSinceVarInput[1] < timeBtwInputForDash)
            {
                canDash[index] = true;
            }
            else
            {
                canDash[index] = false;
            }
        }
    }
}
