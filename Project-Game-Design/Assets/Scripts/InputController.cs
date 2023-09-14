using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Engines engines;
    private float x, y;
    private BulletGun bulletGun;
    [SerializeField] private float[] xbounds;
    [SerializeField] private float[] ybounds;
    private void Start() 
    {
        engines = GetComponent<Engines>();
        bulletGun = GetComponent<BulletGun>();
        x = 0;
        y = 0;
    }
    private void Update() 
    {
        x = Input.GetAxisRaw("Horizontal");

        
        if(transform.position.x < xbounds[0]) x = Mathf.Max(0,x);
        else if(transform.position.x > xbounds[1]) x = Mathf.Min(0, x);

        y = Input.GetAxisRaw("Vertical");
        if(transform.position.y < ybounds[0]) y = Mathf.Max(0,y);
        else if(transform.position.y > ybounds[1]) y = Mathf.Min(0,y);



        engines.speed = new Vector2(x, y);

        if (Input.GetKey(KeyCode.Space))
        {
            bulletGun.Fire();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            bulletGun.canRegen = true;
        }
    }
}
